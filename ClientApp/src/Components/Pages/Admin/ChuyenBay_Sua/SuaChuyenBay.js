import React, { useState, useEffect } from "react";
import './SuaChuyenBay.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';
import { useLocation } from 'react-router-dom';
import axios from 'axios';

const SuaChuyenBay = () => {
    const location = useLocation();
    const [selectedChuyenbayInfo, setSelectedChuyenbayInfo] = useState(location.state?.selectedChuyenbayInfo || []);
    const [showSuccessMessage, setShowSuccessMessage] = useState(false);
    const [listPlace, setListPlace] = useState([]);


    useEffect(() => {
        fetch("/api/chuyenbay/GetPlace")
            .then(response => response.json())
            .then(data => setListPlace(data))
            .catch(error => console.error("Error fetching airport IDs:", error));

    }, []);

    useEffect(() => {
        console.log("Selected chuyenbay info in SuaKhachHang useEffect:", selectedChuyenbayInfo);
        // Các thao tác khác với selectedchuyenbayInfo
    }, [selectedChuyenbayInfo]);

    const [chuyenbayInfo, setChuyenbayInfo] = useState({
        flyId: '',
        flightTime: '',
        fromLocation: '',
        toLocation: '',
        departureTime: '',
        seatEmpty: 0,
        seatBooked: 0,
        departureDay: '',
        originalPrice: 0,
        intermediate: ''
    });

    useEffect(() => {
        if (selectedChuyenbayInfo != null) {
            setChuyenbayInfo({
                flyId: selectedChuyenbayInfo.chuyenbayDetails[0]?.flyId || '',
                flightTime: selectedChuyenbayInfo.chuyenbayDetails[0]?.flightTime || '',
                fromLocation: selectedChuyenbayInfo.chuyenbayDetails[0]?.fromLocation || '',
                toLocation: selectedChuyenbayInfo.chuyenbayDetails[0]?.toLocation || '',
                departureTime: selectedChuyenbayInfo.chuyenbayDetails[0]?.departureTime || '',
                seatEmpty: selectedChuyenbayInfo.chuyenbayDetails[0]?.seatEmpty || 0,
                SeatBooked: selectedChuyenbayInfo.chuyenbayDetails[0]?.SeatBooked || 0,
                departureDay: selectedChuyenbayInfo.chuyenbayDetails[0]?.departureDay || '',
                originalPrice: selectedChuyenbayInfo.chuyenbayDetails[0]?.originalPrice || 0,
                intermediate: selectedChuyenbayInfo.trunggianList || '',
            });
        }
    }, [selectedChuyenbayInfo]);


    const handleChange = (e) => {
        const { id, value } = e.target;

        setChuyenbayInfo({
            ...chuyenbayInfo,
            [id]: value,
        });
    };


    // Phía máy khách - SuaKhachHang.js
    const handleSave = async function update(event) {
        event.preventDefault();
        try {

            if (!chuyenbayInfo || !chuyenbayInfo.flyId) {
                alert("Khách hàng không được tìm thấy");
                return;
            }

            const updatedData = {
                flyId: chuyenbayInfo.flyId,
                flightTime: chuyenbayInfo.flightTime,
                fromLocation: chuyenbayInfo.fromLocation,
                toLocation: chuyenbayInfo.toLocation,
                departureTime: chuyenbayInfo.departureTime,
                seatEmpty: chuyenbayInfo.seatEmpty,
                seatBooked: chuyenbayInfo.seatBooked,
                departureDay: chuyenbayInfo.departureDay,
                originalPrice: chuyenbayInfo.originalPrice,
            };


            if (!updatedData.flyId) {
                alert("CId là bắt buộc");
                return;
            }

            // Sử dụng fetch để thực hiện yêu cầu PUT
            const response = await fetch('https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbay/UpdateChuyenbay', {
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
                    Sửa chuyến bay thành công!
                </div>
            )}
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Sửa thông tin chuyến bay</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="maChuyenBay" className="form-label">Mã chuyến bay</label>
                            <input
                                type="text"
                                className="form-control"
                                id="maChuyenBay"
                                placeholder="Mã chuyến bay"
                                value={chuyenbayInfo.flyId}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="fromLocation" className="form-label">Điểm đi</label>
                            <select
                                className="form-control"
                                id="fromLocation"
                                onChange={handleChange}
                            >
                                <option value="">{chuyenbayInfo.fromLocation}</option>
                                {listPlace.map((boat) => (
                                    <option key={boat} value={boat}>{boat}</option>
                                ))}
                            </select>
                        </div>
                        <div className="col-4">
                            <label htmlFor="toLocation" className="form-label">Điểm đến</label>
                            <select
                                className="form-control"
                                id="toLocation"
                                onChange={handleChange}
                            >
                                <option value="">{chuyenbayInfo.toLocation}</option>
                                {listPlace.map((boat) => (
                                    <option key={boat} value={boat}>{boat}</option>
                                ))}
                            </select>
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="departureDay" className="form-label">Ngày đi</label>
                            <input
                                type="date"
                                className="form-control"
                                id="departureDay"
                                placeholder="Ngày đi"
                                value={chuyenbayInfo.departureDay}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="departureTime" className="form-label">Giờ đi</label>
                            <input
                                type="text"
                                className="form-control"
                                id="departureTime"
                                placeholder="Giờ đi"
                                value={chuyenbayInfo.departureTime}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="flightTime" className="form-label">Thời gian đi</label>
                            <input
                                type="text"
                                className="form-control"
                                id="flightTime"
                                placeholder="Thời gian đi"
                                value={chuyenbayInfo.flightTime}
                                onChange={handleChange}
                            />
                        </div>
                    </div>
                    
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="originalPrice" className="form-label">Giá tiền</label>
                            <input
                                type="text"
                                className="form-control"
                                id="originalPrice"
                                placeholder="Giá tiền"
                                value={chuyenbayInfo.originalPrice}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="intermediate" className="form-label">Trung gian</label>
                            <input
                                type="text"
                                className="form-control"
                                id="intermediate"
                                placeholder="Trung gian"
                                value={chuyenbayInfo.intermediate}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>

                    </div>
                    <div className="d-flex justify-content-center mt-3">
                        <button type="button" className="btn btn-primary" onClick={handleSave}>Lưu</button>
                    </div>
                </form>
            </div>
            <div className="back">
                <a href="./ChuyenBay" className="text-decoration-underline-mk">Quay lại trang dành cho chuyến bay</a>
            </div>
        </div>
    );
}

export default SuaChuyenBay;
