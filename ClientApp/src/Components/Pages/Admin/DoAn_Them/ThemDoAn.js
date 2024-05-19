import React, { useState } from "react";
import './ThemDoAn.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import logo2 from '../../../../assets/logo2.PNG';

const ThemDoAn = () => {
    const [seatID, setSeatID] = useState("");
    const [percent, setPercent] = useState("");


    const handleSave = async () => {
        if (!isValidData()) {
            alert("Invalid Food data");
            return;
        }

        const FoodData = {
            seatID: seatID,
            percent: percent

        };
        try {
        const FoodResponse = await fetch("api/seat/AddSeats", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(FoodData),
        });
            if (!FoodResponse.ok) {
                const FoodError = await FoodResponse.json();
                console.error("Food error:", FoodError);
                alert("Failed to add Food");
                return;
            }

            alert("Food added successfully");
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const isValidData = () => {
        return (
            seatID.trim() !== ""
        );
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
                <h2>Thêm thông tin thức ăn</h2>
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
                                placeholder="Mã loại ghế"
                                value={seatID}
                                onChange={(e) => setSeatID(e.target.value)}
                            />
                        </div>
                        <div className="col-6">
                            <label htmlFor="fName" className="form-label">Phần trăm giá gốc</label>
                            <input
                                type="text"
                                className="form-control"
                                id="percent"
                                placeholder="Phần trăm giá gốc"
                                value={percent}
                                onChange={(e) => setPercent(e.target.value)}
                            />
                        </div>
                    </div>
                   
                    <div className="d-flex justify-content-center mt-3">
                        <button type="button" className="btn btn-primary" onClick={handleSave}>Lưu</button>
                    </div>
                </form>
            </div>
            <div className="back">
                <a href="./DoAn" className="text-decoration-underline-mk">Quay lại trang dành cho ghế ngồi</a>
            </div>
        </div>
    );
}

export default ThemDoAn;
