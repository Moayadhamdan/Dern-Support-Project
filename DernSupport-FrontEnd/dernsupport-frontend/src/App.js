import React from "react";
import "./App.css";
import { BrowserRouter as Router, Route, Routes, useNavigate } from "react-router-dom";
import Login from "./Components/login";
import Register from "./Components/Register";
import { useEffect, useState } from "react";
import Navbar from "./Components/Navbar";
import SubmitRequests from "./Components/SubmitRequests";
import TaskRequests from "./Components/TaskRequests";
import JobSparePart from "./Components/JobSparePart";
import Jobs from "./Components/Jobs";
import KnowledgeBase from "./Components/KnowledgeBase";
import Footer from "./Components/Footer";
import Homeimg from './img/Homeimg.png';
import { Button } from "antd";

function Home() {
  const navigate = useNavigate();
  return (
    <div className="home-background">
      <h1 className="home-title">Welcome to Dern-Support</h1>
      <p className="home-description">
        Dern-Support is a small IT technical support company that specializes in
        repairing computer systems for businesses and individual customers. Our
        services range from troubleshooting and diagnostics to full system
        repairs, ensuring that your technology works smoothly and efficiently.
      </p>
      <p className="home-description">
        We pride ourselves on delivering quick, reliable, and professional
        support to help keep your business or home office running at peak
        performance. Whether you need routine maintenance or emergency repairs,
        our team is here to assist.
      </p>
      <Button className="home-button" type="primary" size="large" onClick={() => navigate("/login")}>
        Get Started
      </Button>
    </div>
  );
}

function App() {
  const [isLoggedIn, setLoggedIn] = useState(false);
  const [role, setRole] = useState();

  useEffect(() => {
    setLoggedIn(localStorage.getItem("account"));
    setRole(localStorage.getItem("role"));
  }, []);

  return (
    <div>
      <Router>
        <Navbar />
        <Routes>
          <Route path="/" element={<Home />} />

          {isLoggedIn ? (
            <>
              {role === "User" ? (
                <>
                  <Route path="/AddRequests" element={<SubmitRequests />} />
                  <Route path="/ViewKnowledgeBase" element={<KnowledgeBase />} />
                  <Route path="/ScheduleRepair" element={<Jobs />} />
                </>
              ) : null}

              {role === "Admin" ? (
                <>
                  <Route path="/Requests" element={<TaskRequests />} />
                  <Route path="/Stocks" element={<JobSparePart />} />
                  <Route path="/Jobs" element={<Jobs />} />
                </>
              ) : null}
            </>
          ) : (
            <>
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
            </>
          )}
        </Routes>
        <Footer />
      </Router>
    </div>
  );
}

export default App;
