import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import PreLoginNavBar from './Component/PreLoginNavBar/PreLoginNavBar'; // Adjust the path as necessary
import PostLoginNavBar from './Component/PostLoginNavBar/PostLoginNavBar'; // Adjust the path as necessary
import Home from './Component/Home/Home'; // Home component
import Login from './Component/Register-Login/Login'; // Login component
import Register from './Component/Register-Login/Register'; // Register component
import Adopt from './Component/Adopt/Adopt'; // Adopt component
import Donate from './Component/Donate/Donate'; // Donate component
import AboutUs from './Component/AboutUs/AboutUs'; // About Us component
import Blog from './Component/Blog/Blog'; // Blog component
import FAQ from './Component/FAQ/FAQ'; // FAQ component
import Terms from './Component/Terms/Terms'; // Terms of Service component
import Footer from './Component/Footer/Footer'; // Footer component
import NotFound from './Component/NotFound/NotFound'; // 404 Not Found component
import UploadImage from './Component/UploadImage/uploadImage'; // Upload Image component
import './App.css'; 

const App = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const handleLogin = () => {
    setIsLoggedIn(true);
  };

  const handleLogout = () => {
    setIsLoggedIn(false);
    localStorage.removeItem('authToken'); // Remove token from local storage
    window.location.href = '/'; // Redirect to home page
  };

  return (
    <Router> 
      <div className="app-container"> 
        {isLoggedIn ? (
          <PostLoginNavBar onLogout={handleLogout} />
        ) : (
          <PreLoginNavBar />
        )}
        
        <Routes>
          <Route path="/" exact element={<Home />} /> 
          <Route path="/adopt" exact element={<Adopt />} />
          <Route path="/donate" exact element={<Donate />} />
          <Route path="/upload-images/:petId" exact element={<UploadImage />} />           
          <Route path="/aboutUs" exact element={<AboutUs />} /> 
          <Route path="/blog" exact element={<Blog />} />
          <Route path="/login" exact element={<Login onLogin={handleLogin} />} />
          <Route path="/register" exact element={<Register />} />
          <Route path="/faq" exact element={<FAQ />} />
          <Route path="/terms" exact element={<Terms />} />
          <Route path="*" exact element={<NotFound />} /> 
        </Routes>
      </div>
      {/* Uncomment the Footer if needed */}
      {/* <Footer/> */}
    </Router>
  );
}

export default App;


App.jsx