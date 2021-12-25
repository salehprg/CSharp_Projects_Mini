import './AdminIndex.css'
import React, { Component } from 'react'
import { Table } from 'reactstrap'
import api from "../../Api/api";
import ReactDOM from 'react-dom';
import { Link, Route } from 'react-router-dom';


export default class AdminIndex extends Component {

    constructor(props) {
        super(props);
        this.state = { MyUsers: [] };
        this.Token = this.props.AuToken;
        this.BaseUrl = this.props.BaseUrl;
        console.log(this.Token);
        if (!this.Token) this.props.history.push("/");

    }

    componentDidMount() {
        
        const config = {
            headers: {
                'Authorization': 'Bearer ' + this.Token}
        }
        api.get("/Admin/Index", config)
          .then(response => {
            const user = response.data;
            this.setState({ MyUsers : user });
            console.log(this.state.MyUsers);
          })
          .catch(error => console.log(error))
    }
    
    render() {
        return (
        <div className="admin-container">
            <div className="card">
                <h5 className="card-header">جدیدترین کاربران</h5>
                <div className="card-body p-0">
                    <div className="table-responsive">
                        <Table className="text-right">
                            <thead className="bg-light">
                                <tr className="border-0">
                                    <th className="border-0">کد کاربر</th>
                                    <th className="border-0">نام کاربری</th>
                                    <th className="border-0">شماره تلفن</th>
                                    <th className="border-0">نام مربی</th>
                                    <th className="border-0">وضعیت</th>
                                    <th className="border-0"></th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    this.state.MyUsers.map((User) => (
                                    <tr key={User.id}>
                                        <td>{User.id}</td>
                                        <td>{User.userName}</td>
                                        <td>{User.phoneNumber ? User.phoneNumber : 'ثبت نشده است'}</td>
                                        <td>{User.coachname ? User.coachname : "ندارد"}</td>
                                            <td><Link className="mr-1" to={{
                                                pathname: "/Admin/UserSetMeal",
                                                id : User.id
                                            }}> برنامه غذایی </Link> </td>
                                        <td><Link className="mr-1" to={{
                                                pathname: "/Admin/UserSetExcercise",
                                                id : User.id
                                            }}> برنامه ورزشی </Link></td>
                                    </tr>
                                    ))
                                }
                            </tbody>
                        </Table>
                    </div>
                </div>
            </div>
                </div>
        )
    }
}
