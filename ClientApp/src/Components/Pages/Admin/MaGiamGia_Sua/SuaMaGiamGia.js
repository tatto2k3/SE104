import React, { useState, useEffect } from "react";
import './SuaMaGiamGia.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';
import { useLocation } from 'react-router-dom';
import axios from 'axios';



const SuaMaGiamGia = () => {
    const location = useLocation();
    const [selectedDiscountInfo, setSelectedDiscountInfo] = useState(location.state?.selectedDiscountInfo || []);
    const [showSuccessMessage, setShowSuccessMessage] = useState(false);

    useEffect(() => {
        console.log("Selected discount info in SuaKhachHang useEffect:", selectedDiscountInfo);
        // Các thao tác khác với selecteddiscountInfo
    }, [selectedDiscountInfo]);

    const [discountInfo, setDiscountInfo] = useState({
        label: '',
        value: '',
    });

    useEffect(() => {
        if (selectedDiscountInfo != null) {
            // Nếu có thông tin khách hàng được chọn, cập nhật discountInfo
            setDiscountInfo({
                label: selectedDiscountInfo.label || '',
                value: selectedDiscountInfo.value || '',
            });
        }
    }, [selectedDiscountInfo]);


    const handleChange = (e) => {
        const { id, value } = e.target;

        setDiscountInfo({
            ...discountInfo,
            [id]: value,
        });
    };


    // Phía máy khách - SuaKhachHang.js
    const handleSave = async function update(event) {
        event.preventDefault();
        try {

            if (!discountInfo || !discountInfo.label) {
                alert("Khuyến mãi không được tìm thấy");
                return;
            }

            const updatedData = {
                label: discountInfo.label,
                value: discountInfo.value,
                valueBefore: selectedDiscountInfo.value
            };

          
            if (!updatedData.label) {
                alert("label là bắt buộc");
                return;
            }

            console.log(updatedData);

            // Sử dụng fetch để thực hiện yêu cầu PUT
            const response = await fetch('http://localhost:44430/api/parameters/UpdateParameter', {
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

            setShowSuccessMessage(true);
            setTimeout(() => setShowSuccessMessage(false), 3000);

        } catch (err) {
            // Xử lý lỗi
            alert(err.message);
        }
    };

    if (!localStorage.getItem('emailNhanVien')) {
        return (
            <div className="containerPersonal">
                <div className="text-insertPersonal">
                </div>
            </div>
        );
    }

    return (
        <div className="container-fluid">
            {showSuccessMessage && (
                <div className="alert alert-success mt-3" role="alert">
                    Sửa quy định thành công!
                </div>
            )}
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Sửa thông tin quy định</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="label" className="form-label">Thông tin quy định</label>
                            <input
                                type="text"
                                className="form-control"
                                id="label"
                                placeholder="Mã khuyến mãi"
                                value={discountInfo.label}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="value" className="form-label">Giá trị</label>
                            <input
                                type="text"
                                className="form-control"
                                id="value"
                                placeholder="Tên khuyến mãi"
                                value={discountInfo.value}
                                onChange={handleChange}
                            />
                        </div>
                      
                    </div>
                    
                    <div className="d-flex justify-content-center mt-3">
                        <button type="button" className="btn btn-primary" onClick={handleSave} >Lưu</button>
                    </div>
                </form>
            </div>
            <div className="back">
                <a href="./QuyDinh" className="text-decoration-underline-mk">Quay lại trang dành cho quy định</a>
            </div>
        </div>
    );
}

export default SuaMaGiamGia;
