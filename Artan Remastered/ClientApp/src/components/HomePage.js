import './HomePage.css';
import React from 'react';

class HomePage extends React.Component {

    onLoginClick = () => this.props.history.push("/login");

    onSignupClick = () => this.props.history.push("/signup");

    render() {
        return (
            <div className="container-fluid hero-full">
                <div className="content">
                    <h1>باشگاه ورزشی آرتان</h1>

                    <div className="row">
                        <div className="col-md-6 order-md-2">
                            <button className="mybtn mybtn-login" onClick={this.onLoginClick}>ورود</button>
                        </div>

                        <div className="col-md-6 order-md-1">
                            <button className="mybtn mybtn-signup" onClick={this.onSignupClick}>ثبت نام</button>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

}

export default HomePage;