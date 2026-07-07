import React, { useEffect } from 'react';
import { useAuth } from './AuthContext'; // Import the AuthContext
import { useNavigate } from 'react-router-dom';

const Logout = () => {
  const { logout } = useAuth();
  const navigate = useNavigate();

  useEffect(() => {
    logout(); // Call the logout function
    navigate('/'); // Redirect to home after logout
  }, [logout, navigate]);

  return null; // No UI to render
};

export default Logout;