import React, {useState } from 'react';
import "./SignUp.css";

export default function SignUp() {
    const [email, setEmail] = useState("");
    const [name, setName] = useState("");
    const [position, setPosition] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const handleSave = async () => {
        if (!isValidData()) {
            alert("Invalid email data");
            return;
        }

        if (password != confirmPassword) {
            alert('Mật khẩu không khớp');
            return;
        }

        const accountData = {
            email: email,
            password: password,
            name: name,
            position: 'Khách hàng'

        };
        try {
            const response = await fetch('https://0e59-14-169-3-149.ngrok-free.app/api/account/register', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(accountData),
            });
            if (response.ok) {
                const responseData = await response.json();
                console.log('Response Data:', responseData);

                // Chuyển hướng đến trang đăng nhập
              
            } else {
                // Xử lý lỗi nếu có
                console.error('Registration failed!');
                alert('Registration failed');
            }
        } catch (error) {
            console.error('Error during registration:', error);
            alert('An error occurred during registration');
        }
    };

    const isValidData = () => {
        return (
            email.trim() !== ""
        );
    };
    return (
        <div className="container">
            <div className="text-insertSignUp">
                <h1>Tạo tài khoản mới</h1>
            </div>

            <div className="white-section">

                <div className="inforSignUp">
                    <form className="form-signin1" >
                        <div className="mb-6">
                            <label htmlFor="inputEmail" className="col-form-label">Họ và tên</label>
                            <input
                                type="text"
                                className="form-control"
                                id="name"
                                placeholder=""
                                value={name}
                                onChange={(e) => setName(e.target.value)}
                                required
                            />
                        </div>
                        <div className="mb-6">
                            <label htmlFor="inputEmail" className="col-form-label">Email</label>
                            <input
                                type="email"
                                className="form-control"
                                id="email"
                                placeholder=""
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                required
                            />
                        </div>
                        <div className="mb-6">
                            <label htmlFor="inputPassword" className="col-form-label">Mật khẩu</label>
                            <input
                                type="password"
                                className="form-control"
                                id="password"
                                placeholder=""
                                value={password}
                                onChange={(e) => setPassword(e.target.value)}
                                required
                            />
                        </div>
                        <div className="mb-6">
                            <label htmlFor="inputPassword" className="col-form-label">Xác nhận mật khẩu</label>
                            <input
                                type="password"
                                className="form-control"
                                id="confirmPassword"
                                placeholder=""
                                value={confirmPassword}
                                onChange={(e) => setConfirmPassword(e.target.value)}
                                required
                            />
                        </div>
                        <div className="mb-6">
                            <button type="submit" className="btn btn-primary btn-block" id="btnLogin1" onClick={handleSave}>Tạo tài khoản</button>
                        </div>
                        
                        <div className="mb-3 d-flex justify-content-between">
                            <a href="/sign-in">Đã có tài khoản ?</a>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
}
