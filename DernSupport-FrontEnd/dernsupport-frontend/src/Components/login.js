import React from "react";
import axios from "axios";
import { Button, Form, Input, Card } from "antd";
import { useNavigate } from "react-router-dom";
import { decodeToken } from "react-jwt";
import Swal from "sweetalert2";
import './login.css'; // Import the CSS file

const { Item } = Form;

const onFinishFailed = (errorInfo) => {
  console.log("Failed:", errorInfo);
};

function Login() {
  const navigate = useNavigate();

  const onFinish = async (values) => {
    try {
      const login = await axios.post("https://localhost:7125/api/Users/Login", {
        username: values.username,
        password: values.password,
      });
      const myDecodedToken = decodeToken(login.data.token);

      await localStorage.setItem(
        "role",
        myDecodedToken[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ]
      );
      await localStorage.setItem("account", JSON.stringify(login.data));

      navigate("/");
      window.location.reload();
    } catch (e) {
      Swal.fire({
        icon: "error",
        title: "Invalid Credentials",
        text: "Please check your username or password",
        color: "red",
        width: 400,
      });
    }
  };

  return (
    <div className="login-container">
      <Card className="login-card">
        <h1 className="login-title">Login</h1>

        <Form
          name="basic"
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          autoComplete="off"
          style={{ width: "100%" }} // Ensure form takes full width of the card
        >
          <Item
            label="Username"
            name="username"
            rules={[{ required: true, message: "Please input your username!" }]}
            className="login-form-item"
          >
            <Input className="login-input" />
          </Item>

          <Item
            label="Password"
            name="password"
            rules={[{ required: true, message: "Please input your password!" }]}
            className="login-form-item"
          >
            <Input.Password className="login-password" />
          </Item>

          <Item style={{ marginBottom: "10px" }}>
            <Button type="primary" htmlType="submit" className="login-button">
              Login
            </Button>
          </Item>

          <Item>
            <Button type="link" className="login-link-button" onClick={() => navigate("/register")}>
              Don't have an account yet? Register.
            </Button>
          </Item>
        </Form>
      </Card>
    </div>
  );
}

export default Login;
