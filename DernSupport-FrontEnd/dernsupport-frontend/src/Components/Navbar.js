import React from "react";
import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import './Navbar.css'; // Import the CSS file

function Navbar() {
  const [account, setAccount] = useState();
  const [role, setRole] = useState();
  const navigate = useNavigate();

  const logout = async () => {
    localStorage.clear();
    navigate("/login");
    window.location.reload();
  };

  useEffect(() => {
    const account = localStorage.getItem("account");
    setRole(localStorage.getItem("role"));
    setAccount(JSON.parse(account));
  }, []);

  return (
    <nav className="navbar">
      <Link to="/" className="navbar__logo">
        DernSupport
      </Link>

      <div className="navbar__menu">
        <Link to="/" className="navbar__link">
          Home
        </Link>

        {role === "Admin" && (
          <>
            <Link to="/Requests" className="navbar__link">
              Requests
            </Link>
            <Link to="/Stocks" className="navbar__link">
              IT Stocks
            </Link>
            <Link to="/Jobs" className="navbar__link">
              Jobs
            </Link>
          </>
        )}
        
        {role === "User" && (
          <>
            <Link to="/AddRequests" className="navbar__link">
              Requests
            </Link>
            <Link to="/ViewKnowledgeBase" className="navbar__link">
              View KnowledgeBase
            </Link>
            <Link to="/ScheduleRepair" className="navbar__link">
            Schedule Repair
            </Link>
          </>
        )}

        {account ? (
          <>
            <Link to="/login" onClick={logout} className="navbar__logout">
              Logout
            </Link>
            <p className="navbar__account">
            Welcome {account.username}
            </p>
          </>
        ) : (
          <>
            <Link to="/login" className="navbar__link">
              Login
            </Link>
            <Link to="/register" className="navbar__link">
              Register
            </Link>
          </>
        )}
      </div>
    </nav>
  );
}

export default Navbar;
