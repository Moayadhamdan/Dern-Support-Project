import React, { useState } from "react";
import { Button, Form, Input, Card, Select } from "antd";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import Swal from "sweetalert2";
import './Register.css'; // Import the CSS file

const { Option } = Select;
const { Item } = Form;

const onFinishFailed = (errorInfo) => {
  console.log("Failed:", errorInfo);
};

function Register() {
  const [accountType, setAccountType] = useState(null); // Track account type
  const navigate = useNavigate();

  const onFinish = async (values) => {
    try {
      const payload = {
        userName: values.userName,
        email: values.email,
        password: values.password,
        roles: values.roles.split(","),
        accountType: values.accountType,
      };

      // Add extra business fields if account type is Business
      if (values.accountType === "Business") {
        payload.businessName = values.businessName;
        payload.businessLocation = values.businessLocation;
      }

      await axios.post("https://localhost:7125/api/Users/Register", payload);

      Swal.fire({
        icon: "success",
        title: "Registration Successful",
        text: "You can now log in with your new account",
        color: "green",
        width: 400,
      });
      navigate("/login"); // Redirect to login page after registration
    } catch (e) {
      Swal.fire({
        icon: "error",
        title: "Registration Failed",
        text: "Please check your details and try again",
        color: "red",
        width: 400,
      });
    }
  };

  return (
    <div className="register-container">
      <Card className="register-card">
        <h1 className="register-title">Register</h1>

        <Form
          name="basic"
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete="off"
          style={{ width: "100%" }} // Ensure form takes full width of the card
        >
          <Item
            label="Username"
            name="userName"
            rules={[{ required: true, message: "Please input your username!" }]}
            className="register-form-item"
          >
            <Input className="register-input" />
          </Item>

          <Item
            label="Email"
            name="email"
            rules={[{ required: true, message: "Please input your email!" }]}
            className="register-form-item"
          >
            <Input className="register-input" />
          </Item>

          <Item
            label="Password"
            name="password"
            rules={[{ required: true, message: "Please input your password!" }]}
            className="register-form-item"
          >
            <Input.Password className="register-password" />
          </Item>

          <Item
            label="Roles"
            name="roles"
            initialValue="User"
            rules={[{ required: true, message: "Role is required!" }]}
            className="register-form-item"
          >
            <Input value="User" disabled className="register-input" />
          </Item>

          <Item
            label="Account Type"
            name="accountType"
            rules={[{ required: true, message: "Please select an account type!" }]}
            className="register-form-item"
          >
            <Select
              placeholder="Select an account type"
              onChange={(value) => setAccountType(value)}
              className="register-select"
            >
              <Option value="Business">Business</Option>
              <Option value="Individual Customer">Individual Customer</Option>
            </Select>
          </Item>

          {accountType === "Business" && (
            <>
              <Item
                label="Business Name"
                name="businessName"
                rules={[{ required: true, message: "Please input your business name!" }]}
                className="register-form-item"
              >
                <Input placeholder="Enter business name" className="register-input" />
              </Item>

              <Item
                label="Business Location"
                name="businessLocation"
                rules={[{ required: true, message: "Please input your business location!" }]}
                className="register-form-item"
              >
                <Input placeholder="Enter business location" className="register-input" />
              </Item>
            </>
          )}

          <Item style={{ alignSelf: "center" }}>
            <Button type="primary" htmlType="submit" className="register-button">
              Register
            </Button>
          </Item>
        </Form>
      </Card>
    </div>
  );
}

export default Register;
