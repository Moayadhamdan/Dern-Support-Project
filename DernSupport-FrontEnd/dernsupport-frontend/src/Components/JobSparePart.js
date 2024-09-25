import React, { useEffect, useState, useMemo } from "react";
import {
  Button,
  Form,
  Input,
  InputNumber,
  Modal,
  notification,
  Table,
} from "antd";
import axios from "axios";
import Swal from "sweetalert2";
import ITStocks from "../img/ITStocks.jpg"; // Import the background image
import './JobSparePart.css'; // Import the CSS file

const { Column } = Table;
const Context = React.createContext({
  name: "Default",
});

function JobSparePart() {
  const [stocks, setStocks] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [currentRecord, setCurrentRecord] = useState(null); // Store the current record to edit
  const [form] = Form.useForm(); // Create form instance

  // Notification
  const [api, contextHolder] = notification.useNotification();
  const openNotification = (placement) => {
    api.info({
      message: "Update Successful",
      description: `Stock ${currentRecord?.name} Updated`,
      placement,
    });
  };

  const contextValue = useMemo(() => ({
    name: "Ant Design",
  }), []);

  const showModal = (record) => {
    setCurrentRecord(record); // Set the record to be edited
    setIsModalOpen(true); // Open the modal
    // Set form fields with current record values
    form.setFieldsValue({
      name: record.name,
      category: record.category,
      description: record.description,
      quantityInStock: record.quantityInStock,
    });
  };

  const handleCancel = () => {
    setIsModalOpen(false);
    form.resetFields(); // Reset the form fields when modal is closed
  };

  const onFinishFailed = () => {
    handleCancel();
  };

  // Fetch stocks data
  const getStocks = async () => {
    const account = JSON.parse(localStorage.getItem("account"));
    try {
      const response = await axios.get(
        "http://dernsupport.runasp.net/api/JobSpareParts/allStocks",
        {
          headers: { Authorization: `Bearer ${account.token}` },
        }
      );
      setStocks(response.data);
    } catch (e) {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Something went wrong!",
        color: "red",
        width: 400,
      });
    }
  };

  // Update stock
  const onFormFinish = async (values) => {
    const account = JSON.parse(localStorage.getItem("account"));
    try {
      await axios.put(
        `http://dernsupport.runasp.net/api/JobSpareParts/${currentRecord.jobSparePartId}`,
        values, // Submit updated values
        {
          headers: {
            Authorization: `Bearer ${account.token}`,
            "Content-Type": "application/json",
          },
        }
      );
      getStocks(); // Refresh stock list
      handleCancel(); // Close modal
      openNotification("bottomRight"); // Show notification
    } catch (e) {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Something went wrong!",
        color: "red",
        width: 400,
      });
    }
  };

  // Search for stocks
  const onSearch = async (value) => {
    const account = JSON.parse(localStorage.getItem("account"));
    try {
      const response = await axios.get(
        `http://dernsupport.runasp.net/api/JobSpareParts/search?name=${value}`,
        {
          headers: { Authorization: `Bearer ${account.token}` },
        }
      );
      setStocks(response.data);
    } catch (e) {
      Swal.fire({
        icon: "error",
        title: "Oops...",
        text: "Item doesn't exist!",
        color: "red",
        width: 400,
      });
      getStocks();
    }
  };

  useEffect(() => {
    getStocks();
  }, []);

  return (
    <Context.Provider value={contextValue}>
      {contextHolder}
      <div
        className="job-sparepart-container"
        style={{
          backgroundImage: `url(${ITStocks})`, // Set the background image
        }}
      >
        <div className="job-sparepart-content">
          <Input.Search
            style={{ width: "100%", marginBottom: "20px" }}
            placeholder="Search"
            onChange={(e) => onSearch(e.target.value)}
          />
          <Table
            dataSource={stocks}
            bordered
            className="job-sparepart-table"
            rowKey="jobSparePartId"
          >
            <Column title="Name" dataIndex="name" key="name" />
            <Column title="Category" dataIndex="category" key="category" />
            <Column title="Description" dataIndex="description" key="description" />
            <Column
              title="Quantity In Stock"
              dataIndex="quantityInStock"
              key="quantityInStock"
              align="center"
            />
            <Column
              title="Action"
              key="action"
              align="center"
              render={(_, record) => (
                <Button type="default" onClick={() => showModal(record)}>
                  Edit
                </Button>
              )}
            />
          </Table>
          <Modal
            title="Edit Stock"
            open={isModalOpen}
            onCancel={handleCancel}
            footer={null}
          >
            <Form
              form={form}
              name="basic"
              layout="vertical"
              autoComplete="off"
              onFinish={onFormFinish}
              onFinishFailed={onFinishFailed}
            >
              <Form.Item label="Name" name="name">
                <Input />
              </Form.Item>
              <Form.Item label="Category" name="category">
                <Input />
              </Form.Item>
              <Form.Item label="Description" name="description">
                <Input.TextArea />
              </Form.Item>
              <Form.Item label="Quantity In Stock" name="quantityInStock">
                <InputNumber />
              </Form.Item>
              <Form.Item style={{ alignSelf: "center" }}>
                <Button type="primary" htmlType="submit">
                  Submit
                </Button>
              </Form.Item>
            </Form>
          </Modal>
        </div>
      </div>
    </Context.Provider>
  );
}

export default JobSparePart;
