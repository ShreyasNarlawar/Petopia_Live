import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import './NavBar.css'; // Custom CSS for styling
import logo from '../../assets/petopia_logo.png'; // Replace with your logo path

const PostLoginNavBar = ({ onLogout }) => {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
  };

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-dark shadow-sm">
      <div className="container-fluid">
        <Link className="navbar-brand" to="/">
          <img src={logo} alt="Petopia Logo" className="logo" />
        </Link>
        <button
          className="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarNav"
          aria-controls="navbarNav"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span className="navbar-toggler-icon"></span>
        </button>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav me-auto">
            <li className="nav-item">
              <Link className="nav-link hover-effect" to="/">
                Home
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link hover-effect" to="/adopt">
                Adopt
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link hover-effect" to="/donate">
                Donate
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link hover-effect" to="/aboutUs">
                About Us
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link hover-effect" to="/blog">
                Blog
              </Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link hover-effect" to="/faq">
                FAQ
              </Link>
            </li>
            <li className="nav-item dropdown">
              <Link
                className={`nav-link dropdown-toggle hover-effect ${isDropdownOpen ? 'active' : ''}`}
                to="#"
                onClick={toggleDropdown}
                aria-expanded={isDropdownOpen}
              >
                More
              </Link>
              <ul className={`dropdown-menu ${isDropdownOpen ? 'show dropdown-animate' : ''}`}>
                <li>
                  <Link className="dropdown-item" to="/terms">
                    Terms of Service
                  </Link>
                </li>
              </ul>
            </li>
          </ul>
          <ul className="navbar-nav ms-auto">
            <li className="nav-item">
              <button
                className="btn btn-outline-danger rounded-pill me-2"
                onClick={onLogout}
              >
                Logout
              </button>
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default PostLoginNavBar;

