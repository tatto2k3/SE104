import React, { useState, useEffect } from "react";
import './ThemVe.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';

const ThemVe = () => {
    const [tId, setTId] = useState("");
    const [cccd, setCccd] = useState("");
    const [name, setName] = useState("");
    const [flyId, setFlyId] = useState("");
    const [seat_Type_ID, setSeatTypeID] = useState("");
    const [ticketPrice, setTicketPrice] = useState("");
    const [sdt, setSdt] = useState("");
    const [flyIDs, setFlyIDs] = useState([]);
    const [seatIDs, setSeatIDs] = useState([]);
    const [showSuccessMessage, setShowSuccessMessage] = useState(false);

    useEffect(() => {
        fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbaysanbay/GetFlyID")
            .then(response => response.json())
            .then(data => setFlyIDs(data))
            .catch(error => console.error("Error fetching fly IDs:", error));

        fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/ticket/GetSeatID")
            .then(response => response.json())
            .then(data => setSeatIDs(data))
            .catch(error => console.error("Error fetching airport IDs:", error));


    }, []);


    const handleSave = async () => {
        if (!isValidData()) {
            alert("Invalid Ticket data");
            return;
        }

        const ticketData = {
            tId: tId,
            cccd: cccd,
            name: name,
            flyId: flyId,  
            seat_Type_ID: seat_Type_ID,
            sdt: sdt,
        };
        try {
            const ticketResponse = await fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/ticket/AddTicket", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(ticketData),
        });
            if (ticketResponse.status === 500) {
                console.log("Ticket error:", ticketResponse);
                alert("Không thể đặt vé do quá hạn");
                return;
            }

            setShowSuccessMessage(true);
            setFlyId("");
            setTId("");
            setCccd("");
            setSdt("");
            setName("");
            setSeatTypeID("");
            setTimeout(() => setShowSuccessMessage(false), 3000);
        } catch (error) {
            console.log("Error:", error.message);
            alert(error);
        }
    };

    const isValidData = () => {
        return (
            tId.trim() !== ""
        );
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
                    Thêm vé thành công!
                </div>
            )}
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Thêm vé</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="tId" className="form-label">Mã vé</label>
                            <input
                                type="text"
                                className="form-control"
                                id="tId"
                                placeholder="Mã vé"
                                value={tId}
                                onChange={(e) => setTId(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="cccd" className="form-label">CCCD</label>
                            <input
                                type="number"
                                className="form-control"
                                id="cccd"
                                placeholder="CCCD"
                                value={cccd}
                                onChange={(e) => setCccd(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="name" className="form-label">Tên khách hàng</label>
                            <input
                                type="text"
                                className="form-control"
                                id="name"
                                placeholder="Tên khách hàng"
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="flyID" className="form-label">Mã chuyến bay</label>
                            <select
                                className="form-select"
                                id="flyID"
                                value={flyId}
                                onChange={(e) => setFlyId(e.target.value)}
                            >
                                <option value="">Chọn mã chuyến bay</option>
                                {flyIDs.map(flyID => (
                                    <option key={flyID} value={flyID}>{flyID}</option>
                                ))}

                            </select>
                        </div>
                        <div className="col-4">
                            <label htmlFor="kgId" className="form-label">Mã loại ghế</label>
                            <select
                                className="form-select"
                                id="seat_Type_ID"
                                value={seat_Type_ID}
                                onChange={(e) => setSeatTypeID(e.target.value)}
                            >
                                <option value="">Chọn mã loại ghế</option>
                                {seatIDs.map(seat => (
                                    <option key={seat} value={seat}>{seat}</option>
                                ))}

                            </select>
                        </div>
                        <div className="col-4">
                            <label htmlFor="seatId" className="form-label">Số điện thoại</label>
                            <input
                                type="number"
                                className="form-control"
                                id="sdt"
                                placeholder="Số điện thoại"
                                value={sdt}
                                onChange={(e) => setSdt(e.target.value)}
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

export default ThemVe;
