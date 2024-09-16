import React, { useEffect, useState, useMemo } from "react";
import {
  Button,
  DatePicker,
  Form,
  Modal,
  notification,
  Table,
  Typography,
} from "antd";
import axios from "axios";
import Swal from "sweetalert2";
import jobsBackground from "../img/jobsBackground.jpg"; // Import the background image
import "./Jobs.css"; // Import the CSS file

const Context = React.createContext({
  name: "Default",
});

const { Column } = Table;
const { Text } = Typography;

function Jobs() {
  const [jobs, setJobs] = useState([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [api, contextHolder] = notification.useNotification();

  const openNotification = (placement) => {
    api.info({
      message: "Schedule Successful",
      description: `${isModalOpen?.title} has been Scheduled`,
      placement,
    });
  };

  const contextValue = useMemo(
    () => ({
      name: "Ant Design",
    }),
    []
  );

  const showModal = (job) => {
    setIsModalOpen(job);
  };

  const handleCancel = () => {
    setIsModalOpen(false);
  };

  const onFinishFailed = (errorInfo) => {
    handleCancel();
  };

  const getJobs = async () => {
    const account = JSON.parse(localStorage.getItem("account"));

    try {
      const response = await axios.get(
        "https://localhost:7125/api/Jobs/getAllJobs",
        {
          headers: { Authorization: `Bearer ${account.token}` },
        }
      );
      setJobs(response.data);
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

  const onFinish = async (values) => {
    const account = JSON.parse(localStorage.getItem("account"));
    try {
      await axios.put(
        `https://localhost:7125/api/Jobs/${isModalOpen.jobsId}`,
        {
          title: isModalOpen.title,
          description: isModalOpen.description,
          cost: isModalOpen.cost,
          priority: isModalOpen.priority,
          scheduledDate: values.scheduledDate,
        },
        {
          headers: {
            Authorization: `Bearer ${account.token}`,
            "Content-Type": "application/json",
          },
        }
      );
      getJobs();
      handleCancel();
      openNotification("bottomLeft");
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

  const disablePastDates = (current) => {
    return current && current < new Date().setHours(0, 0, 0, 0);
  };

  useEffect(() => {
    getJobs();
  }, []);

  return (
    <Context.Provider value={contextValue}>
      {contextHolder}

      <div
        className="jobs-container"
        style={{
          backgroundImage: `url(${jobsBackground})`,
        }}
      >
        <Table
          dataSource={jobs}
          bordered
          className="jobs-table"
          rowKey="jobsId"
        >
          <Column title="Title" dataIndex="title" key="title" />
          <Column title="Description" dataIndex="description" key="description" />
          <Column title="Priority" dataIndex="priority" key="priority" align="center" />
          <Column
            title="Cost"
            dataIndex="cost"
            key="cost"
            align="center"
            render={(text) => <Text>{text}</Text>}
          />
          <Column
            title="Scheduled Date"
            dataIndex="scheduledDate"
            key="scheduledDate"
            render={(_, record) => {
              const date = new Date(record.scheduledDate);
              const formattedDate = date.toLocaleDateString("en-GB", {
                day: "2-digit",
                month: "long",
                year: "numeric",
              });

              return <Text>{record.scheduledDate ? formattedDate : "Not Scheduled"}</Text>;
            }}
          />
          <Column
            title="Action"
            key="action"
            align="center"
            render={(_, record) => (
              <Button type="default" onClick={() => showModal(record)}>
                Schedule
              </Button>
            )}
          />
        </Table>

        <Modal
          title="Schedule"
          open={isModalOpen}
          onCancel={handleCancel}
          footer={null}
        >
          <Form
            id="submitRequestForm"
            name="basic"
            layout="vertical"
            autoComplete="off"
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            style={{
              display: "flex",
              flexDirection: "column",
              justifyContent: "center",
              marginTop: 30,
            }}
          >
            <Form.Item
              label="Schedule Job"
              name="scheduledDate"
              rules={[
                { required: true, message: "Please select a date" },
              ]}
            >
              <DatePicker disabledDate={disablePastDates} />
            </Form.Item>

            <Form.Item style={{ alignSelf: "center" }}>
              <Button type="primary" htmlType="submit">
                Submit
              </Button>
            </Form.Item>
          </Form>
        </Modal>
      </div>
    </Context.Provider>
  );
}

export default Jobs;
