import React, { useState, useEffect } from "react";
import './SuaVe.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';
import { useLocation } from 'react-router-dom';
import axios from 'axios';

const SuaVe = () => {
    const location = useLocation();
    const [selectedTicketInfo, setSelectedTicketInfo] = useState(location.state?.selectedTicketInfo || []);
    const [flyIDs, setFlyIDs] = useState([]);
    const [seatIDs, setSeatIDs] = useState([]);
    const [showSuccessMessage, setShowSuccessMessage] = useState(false);

    useEffect(() => {
        fetch("/api/chuyenbaysanbay/GetFlyID")
            .then(response => response.json())
            .then(data => setFlyIDs(data))
            .catch(error => console.error("Error fetching fly IDs:", error));

        fetch("/api/ticket/GetSeatID")
            .then(response => response.json())
            .then(data => setSeatIDs(data))
            .catch(error => console.error("Error fetching airport IDs:", error));


    }, []);

    useEffect(() => {
        console.log("Selected Ticket info in SuaKhachHang useEffect:", selectedTicketInfo);
        // Các thao tác khác với selectedTicketInfo
    }, [selectedTicketInfo]);

    const [ticketInfo, setTicketInfo] = useState({
        tId: '',
        cccd: '',
        name: '',
        flyId: '',
        seat_Type_ID: '',
        sdt: '',
        ticketPrice: '',

    });

    useEffect(() => {
        if (selectedTicketInfo.length > 0) {
            // Nếu có thông tin khách hàng được chọn, cập nhật TicketInfo
            setTicketInfo({
                tId: selectedTicketInfo[0]?.tId || '',
                cccd: selectedTicketInfo[0]?.cccd || '',
                name: selectedTicketInfo[0]?.name || '',
                flyId: selectedTicketInfo[0]?.flyId || '',
                seat_Type_ID: selectedTicketInfo[0]?.seat_Type_ID || '',
                ticketPrice: selectedTicketInfo[0]?.ticketPrice || '',
                sdt: selectedTicketInfo[0]?.sdt || '',
            });
        }
    }, [selectedTicketInfo]);


    const handleChange = (e) => {
        const { id, value } = e.target;

        setTicketInfo({
            ...ticketInfo,
            [id]: value,
        });
    };


    // Phía máy khách - SuaKhachHang.js
    const handleSave = async function update(event) {
        event.preventDefault();
        try {

            if (!ticketInfo || !ticketInfo.tId) {
                alert("Vé không được tìm thấy");
                return;
            }

            const updatedData = {
                tId: ticketInfo.tId,
                cccd: ticketInfo.cccd,
                name: ticketInfo.name,
                flyId: ticketInfo.flyId,
                seat_Type_ID: ticketInfo.seat_Type_ID,
                sdt: ticketInfo.sdt,
                ticketPrice: ticketInfo.ticketPrice,
            };


            if (!updatedData.tId) {
                alert("TId là bắt buộc");
                return;
            }

            // Sử dụng fetch để thực hiện yêu cầu PUT
            const response = await fetch('api/ticket/UpdateTicket', {
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

    return (
        <div className="container-fluid">
            {showSuccessMessage && (
                <div className="alert alert-success mt-3" role="alert">
                    Sửa vé thành công!
                </div>
            )}
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Sửa thông tin vé</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="tId" className="form-label">Mã vé</label>
                            <input
                                type="text"
                                className="form-control"
                                id="tId"
                                placeholder="Mã vé"
                                value={ticketInfo.tId}
                                onChange={handleChange}
                                readOnly
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="cccd" className="form-label">CCCD</label>
                            <input
                                type="text"
                                className="form-control"
                                id="cccd"
                                placeholder="Tên khách hàng"
                                value={ticketInfo.cccd}
                                onChange={handleChange}
                            />
                        </div>
                        
                    </div>
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="name" className="form-label">Tên khách hàng</label>
                            <input
                                type="text"
                                className="form-control"
                                id="name"
                                placeholder="Tên khách hàng"
                                value={ticketInfo.name}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="col-6">
                        <label htmlFor="maMayBay" className="form-label">Mã chuyến bay</label>
                        <select
                            className="form-control"
                            id="flyId"
                            onChange={handleChange}
                        >
                            <option value="">{ticketInfo.flyId}</option>
                            {flyIDs.map((flyID) => (
                                <option key={flyID} value={flyID}>{flyID}</option>
                            ))}
                        </select>

                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="seat_Type_ID" className="form-label">Mã loại ghế</label>
                            <select
                                className="form-control"
                                id="seat_Type_ID"
                                onChange={handleChange}
                            >
                                <option value="">{ticketInfo.seat_Type_ID}</option>
                                {seatIDs.map((seatID) => (
                                    <option key={seatID} value={seatID}>{seatID}</option>
                                ))}
                            </select>
                        </div>
                        <div className="col-4">
                            <label htmlFor="mail" className="form-label">Số điện thoại</label>
                            <input
                                type="text"
                                className="form-control"
                                id="sdt"
                                placeholder="Email"
                                value={ticketInfo.sdt}
                                onChange={handleChange}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="ticketPrice" className="form-label">Giá vé</label>
                            <input
                                type="text"
                                className="form-control"
                                id="ticketPrice"
                                placeholder="Giá vé"
                                value={ticketInfo.ticketPrice}
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
                <a href="./Ve" className="text-decoration-underline-mk">Quay lại trang dành cho vé</a>
            </div>
        </div>
    );
}

export default SuaVe;
