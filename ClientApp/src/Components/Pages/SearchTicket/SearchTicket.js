import React, { useEffect, useState } from "react";
import "./SearchTicket.css";
import { useNavigate } from 'react-router-dom';

const SearchTicket = () => {
    const navigate = useNavigate();
    const [searchInfo, setSearchInfo] = useState({
        tenKhachHang: '',
        ngaySinh: '',
        cccd: ''
    });

    const handleShowInfo = async () => {
        try {
            const response = await fetch(`http://localhost:44430/api/ticket/GetTicketReviewDetails?name=${searchInfo.cccd}`);
            const data = await response.json();

            console.log("Data from API:", data);

            navigate('/ticket-rv', { state: { selectedCustomerInfo: data } });
        } catch (error) {
            console.error("Error fetching ticket details:", error);
        }
    };

    return (
        <div className="container2">
        <div className="text-insertSearch">
            <h1>TÌM KIẾM VÉ MÁY BAY</h1>
        </div>

        <div className="white-section">

        <div className="inforSearch">
            <form className="form-signin2">
                       
                        <div className="mb-3 row text-xl-center">
                            <label htmlFor="inputTicketCode" className="col-sm-2 col-form-label">Nhập CCCD</label>
                            <div className="col-sm-10">
                                <input
                                    type="text"
                                    className="form-controlSearchTicket"
                                    id="inputTicketCode"
                                    value={searchInfo.cccd}
                                    onChange={(e) => setSearchInfo({ ...searchInfo, cccd: e.target.value })}
                                />
                            </div>
                        </div>

                        <div className="row text-xl-center">
                            <div className="col-sm-10">
                                <button
                                    type="button"
                                    className="btn search"
                                    id="btnSearch"
                                    onClick={handleShowInfo}
                                >
                                    Tìm
                                </button>
                            </div>
                        </div>
            </form>
        </div>
    </div>
    </div>
    );
};
export default SearchTicket;