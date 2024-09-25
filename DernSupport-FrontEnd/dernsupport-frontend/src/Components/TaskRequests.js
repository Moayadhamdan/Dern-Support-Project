import React, { useEffect, useState } from 'react';
import { Button, Space, Table } from 'antd';
import axios from 'axios';
import Regester from "../img/Request.jpg";
import './TaskRequests.css'; // Import the CSS file

const { Column } = Table;

const TaskRequests = () => {
    const [requests, setRequests] = useState([]);
    const [loading, setLoading] = useState(true);

    const getRequests = async () => {
        const account = JSON.parse(localStorage.getItem('account'));
        try {
            const response = await axios.get("http://dernsupport.runasp.net/api/Technicians/GetTechniciansRequests", {
                headers: { Authorization: `Bearer ${account.token}` },
            });

            setRequests(response.data.tasks);
        } catch (e) {
            console.log(e);
        } finally {
            setLoading(false);
        }
    };

    const updateStatus = async (technicianTaskId, newStatus) => {
        const account = JSON.parse(localStorage.getItem('account'));
        try {
            await axios.put(`http://dernsupport.runasp.net/api/Technicians/UpdateRequestStatus/${technicianTaskId}`, `"${newStatus}"`, {
                headers: { Authorization: `Bearer ${account.token}`, 'Content-Type': 'application/json' },
            });
            getRequests();
        } catch (e) {
            console.log(e);
        }
    };

    useEffect(() => {
        getRequests();
    }, []);

    return (
        <div className="task-requests-container" style={{ backgroundImage: `url(${Regester})` }}>
            <Table dataSource={requests} bordered loading={loading} rowKey="technicianTaskId" className="table-wrapper">
                <Column title="Title" dataIndex="title" key="title" />
                <Column title="Description" dataIndex="description" key="description" />
                <Column title="Status" dataIndex="status" key="status" />
                <Column title="UserName" dataIndex="userName" key="userName" />
                <Column
                    title="Action"
                    key="action"
                    render={(_, record) => (
                        <Space size="middle">
                            {record.status !== 'approved' ? (
                                <Button type="primary" onClick={() => updateStatus(record.technicianTaskId, 'approved')}>
                                    Approve
                                </Button>
                            ) : null}
                            {record.status !== 'declined' ? (
                                <Button type="primary" danger onClick={() => updateStatus(record.technicianTaskId, 'declined')}>
                                    Decline
                                </Button>
                            ) : null}
                        </Space>
                    )}
                />
            </Table>
        </div>
    );
};

export default TaskRequests;
