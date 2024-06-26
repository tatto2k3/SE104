﻿import * as React from 'react';
import "./TicketResult.css";
import logo from '../../../../assets/logo2.PNG';
import AttachMoneyOutlinedIcon from '@mui/icons-material/AttachMoneyOutlined';
export default function TicketResult({ flight, handleClick }) {
    function removeTrailingZeros(number) {
        const fixedNumber = number.toFixed(4);
        const trimmedNumber = parseFloat(fixedNumber);

        return trimmedNumber;
    }
    function formatTimeDuration(departureTime, flightTime) {

        console.log(flightTime.substring(0, 2));
        console.log(flightTime.substring(3));

        console.log(departureTime.substring(0, 2));
        console.log(departureTime.substring(3));

        let hour = parseInt(flightTime.substring(0, 2)) + parseInt(departureTime.substring(0, 2));
        let minute = parseInt(flightTime.substring(3)) + parseInt(departureTime.substring(3));

        if (minute >= 60) {
            hour++;
            minute -= 60;
        }

        const arriveTime = hour.toString().padStart(2, '0') + ":" + minute.toString().padStart(2, '0');



        //const departureDate = new Date(`2000-01-01T${departureTime}`);
        //const arrivalDate = new Date(`2000-01-01T${arrivalTime}`);

        //const durationInMinutes = (arrivalDate - departureDate) / (1000 * 60);
        //const hours = Math.floor(durationInMinutes / 60);
        //const minutes = durationInMinutes % 60;

        //let formattedDuration = `${hours} hr`;
        //if (minutes > 0) {
        //    formattedDuration += ` ${minutes} min`;
        // }

        return arriveTime;
    }

    return (
        <div className="Ticket-Wrapper" onClick={handleClick} >
            <div className="Ticket-Left" >
                <div className="Logo-Wrapper">
                    <div className="Logo-Image">
                        <img src={logo } />
                    </div>
                </div>
                <div className="schedule">
                    <div className="schedule-depart">
                        <p className="schedule-header">
                            Depart
                        </p>
                        <h5 className="schedule-time">
                            {flight.departureTime}
                        </h5>
                        <p className="schedule-date">
                            {flight.departureDay}
                        </p>
                    </div>
                    <div className="schedule-detail">
                        <span className="schedule-round-left">
                        </span>
                        <div className="time-duration-wrapper">

                            <div className="time-duration">
                                {flight.flightTime}
                            </div>

                        </div>

                        <span className="schedule-round-right"></span>
                    </div>
                    <div className="schedule-des">
                        <p className="schedule-header">
                            Arrive
                        </p>
                        <h5 className="schedule-time">
                            {formatTimeDuration(flight.departureTime, flight.flightTime)}
                        </h5>
                        <p className="schedule-date">
                            {flight.departureDay}
                        </p>
                    </div>

                </div>
            </div>

            <div className="Ticket-Right">
                <p className="Price-header">
                    Price
                </p>
                <div className="Price-value">
                    <p className="price-value">
                        {removeTrailingZeros(flight.originalPrice)}
                    </p>
                    <p className="price-value-VND" >VND</p>
                </div>
            </div>

        </div>
    )
}