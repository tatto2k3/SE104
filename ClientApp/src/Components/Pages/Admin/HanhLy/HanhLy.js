import React, { useEffect, useState } from "react";
import './HanhLy.css';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { toast } from 'react-toastify';
import 'bootstrap/dist/css/bootstrap.min.css';

const axiosInstance = axios.create({
    baseURL: 'https://2b0c-113-161-73-175.ngrok-free.app/api/', // điều chỉnh URL nếu cần thiết
    headers: {
        'Content-Type': 'application/json',
    },
});

const HanhLy = () => {
    const [searchKeyword, setSearchKeyword] = useState('');
    const [luggages, setLuggages] = useState([]);
    const [selectedLuggages, setSelectedLuggages] = useState([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [itemsPerPage] = useState(8);
    const navigate = useNavigate();

    const indexOfLastItem = currentPage * itemsPerPage;
    const indexOfFirstItem = indexOfLastItem - itemsPerPage;
    const currentItems = luggages.slice(indexOfFirstItem, indexOfLastItem);

    const paginate = pageNumber => setCurrentPage(pageNumber);

    const handleClick = () => {
        navigate('/SanBayTrungGian_Them');
    };

    useEffect(() => {
        fetch("https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbaysanbay/GetChuyenbay_Sanbays")
            .then(response => response.json())
            .then(responseJson => {
                console.log(responseJson);
                console.log(selectedLuggages);
                setLuggages(responseJson);
            })
            .catch(error => {
                console.error("Error fetching data:", error);
            });
    }, [selectedLuggages])




    const handleCheckboxChange = (luggageId, airportId) => {
        const isChecked = selectedLuggages.some(selected => selected.flyId === luggageId && selected.airportId === airportId);

        if (isChecked) {
            // Nếu checkbox đã được chọn, hủy chọn nó
            setSelectedLuggages(prevSelected => prevSelected.filter(selected => !(selected.flyId === luggageId && selected.airportId === airportId)));
        } else {
            // Nếu checkbox chưa được chọn, hủy chọn tất cả các checkbox khác và chọn checkbox mới
            setSelectedLuggages([{ flyId: luggageId, airportId: airportId }]);
        }
    };


    const handleShowInfo = async () => {
        try {
            if (selectedLuggages.length === 0) {
                console.log("Không có hành lý nào được chọn.");
                return;
            }

            // Đảm bảo selectedLuggage là một object
            if (!selectedLuggages[0] || typeof selectedLuggages[0] !== 'object') {
                console.error("Dữ liệu hành lý không hợp lệ. Cần phải là object.");
                return;
            }

            const selectedLuggage = selectedLuggages[0];
            console.log('selectedLuggage: ', selectedLuggage);

            const { FlyId, AirportId } = selectedLuggage; // Giả sử FlyId và AirportId là các thuộc tính cần thiết

            const response = await axiosInstance.get(`https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbaysanbay/GetLuggageDetails?FlyId=${selectedLuggage.flyId}&AirportId=${selectedLuggage.airportId}`);

            const data = response.data;

            navigate('/SanBayTrungGian_Sua', { state: { selectedLuggageInfo: data } });

        } catch (error) {
            console.error("Lỗi khi lấy thông tin hành lý:", error);
        }
    };


    const handleDelete = async () => {
        if (window.confirm("Bạn có đồng ý xóa sân bay trung gian?")) {
            try {
                
                const selectedLuggage = selectedLuggages[0];

                const response = await axios.delete(`https://2b0c-113-161-73-175.ngrok-free.app/api/chuyenbaysanbay/DeleteLuggage`, {
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    data: selectedLuggage, 
                });

                if (response.status === 200) {
                    // Lọc ra các hành lý còn lại sau khi xóa
                    const updatedLuggages = luggages.filter(luggage => luggage.flyId !== selectedLuggage.flyId || luggage.airportId !== selectedLuggage.airportId);

                    // Cập nhật state để tái render bảng
                    setLuggages(updatedLuggages);

                    // Xóa danh sách hành lý đã chọn
                    setSelectedLuggages([]);
                    toast.success('Luggage deleted successfully');
                } else {
                    toast.error('Failed to delete Luggage');
                }
            } catch (error) {
                toast.error('Error deleting Luggage: ' + error.message);
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
                const response = await fetch(`/api/luggage/SearchLuggages?searchKeyword=${searchKeyword}`);
                const data = await response.json();
                setLuggages(data);
            } catch (error) {
                console.error("Error searching customers:", error);
            }
        }
        else {
            try {
                const response = await fetch("/api/luggage/GetLuggages");
                const data = await response.json();
                setLuggages(data);
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
        <div className=" mt-md-6">
          <div className="navbar d-flex justify-content-between align-items-center">
            <h2 className="main-name mb-0">Sân bay trung gian</h2>
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
                <th />
                <th>Mã chuyến bay</th>
                <th>Mã sân bay</th>
                <th>Thời gian dừng</th>
                <th>Ghi chú</th>
              </tr>
            </thead>
                    <tbody>
                        {currentItems.map((item) => (
                            <tr key={item.flyId}>
                                <td contentEditable="true" className="choose">
                                    <input
                                        className="form-check-input"
                                        type="checkbox"
                                        onChange={() => handleCheckboxChange(item.flyId, item.airportId)} // Truyền cả flyId và airportId
                                        checked={selectedLuggages.some(selected => selected.flyId === item.flyId && selected.airportId === item.airportId)} // Kiểm tra cả hai thuộc tính
                                    />
                                </td>
                                <td>{item.flyId}</td>
                                <td>{item.airportId}</td>
                                <td>{item.time}</td>
                                <td>{item.note}</td>
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
                    {[...Array(Math.ceil(luggages.length / itemsPerPage)).keys()].map((number) => (
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
export default HanhLy;