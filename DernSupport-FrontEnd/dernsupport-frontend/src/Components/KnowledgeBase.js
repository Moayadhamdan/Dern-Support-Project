import React, { useEffect, useState } from "react";
import { Table, Typography } from "antd";
import axios from "axios";
import Swal from "sweetalert2";
import './KnowledgeBase.css'; // Import the CSS file

const { Column } = Table;
const { Text } = Typography;

function KnowledgeBase() {
  const [knowledgeBaseItems, setKnowledgeBaseItems] = useState([]);
  const [loading, setLoading] = useState(false);

  const account = JSON.parse(localStorage.getItem("account"));

  const headers = {
    Authorization: `Bearer ${account?.token}`,
  };

  const fetchKnowledgeBase = async () => {
    setLoading(true);
    try {
      const { data } = await axios.get(
        "https://localhost:7125/api/KnowledgeBases",
        { headers }
      );
      setKnowledgeBaseItems(data);
    } catch (error) {
      Swal.fire({
        icon: "error",
        title: "Error Fetching Data",
        text: error.response?.data?.message || "Something went wrong!",
        confirmButtonColor: "#d33",
        width: 400,
      });
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchKnowledgeBase();
  }, []);

  return (
    <div className="knowledge-base-container">
      <Typography.Title className="knowledge-base-title" level={3}>
        Knowledge Base
      </Typography.Title>
      <div className="knowledge-base-table">
        <Table
          dataSource={knowledgeBaseItems}
          bordered
          loading={loading}
          rowKey={(record) => record.id}
        >
          <Column
            title={<Text strong>Title</Text>}
            dataIndex="title"
            key="title"
          />
          <Column
            title={<Text strong>Description</Text>}
            dataIndex="description"
            key="description"
          />
          <Column
            title={<Text strong>Category</Text>}
            dataIndex="category"
            key="category"
          />
        </Table>
      </div>
    </div>
  );
}

export default KnowledgeBase;
