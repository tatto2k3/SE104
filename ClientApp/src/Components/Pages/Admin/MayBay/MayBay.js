import React from "react";
import './MayBay.css';
import { useEffect, useState } from "react";
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';
import 'bootstrap/dist/css/bootstrap.min.css';

const MayBay = () => {
    const [searchKeyword, setSearchKeyword] = useState('');
    const [planes, setPlanes] = useState([]);
    const [selectedPlanes, setSelectedPlanes] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage] = useState(8);
    const navigate = useNavigate();

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = planes.slice(indexOfFirstItem, indexOfLastItem);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const handleClick = () => {
        navigate('/MayBay_Them');
    };

    useEffect(() => {
        fetch("api/plane/GetPlanes")
            .then(response => response.json())
            .then(responseJson => {
                console.log(responseJson);
                setPlanes(responseJson);
            })
            .catch(error => {
                console.error("Error fetching data:", error);
            });
    }, [])




    useEffect(() => {
        // Lấy danh sách khách hàng từ API hoặc nguồn dữ liệu khác
        const fetchData = async () => {
            try {
                const response = await fetch("/api/plane/GetPlanes");
                const data = await response.json();
                setPlanes(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);


    const handleCheckboxChange = (planeId) => {
        if (selectedPlanes.includes(planeId)) {
            // Nếu đã chọn thì hủy chọn
            setSelectedPlanes([]);
        } else {
            // Nếu chưa chọn thì chọn
            setSelectedPlanes([planeId]);
        }
    };



    const handleShowInfo = async () => {
        try {
            if (selectedPlanes.length > 0) {
                const response = await fetch(`/api/plane/GetPlaneDetails?plIds=${selectedPlanes.join(',')}`);
                const data = await response.json();

                // Chuyển hướng sang trang sửa khách hàng và truyền thông tin khách hàng
                navigate('/MayBay_Sua', { state: { selectedPlaneInfo: data } });
            } else {
                console.log("No planes selected.");
            }
        } catch (error) {
            console.error("Error fetching plane details:", error);
        }
    };

    const handleDelete = async () => {
        if (window.confirm("Are you sure to delete this plane")) {
            try {
                const response = await axios.delete('http://localhost:44430/api/plane', {
                    data: selectedPlanes, // Pass the array as data
                    headers: {
                        'Content-Type': 'application/json',
                    },
                });

                if (response.status === 200) {
                    const updatedplanes = planes.filter(plane => !selectedPlanes.includes(plane.plId));

                    // Cập nhật state để tái render bảng
                    setPlanes(updatedplanes);

                    // Xóa danh sách khách hàng đã chọn
                    setSelectedPlanes([]);
                    toast.success('Planes deleted successfully');

                } else {
                    toast.error('Failed to delete planes');
                }
            } catch (error) {
                toast.error('Error deleting planes: ' + error.message);
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
                const response = await fetch(`/api/plane/SearchPlanes?searchKeyword=${searchKeyword}`);
                const data = await response.json();
                setPlanes(data);
            } catch (error) {
                console.error("Error searching customers:", error);
            }
        }
        else {
            try {
                const response = await fetch("/api/plane/GetPlanes");
                const data = await response.json();
                setPlanes(data);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        }
    };
    return (
        <div className="col-md-12 main">
  <div className="mt-md-6">
    <div className="navbar d-flex justify-content-between align-items-center">
      <h2 className="main-name mb-0">Thông tin máy bay</h2>
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
        <span className="second">Filters <i className="bi bi-chevron-compact-down" /></span>
      </div>
    </div>
    <table className="table table-bordered">
      <thead>
        <tr>
          <th />
          <th>Mã máy bay</th>
          <th>Loại máy bay</th>
          <th>Sức chứa khoang thương gia</th>
          <th>Sức chứa khoang thường</th>
        </tr>
      </thead>
      <tbody>
        
                {currentItems.map((item) => (
                    <tr key={item.plId}>
                        <td contentEditable="true" className="choose">
                            <input
                                className="form-check-input"
                                type="checkbox"
                                onChange={() => handleCheckboxChange(item.plId)}
                                checked={selectedPlanes.includes(item.plId)}
                            />
                        </td>
                        <td>{item.plId}</td>
                        <td>{item.typeofplane}</td>
                        <td>{item.businessCapacity}</td>
                        <td>{item.economyCapacity}</td>
                    </tr>
                ))}
                            
      
        
      </tbody>
    </table>
    {/*3 nut bam*/}
    <div className="d-flex justify-content-end my-3">
                    <button className="btn btn-primary mr-2" id="btnThem" onClick={handleClick}>Thêm</button>
                    <button className="btn btn-danger mr-2" id="btnXoa" onClick={handleDelete}>Xóa</button>
                    <button className="btn btn-warning" id="btnSua" onClick={handleShowInfo}>Sửa</button>
    </div>
                <ul className="pagination justify-content-center">
                    <li className="page-item ">
                        <a className="page-link" tabIndex={-1} onClick={() => paginate(currentPage - 1)}>Previous</a>
                    </li>
                    {[...Array(Math.ceil(planes.length / itemsPerPage)).keys()].map((number) => (
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
export default MayBay;