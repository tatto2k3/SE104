import React, { useState , useEffect } from "react";
import './SuaHanhLy.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';
import { useLocation } from 'react-router-dom';

const SuaHanhLy = () => {
    const location = useLocation();
    const [selectedLuggageInfo, setSelectedLuggageInfo] = useState(location.state?.selectedLuggageInfo || []);
    const [flyIDs, setFlyIDs] = useState([]);
    const [airportIDs, setAirportIDs] = useState([]);
    const [showSuccessMessage, setShowSuccessMessage] = useState(false);

    useEffect(() => {
        console.log("Selected Luggage info in SuaKhachHang useEffect:", selectedLuggageInfo);
        // Các thao tác khác với selectedLuggageInfo
    }, [selectedLuggageInfo]);



    const [luggageInfo, setLuggageInfo] = useState({
        flyID: '',
        airportID: '',
        time: 0,
        note: ''
    });

    useEffect(() => {
        if (selectedLuggageInfo != null ) {
            // Nếu có thông tin khách hàng được chọn, cập nhật LuggageInfo
            setLuggageInfo({
                flyID: selectedLuggageInfo.flyId || '',
                airportID: selectedLuggageInfo.airportId || '',
                time: selectedLuggageInfo.time || '',
                note: selectedLuggageInfo.note || '',
            });
        }
    }, [selectedLuggageInfo]);


    console.log(luggageInfo)


    const handleChange = (e) => {
        const { id, value } = e.target;

        setLuggageInfo({
            ...luggageInfo,
            [id]: value,
        });
    };


    // Phía máy khách - SuaKhachHang.js
    const handleSave = async function update(event) {
        event.preventDefault();
        try {
            if (!luggageInfo || !luggageInfo.flyID) {
                alert("Không tìm thấy thông tin hành khách");
                return;
            }

            const updatedData = {
                flyID: luggageInfo.flyID,
                airportID: luggageInfo.airportID,
                time: luggageInfo.time,
                note: luggageInfo.note,
            };

            if (!updatedData.flyID) {
                alert("Mã hành lý là bắt buộc");
                return;
            }

            const response = await fetch('http://localhost:44430/api/chuyenbaysanbay/UpdateLuggage', {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(updatedData),
            });

            if (!response.ok) {
                const errorMessage = await response.text();
                throw new Error(errorMessage);
            }

            setShowSuccessMessage(true);
            setTimeout(() => setShowSuccessMessage(false), 3000);
        } catch (err) {
            alert("Lỗi: " + err.response);
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
                    Sửa chuyến bay trung gian thành công!
                </div>
            )}
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Sửa thông tin chuyến bay trung gian</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="maMayBay" className="form-label">Mã chuyến bay</label>
                            <input
                                type="text"
                                className="form-control"
                                id="flyID"
                                placeholder="Mã chuyến bay"
                                value={luggageInfo.flyID}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="maMayBay" className="form-label">Mã sân bay</label>
                            <input
                                type="text"
                                className="form-control"
                                id="airportID"
                                placeholder="Mã sân bay"
                                value={luggageInfo.airportID}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>
                        
                    </div>
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="luggageCode" className="form-label">Thời gian dừng</label>
                            <input
                                type="number"
                                className="form-control"
                                id="time"
                                placeholder="Thời gian dừng"
                                value={luggageInfo.time}
                                onChange={handleChange}
                                
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="mass" className="form-label">Ghi chú</label>
                            <input
                                type="text"
                                className="form-control"
                                id="note"
                                placeholder="Ghi chú"
                                value={luggageInfo.note}
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
                <a href="./SanBayTrungGian" className="text-decoration-underline-mk">Quay lại trang dành cho sân bay trung gian</a>
            </div>
        </div>
    );
}

export default SuaHanhLy;
