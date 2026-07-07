import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './Register.css';

const Register = () => {
    const [fname, setfName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [phonenumber, setPhoneNumber] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [error, setError] = useState('');
    const [location, setLocation] = useState('');
    const [role, setRole] = useState('');
    const [successMessage, setSuccessMessage] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();

    const validatePassword = (password) => {
        const regex = /^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*]).{8,}$/;
        return regex.test(password);
    };

    const handleRegister = async (e) => {
        e.preventDefault();

        if (!validatePassword(password)) {
            setError('Password must be at least 8 characters long, contain at least one uppercase letter, one number, and one special character.');
            return;
        }

        if (password !== confirmPassword) {
            setError('Passwords do not match.');
            return;
        }

        if (!fname || !email || !location || !password || !phonenumber || !role) {
            setError('Please fill in all fields.');
            return;
        }

        const registrationData = {
            Name: fname,
            email,
            location,
            password,
            phoneNo: phonenumber,
            userRole: role,
        };

        try {
            setIsLoading(true);
            const response = await fetch('https://localhost:44395/api/users/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(registrationData),
            });

            if (!response.ok) {
                const errorResponse = await response.text();
                console.log('Error response from server:', errorResponse);
                setError(`Registration failed: ${errorResponse}`);
            } else {
                setError('');
                setSuccessMessage('Registration successful! Redirecting to login...');
                console.log('Registration successful:', await response.json());

                setTimeout(() => {
                    navigate('/login');
                }, 2000);
            }
        } catch (error) {
            setError('An error occurred during registration. Please try again.');
            console.error('Registration error:', error);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <div className="register-container">
            <h2 className="register-title">Register for Our Great Service</h2>
            {error && <p className="error-message">{error}</p>}
            {successMessage && <p className="success-message">{successMessage}</p>}
            <form className="register-form" onSubmit={handleRegister}>
                <div className="form-group">
                    <label htmlFor="fname">Full Name</label>
                    <input
                        type="text"
                        id="fname"
                        value={fname}
                        onChange={(e) => setfName(e.target.value)}
                        required
                        placeholder="Enter Your Full Name"
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="email">Email</label>
                    <input
                        type="email"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                        placeholder="Enter Your Email"
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="location">Location</label>
                    <input
                        type="text"
                        id="location"
                        value={location}
                        onChange={(e) => setLocation(e.target.value)}
                        required
                        placeholder="Enter Your Location"
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="role">What do you want to do?</label>
                    <select id="role" value={role} onChange={(e) => setRole(e.target.value)} required>
                        <option value="" disabled>Select your role</option>
                        <option value="both">Both</option>
                        <option value="donor">Donor</option>
                        <option value="adopter">Adopter</option>
                    </select>
                </div>

                <div className="form-group">
                    <label htmlFor="phonenumber">Phone Number</label>
                    <input
                        type="tel"
                        id="phonenumber"
                        value={phonenumber}
                        onChange={(e) => setPhoneNumber(e.target.value)}
                        required
                        placeholder="Enter Your Contact Number"
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="password">Password</label>
                    <input
                        type="password"
                        id="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                        placeholder="Enter Your Password"
                    />
                </div>
                <div className="form-group">
                    <label htmlFor="confirm-password">Confirm Password</label>
                    <input
                        type="password"
                        id="confirm-password"
                        value={confirmPassword}
                        onChange={(e) => setConfirmPassword(e.target.value)}
                        required
                        placeholder="Confirm Your Password"
                    />
                </div>
                <button type="submit" className="register-button" disabled={isLoading}>
                    {isLoading ? 'Registering...' : 'Register'}
                </button>
            </form>
            <p className="register-footer">
                Already have an account? <a href="/login">Log in</a>
            </p>
        </div>
    );
};

export default Register;
