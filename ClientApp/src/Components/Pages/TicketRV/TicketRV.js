import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useLocation , useNavigate} from 'react-router-dom';
import './TicketRV.css';
import jsPDF from 'jspdf';
import { useSearch } from '../../CustomHooks/SearchContext';

const TicketRV = () => {


    const location = useLocation();
    const [ticketReviewDetails, setTicketReviewDetails] = useState(location.state?.selectedCustomerInfo || []);
    const [searchResult, setSearchResult, isLoading, setIsLoading, searchInfo,
        setSearchInfo, tripType, setTripType, airport, setAirport, departFlight, setDepartFlight, ariveFlight, setArriveFlight,
        total1, setTotal1, foodItems1, setFoodItems1, total2, setTotal2,
        foodItems2, setFoodItems2, addFoodItem1, calculateTotal1, addFoodItem2, calculateTotal2, passengerInfo,
        setPassengerInfo, seatId, setSeatId, luggaeId, setLuggageId] = useSearch();
    const navigate = useNavigate();


    //useEffect(() => {
    //    if (ticketReviewDetails.length > 0) {
    //        // Nếu có thông tin khách hàng được chọn, cập nhật TicketInfo
    //        setTicketReviewDetails({
    //            cccd: ticketReviewDetails.cccd || '',
    //            name: ticketReviewDetails[0]?.name || '',
    //            flyId: ticketReviewDetails[0]?.flyId || '',
    //            seatId: ticketReviewDetails[0]?.seat_Type_ID || '',
    //            flightTime: ticketReviewDetails[0]?.flightTime || '',
    //            departureDay: ticketReviewDetails[0]?.departureDay || '',
    //            departureTime: ticketReviewDetails[0]?.departureTime || '',
    //        });
    //    }
    //}, [ticketReviewDetails]);


    const handleDownload = () => {
       
        navigate("/");
    };

    return (
        <div className="container mt-4 containerHeight">
            <div className="row justify-content-center">
                <div className="col-md-10">
                    <div className="card">
                        <div className="card-header text-white">
                            <h3 className="mb-0">Thông tin vé máy bay</h3>
                        </div>
                        <div className="card-body">
                            <div className="row">
                                <div className="col-md-3 column-with-border">
                                    <div className="form-group">
                                        <label htmlFor="departureDate">Ngày khởi hành:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="departureDate"
                                            value={ticketReviewDetails.departureDay}
                                            readOnly
                                        />
                                    </div>
                                    <div className="form-group row-with-border">
                                        <label htmlFor="fullName">Giờ khởi hành:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="departureTime"
                                            value={ticketReviewDetails.departureTime}
                                            readOnly
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="seatNumber">Mã chỗ ngồi:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="seatNumber"
                                            value={ticketReviewDetails.seat_Type_ID}
                                            readOnly
                                        />
                                    </div>
                                </div>
                                <div className="col-md-3 column-with-border">
                                    <div className="form-group">
                                        <label htmlFor="arrivalDate">Ngày đến:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="arrivalDate"
                                            value={ticketReviewDetails.departureDay}
                                            readOnly
                                        />
                                    </div>
                                    <div className="form-group row-with-border">
                                        <label htmlFor="cccd">Thời gian đi:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="arrivalTime"
                                            value={ticketReviewDetails.flightTime}
                                            readOnly
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="flightCode">Mã chuyến bay:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="flightCode"
                                            value={ticketReviewDetails.flyId}
                                            readOnly
                                        />
                                    </div>
                                </div>
                                <div className="col-md-2">
                                    <img src="/Images/Plane.png" alt="" className="img-flight" />
                                </div>
                                <div className="col-md-3">
                                    <div className="form-group">
                                        <label htmlFor="fullName">Họ và tên:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="fullName"
                                            value={ticketReviewDetails.name}
                                            readOnly
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label htmlFor="cccd">CCCD:</label>
                                        <input
                                            type="text"
                                            className="form-control"
                                            id="cccd"
                                            value={ticketReviewDetails.cccd}
                                            readOnly
                                        />
                                    </div>
                                    <div className="button-card">

                                        <button type="button" className="btn btn-outline-primary btn-block" onClick={handleDownload}>
                                            Trang chủ
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default TicketRV;
