import React, { useState, useEffect } from "react";
import './SuaDoAn.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';
import { useLocation } from 'react-router-dom';
import axios from 'axios';

const SuaDoAn = () => {
    const location = useLocation();
    const [selectedFoodInfo, setSelectedFoodInfo] = useState(location.state?.selectedFoodInfo || []);

    useEffect(() => {
        console.log("Selected Food info in SuaKhachHang useEffect:", selectedFoodInfo);
        // Các thao tác khác với selectedFoodInfo
    }, [selectedFoodInfo]);

    const [foodInfo, setFoodInfo] = useState({
        seatID: '',
        percent: '',
    });

    useEffect(() => {
        if (selectedFoodInfo.length > 0) {
            // Nếu có thông tin khách hàng được chọn, cập nhật FoodInfo
            setFoodInfo({
                seatID: selectedFoodInfo[0]?.seatID || '',
                percent: selectedFoodInfo[0]?.percent || '',
            });
        }
    }, [selectedFoodInfo]);


    const handleChange = (e) => {
        const { id, value } = e.target;

        setFoodInfo({
            ...foodInfo,
            [id]: value,
        });
    };


    // Phía máy khách - SuaKhachHang.js
    const handleSave = async function update(event) {
        event.preventDefault();
        try {

            if (!foodInfo || !foodInfo.seatID) {
                alert("Thức ăn không được tìm thấy");
                return;
            }

            const updatedData = {
                seatID: foodInfo.seatID,
                percent: foodInfo.percent,
            };


            if (!updatedData.seatID) {
                alert("FId là bắt buộc");
                return;
            }

            // Sử dụng fetch để thực hiện yêu cầu PUT
            const response = await fetch('api/seat/UpdateSanbay', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedData),
            });

            if (!response.ok) {
                // Xử lý lỗi
                const errorMessage = await response.text();
                throw new Error(JSON.stringify(errorMessage));
            }

            alert("Thức ăn đã được cập nhật");

        } catch (err) {
            // Xử lý lỗi
            alert(err.message);
        }
    };

    return (
        <div className="container-fluid">
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Sửa thông tin thức ăn</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="fId" className="form-label">Mã loại ghế</label>
                            <input
                                type="text"
                                className="form-control"
                                id="seatID"
                                placeholder="Mã thức ăn"
                                value={foodInfo.seatID}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="fName" className="form-label">Phần trăm giá gốc</label>
                            <input
                                type="text"
                                className="form-control"
                                id="percent"
                                placeholder="Tên thức ăn"
                                value={foodInfo.percent}
                                onChange={handleChange}
                            />
                        </div>
                    </div>
                   
                    <div className="d-flex justify-content-center mt-3">
                        <button type="button" className="btn btn-primary" onClick={handleSave}>Lưu</button>
                    </div>
                </form>
            </div>
            <div className="back">
                <a href="./DoAn" className="text-decoration-underline-mk">Quay lại trang dành cho thức ăn</a>
            </div>
        </div>
    );
}

export default SuaDoAn;
