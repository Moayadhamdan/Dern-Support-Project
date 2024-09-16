import React, { useState, useEffect } from "react";
import { Button, Form, Input, Modal, List } from "antd";
import axios from "axios";
import Swal from "sweetalert2";
import './SubmitRequests.css'; // Import the CSS file

function SubmitRequests() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [requests, setRequests] = useState([]);
  const [loading, setLoading] = useState(false);

  const showModal = () => {
    setIsModalOpen(true);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const onFinishFailed = (errorInfo) => {
    handleCancel();
  };

  const onFinish = async (values) => {
    try {
      const account = JSON.parse(localStorage.getItem("account"));
      const response = await axios.post(
        "https://localhost:7125/api/Technicians/SubmitRequest",
        {
          technicianTaskId: 0,
          userId: account.id,
          userName: account.username,
          technicianId: 1,
          title: values.title,
          description: values.description,
          status: "pending",
        },
        {
          headers: { Authorization: `Bearer ${account.token}` },
        }
      );

      console.log("submitted", response.data);
      handleCancel();
      window.location.reload();
    } catch (e) {
      console.error(e);
      handleCancel();
    }
  };

  const fetchTechnicianRequests = async () => {
    try {
      setLoading(true);
      const account = JSON.parse(localStorage.getItem("account"));
      const response = await axios.get(
        `https://localhost:7125/api/Technicians/GetTechniciansRequestsByUser?UserId=${account.id}`,
        {
          headers: { Authorization: `Bearer ${account.token}` },
        }
      );
      setRequests(response.data);
      setLoading(false);
    } catch (error) {
      setLoading(false);
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Failed to fetch requests!",
        color: "red",
        width: 400,
      });
    }
  };

  useEffect(() => {
    fetchTechnicianRequests();
  }, []);

  return (
    <div className="submit-requests-container">
      <h2>SUBMIT A REQUEST</h2>
      <Button
        className="ant-btn-primary"
        style={{ width: 300 }}
        type="primary"
        size="large"
        onClick={showModal}
      >
        Add Task
      </Button>
      <Modal
        title="SUBMIT A REQUEST"
        open={isModalOpen}
        footer={[<></>]}
        onCancel={handleCancel}
        className="submit-requests-modal"
      >
        <Form
          id="submitRequestForm"
          name="basic"
          layout="vertical"
          autoComplete="off"
          onFinish={onFinish}
          onFinishFailed={onFinishFailed}
          className="submit-requests-form"
        >
          <Form.Item
            label="Title"
            name="title"
            rules={[{ required: true, message: "Please input a title" }]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Description"
            name="description"
            rules={[{ required: true, message: "Please input a Description" }]}
          >
            <Input.TextArea />
          </Form.Item>
          <Form.Item style={{ alignSelf: "center" }}>
            <Button type="primary" htmlType="submit" className="ant-btn-primary">
              Submit
            </Button>
          </Form.Item>
        </Form>
      </Modal>
      <div></div>
      <div></div>
      <div></div>
      <div></div>
      <h2>MY REQUESTS</h2>
      <List
        bordered
        dataSource={requests}
        loading={loading}
        renderItem={(item) => (
          <List.Item>
            <List.Item.Meta
              title={item.title}
              description={item.description}
            />
            <div>Status: {item.status}</div>
          </List.Item>
        )}
        className="submit-requests-list"
      />
    </div>
  );
}

export default SubmitRequests;
