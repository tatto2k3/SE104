import React from "react";
import './ChuyenBay.css';
import { useState, useEffect } from 'react';
import moment from "moment";
import axios from 'axios';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.min.css';

const ChuyenBay = () => {
    const [searchKeyword, setSearchKeyword] = useState('');
    const [chuyenbays, setChuyenbays] = useState([]);
    const [selectedChuyenbays, setSelectedChuyenbays] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage] = useState(20);
    const navigate = useNavigate();

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = chuyenbays.slice(indexOfFirstItem, indexOfLastItem);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const handleClick = () => {
        navigate('/ChuyenBay_Them');
    };



    useEffect(() => {
        // Lấy danh sách khách hàng từ API hoặc nguồn dữ liệu khác
        const fetchData = async () => {
            try {
                const response = await fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbay/GetChuyenbays");
                const data = await response.json();
                console.log(data);
                setChuyenbays(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);
    

    const handleCheckboxChange = (chuyenbayId) => {
        if (selectedChuyenbays.includes(chuyenbayId)) {
            // Nếu đã chọn thì hủy chọn
            setSelectedChuyenbays([]);
        } else {
            // Nếu chưa chọn thì chọn
            setSelectedChuyenbays([chuyenbayId]);
        }
    };



    const handleShowInfo = async () => {
        try {
            if (selectedChuyenbays.length > 0) {
                const response = await fetch(`/api/chuyenbay/GetChuyenbayDetails?flyIds=${selectedChuyenbays.join(',')}`);
                const data = await response.json();

                const departureDay = moment(data.chuyenbayDetails[0].departureDay);             
                const today = moment();
                if (departureDay.isBefore(today)) {
                    alert("Không thể sửa thông tin của chuyến bay đã qua.");
                    return;
                }

                // Chuyển hướng sang trang sửa khách hàng và truyền thông tin khách hàng
                navigate('/ChuyenBay_Sua', { state: { selectedChuyenbayInfo: data } });
            } else {
                console.log("No chuyenbays selected.");
            }
        } catch (error) {
            console.error("Error fetching chuyenbay details:", error);
        }
    };

    const handleDelete = async () => {

        console.log(selectedChuyenbays);
        if (selectedChuyenbays.length > 0) {

            if (window.confirm("Bạn có chắc chắn xóa chuyến bay này?")) {


                const today = new Date();
                const pastDateFlights = chuyenbays.filter(chuyenbay =>
                    selectedChuyenbays.includes(chuyenbay.flyId) && new Date(chuyenbay.departureDay) < today
                );

                
                if (pastDateFlights.length > 0) {
                    alert("Không thể xóa chuyến bay do quá hạn");
                    return;
                }
                try {
                    const response = await axios.delete('https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbay', {
                        data: selectedChuyenbays, // Pass the array as data
                        headers: {
                            'Content-Type': 'application/json',
                        },
                    });

                    if (response.status === 200) {
                        const updatedChuyenbays = chuyenbays.filter(chuyenbay => !selectedChuyenbays.includes(chuyenbay.flyId));

                        // Cập nhật state để tái render bảng
                        setChuyenbays(updatedChuyenbays);

                        // Xóa danh sách khách hàng đã chọn
                        setSelectedChuyenbays([]);
                        toast.success('chuyenbays deleted successfully');

                    } else {
                        toast.error('Failed to delete chuyenbays');
                    }
                } catch (error) {
                    toast.error('Error deleting chuyenbays: ' + error.message);
                }
            }
        }
    };
    const handleKeyPress = (e) => {
        if (e.key === 'Enter') {
            handleSearch();
        }
    };


    const handleSearch = async () => {
        if (searchKeyword != "") {
            try {
                const response = await fetch(`/api/chuyenbay/SearchChuyenbays?searchKeyword=${searchKeyword}`);
                const data = await response.json();
                setChuyenbays(data);
            } catch (error) {
                console.error("Error searching customers:", error);
            }
        }
        else {
            try {
                const response = await fetch("/api/chuyenbay/GetChuyenbays");
                const data = await response.json();
                setChuyenbays(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
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
        <div className="col-md-12 main">
        <div className="mt-md-6">
          <div className="navbar d-flex justify-content-between align-items-center">
            <h2 className="main-name mb-0">Thông tin chuyến bay</h2>
            {/* Actions: Đổi mật khẩu và Xem thêm thông tin */}
            <div className="dropdown">
              <a className="d-flex align-items-center dropdown-toggle" href="#" role="button" id="userDropdown" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i className="bi bi-person-circle" /> 
              </a>
              {/* Dropdown menu */}
              <div className="dropdown-menu" aria-labelledby="userDropdown">
                <a className="dropdown-item" href="password_KhachHang.html">Đổi mật khẩu</a>
                <a className="dropdown-item" href="profile_KhachHang.html">Xem thêm thông tin</a>
              </div>
            </div>
          </div>
          {/*thanh tìm kiếm với bộ lọc*/}
          <div className="find mt-5">
            <div className="d-flex w-100 justify-content-start align-items-center">
              <i className="bi bi-search" />
              <span className="first">
                            <input
                                className="form-control"
                                placeholder="Tìm kiếm ..."
                                value={searchKeyword}
                                onChange={(e) => setSearchKeyword(e.target.value)}
                                onKeyPress={handleKeyPress}
                            />
              </span>
             
            </div>
          </div>
                <table className="table table-bordered">
                    <thead>
                        <tr>
                            <th></th>
                            <th>Mã chuyến bay</th>
                            <th>Giá tiền</th>
                            <th>Điểm đi</th>
                            <th>Điểm đến</th>
                            <th>Giờ đi</th>
                            <th>Ngày đi</th>
                            <th>Thời gian bay</th>
                            <th>Số ghế còn trống</th>
                            <th>Số ghế đã đặt</th>
                            <th>Trung gian</th>
                        </tr>
                    </thead>
                    <tbody>
                        {currentItems.map((item) => (
                            <tr key={item.flyId}>
                                <td>
                                    <input
                                        className="form-check-input"
                                        type="checkbox"
                                        onChange={() => handleCheckboxChange(item.flyId)}
                                        checked={selectedChuyenbays.includes(item.flyId)}
                                    />
                                </td>
                                <td>{item.flyId}</td>
                                <td>{item.originalPrice}</td> 
                                <td>{item.fromLocation}</td> 
                                <td>{item.toLocation}</td> 
                                <td>{item.departureTime}</td> 
                                <td>{item.departureDay}</td> 
                                <td>{item.flightTime}</td> 
                                <td>{item.seatEmpty}</td> 
                                <td>{item.seatBooked}</td> 
                                <td>
                                    {item.trungGian != null ?
                                        item.trungGian.join(', ') :
                                        ""
                                    }
                                </td>

                            </tr>
                        ))}
                    </tbody>
                </table>
          <div className="d-flex justify-content-end my-3">
                    <button className="btn btn-primary mr-2" id="btnThem" onClick={handleClick}>Thêm</button>
                    <button className="btn btn-danger mr-2" id="btnXoa" onClick={handleDelete}>Xóa</button>
                    <button className="btn btn-warning" id="btnSua" onClick={handleShowInfo}>Sửa</button>
          </div>
                <ul className="pagination justify-content-center">
                    <li className="page-item ">
                        <a className="page-link" tabIndex={-1} onClick={() => paginate(currentPage - 1)}>Previous</a>
                    </li>
                    {[...Array(Math.ceil(chuyenbays.length / itemsPerPage)).keys()].map((number) => (
                        <li key={number} className={`page-item ${number + 1 === currentPage ? 'active' : ''}`}>
                            <a className="page-link" onClick={() => paginate(number + 1)}>{number + 1}</a>
                        </li>
                    ))}
                    <li className="page-item">
                        <a className="page-link" onClick={() => paginate(currentPage + 1)}>Next</a>
                    </li>
                </ul>
        </div>
      </div>
      
    );
}
export default ChuyenBay;