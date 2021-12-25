import './Settings.css';

import React from 'react';

class Settings extends React.Component {

    render() {
        return (
            <div className="food-container" onClick={this.hideInfo}>
                <div className="food-content">
                    <div className="food-title">
                        <span>نام مربی:</span>
                        <div className="list">
                            <table className="settings-table">
                                <tbody>
                                    <tr>
                                        <td className="info-title">نام:</td>
                                        <td className="info-col">{this.props.userData.userInfo.firstName}</td>
                                    </tr>

                                    <tr>
                                        <td className="info-title">نام خانوادگی:</td>
                                        <td className="info-col">{this.props.userData.userInfo.lastName}</td>
                                    </tr>

                                    <tr>
                                        <td className="info-title">نام کاربری:</td>
                                        <td className="info-col">{this.props.userData.userInfo.userName}</td>
                                    </tr>

                                    <tr>
                                        <td className="info-title">ایمیل:</td>
                                        <td className="info-col">{this.props.userData.userInfo.email}</td>
                                    </tr>

                                    <tr>
                                        <td className="info-title">شماره تلفن:</td>
                                        <td className="info-col">{this.props.userData.userInfo.phoneNumber}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

}

export default Settings;