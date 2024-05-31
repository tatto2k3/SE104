import React, { useState, useEffect } from "react";
import './ThemHanhLy.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';

const ThemHanhLy = () => {
    const [flyID, setFlyID] = useState("");
    const [airportID, setAirportID] = useState("");
    const [time, setTime] = useState("");
    const [note, setNote] = useState("");
    const [flyIDs, setFlyIDs] = useState([]); 
    const [airportIDs, setAirportIDs] = useState([]);
    const [showSuccessMessage, setShowSuccessMessage] = useState(false);


    useEffect(() => {
        fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbaysanbay/GetFlyID")
            .then(response => response.json())
            .then(data => setFlyIDs(data))       
            .catch(error => console.error("Error fetching fly IDs:", error));

        fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbaysanbay/GetAirportID")
            .then(response => response.json())
            .then(data => setAirportIDs(data))
            .catch(error => console.error("Error fetching airport IDs:", error));

           
    }, []);
    console.log(flyIDs);

    const handleSave = async () => {
        if (!isValidData()) {
            alert("Invalid luggage data");
            return;
        }

        const luggageData = {
            flyID: flyID,
            airportID: airportID,
            time: time,
            note: note
        };
        try {
            const luggageResponse = await fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbaysanbay/AddChuyenbay_Sanbays", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(luggageData),
        });
            if (!luggageResponse.ok) {
                const luggageError = await luggageResponse.json();
                console.error("Luggage error:", luggageError);
                alert("Failed to add Luggage");
                return;
            }

            setShowSuccessMessage(true);
            setFlyID("");
            setAirportID("");
            setTime("");
            setNote("");
            setTimeout(() => setShowSuccessMessage(false), 3000);
        } catch (error) {
            alert("Lỗi thời gian dừng hoặc sân bay.");
        }
    };

    const isValidData = () => {
        return (
            flyID.trim() !== ""
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
                    Thêm chuyến bay trung gian thành công!
                </div>
            )}
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Thêm sân bay trung gian</h2>
            </div>

            <div className="infor-cn">
                <form className="form-signin-cn">
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="flyID" className="form-label">Mã chuyến bay</label>
                            <select
                                className="form-select"
                                id="flyID"
                                value={flyID}
                                onChange={(e) => setFlyID(e.target.value)}
                            >
                                <option value="">Chọn mã chuyến bay</option>
                                {flyIDs.map(flyID => (
                                    <option key={flyID} value={flyID}>{flyID}</option>
                                ))}

                            </select>
                        </div>
                        <div className="col-6">
                            <label htmlFor="airportID" className="form-label">Mã sân bay</label>
                            <select
                                className="form-select"
                                id="airportID"
                                value={airportID}
                                onChange={(e) => setAirportID(e.target.value)}
                                
                            >
                                <option value="">Chọn mã sân bay</option>
                                {airportIDs.map(airportID => (
                                    <option key={airportID} value={airportID}>{airportID}</option>
                                ))}

                            </select>
                        </div>
                       
                    </div>
                    <div className="row mb-3">
                        <div className="col-6">
                            <label htmlFor="luggageCode" className="form-label">Thời gian dừng</label>
                            <input
                                type="text"
                                className="form-control"
                                id="time"
                                placeholder="Thời gian dừng"
                                value={time}
                                onChange={(e) => setTime(e.target.value)}
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="mass" className="form-label">Ghi chú</label>
                            <input
                                type="text"
                                className="form-control"
                                id="note"
                                placeholder="Ghi chú"
                                value={note}
                                onChange={(e) => setNote(e.target.value)}
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

export default ThemHanhLy;
