// src/Components/NotFound.js
import React from 'react';
import './NotFound.css'; 
import logo from '../NotFound/Image1.png';
//import logo1 from '../NotFound.jpeg';

const NotFound = () => {
    return (
        <div className="not-found-container">
            <img src={logo} alt="Not Found" />
            {/* <h1>404 - Page Not Found</h1> */}
            <p>Sorry, the page you are looking for does not exist.</p>
         
            {/* <img src={logo1} alt="Not Found" />
            <h1>404 - Page Not Found</h1>
            <p>Sorry, the page you are looking for does not exist.</p> */}
        </div>
    );
};

export default NotFound;