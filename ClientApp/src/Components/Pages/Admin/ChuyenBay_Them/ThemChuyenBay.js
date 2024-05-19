import React, { useState ,useEffect } from "react";
import './ThemChuyenBay.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';

const ThemChuyenBay = () => {
    const [flyId, setFlyId] = useState("");
    const [originalPrice, setOriginalPrice] = useState("");
    const [fromLocation, setFromLocation] = useState("");
    const [toLocation, setToLocation] = useState("");
    const [departureTime, setDepartureTime] = useState("");
    const [departureDay, setDepartureDay] = useState("");
    const [flightTime, setFlightTime] = useState("");
    const [showSuccessMessage, setShowSuccessMessage] = useState(false);
    const [listPlace, setListPlace] = useState([]);


    useEffect(() => {
        fetch("/api/chuyenbay/GetPlace")
            .then(response => response.json())
            .then(data => setListPlace(data))
            .catch(error => console.error("Error fetching airport IDs:", error));

    }, []);

    const handleSave = async () => {
        if (!isValidData()) {
            alert("Invalid customer data");
            return;
        }

        const flightData = {
            flyId: flyId ,
            flightTime: flightTime,
            fromLocation: fromLocation ,
            toLocation: toLocation,  
            departureTime: departureTime,
            departureDay: departureDay,
            originalPrice: originalPrice,
        };
        try {
        const flightResponse = await fetch("api/chuyenbay/AddChuyenbay", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(flightData),
        });
            if (!flightResponse.ok) {
                const flightError = await flightResponse.json();
                console.error("Flight error:", flightError);
                alert("Failed to add flight");
                return;
            }

            setShowSuccessMessage(true);
            setFlyId("");
            setFromLocation("");
            setToLocation("");
            setFlightTime("");
            setDepartureDay("");
            setOriginalPrice("");
            setDepartureTime("");
            setTimeout(() => setShowSuccessMessage(false), 3000);
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const isValidData = () => {
        return (
            flyId.trim() !== ""
        );
    };

    return (
        <div className="container-fluid">
            {showSuccessMessage && (
                <div className="alert alert-success mt-3" role="alert">
                    Thêm chuyến bay thành công!
                </div>
            )}
            <div className="logo-container">
                <div className="logo-inner">
                    <img src={logo2} alt="Logo" className="logo-img" />
                </div>
                <span className="Logo-name">Blue Star</span>
            </div>

            <div className="head-name">
                <h2>Thêm chuyến bay</h2>
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
                                value={flyId}
                                onChange={(e) => setFlyId(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="fromLocation" className="form-label">Điểm đi</label>
                            <select
                                className="form-select"
                                id="fromLocation"
                                value={fromLocation}
                                onChange={(e) => setFromLocation(e.target.value)}

                            >
                                <option value="">Chọn mã sân bay</option>
                                {listPlace.map(airportID => (
                                    <option key={airportID} value={airportID}>{airportID}</option>
                                ))}

                            </select>
                        </div>
                        <div className="col-4">
                            <label htmlFor="toLocation" className="form-label">Điểm đến</label>
                            <select
                                className="form-select"
                                id="toLocation"
                                value={toLocation}
                                onChange={(e) => setToLocation(e.target.value)}

                            >
                                <option value="">Chọn mã sân bay</option>
                                {listPlace.map(airportID => (
                                    <option key={airportID} value={airportID}>{airportID}</option>
                                ))}

                            </select>
                        </div>
                    </div>
                    <div className="row mb-3">
                        <div className="col-4">
                            <label htmlFor="departureTime" className="form-label">Giờ đi</label>
                            <input
                                type="text"
                                className="form-control"
                                id="departureTime"
                                placeholder="Giờ đi"
                                value={departureTime}
                                onChange={(e) => setDepartureTime(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="departureDay" className="form-label">Ngày đi</label>
                            <input
                                type="date"
                                className="form-control"
                                id="departureDay"
                                placeholder="Ngày đi"
                                value={departureDay}
                                onChange={(e) => setDepartureDay(e.target.value)}
                            />
                        </div>
                        <div className="col-4">
                            <label htmlFor="flightTime" className="form-label">Thời gian đi</label>
                            <input
                                type="text"
                                className="form-control"
                                id="flightTime"
                                placeholder="Thời gian đi"
                                value={flightTime}
                                onChange={(e) => setFlightTime(e.target.value)}
                            />
                        </div>
                    </div>
                    <div className="row mb-3">
                        
                        <div className="col-4">
                            <label htmlFor="originalPrice" className="form-label">Giá vé</label>
                            <input
                                type="number"
                                className="form-control"
                                id="originalPrice"
                                placeholder="Giá vé"
                                value={originalPrice}
                                onChange={(e) => setOriginalPrice(e.target.value)}
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

export default ThemChuyenBay;
