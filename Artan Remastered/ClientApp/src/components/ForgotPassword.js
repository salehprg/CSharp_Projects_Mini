import React from 'react';
import queryString from 'query-string';
import toastr from 'toastr';
import api from "../Api/api";
import { Link } from 'react-router-dom';

class ForgotPassword extends React.Component {

    state = { userId: null, token: null, newPassword1: null, newPassword2: null, whatShow: 'content' };

    componentDidMount() {
        const value = queryString.parse(this.props.location.search);
        
        try {
            var finalToken = value.Token.replace(/ /g, '+');
        } catch (e) {
            this.props.history.push('/');
        }

        this.setState({ userId: value.UserId, token: finalToken });

    }

    changePass = async (event) => {

        event.preventDefault();

        if ( this.state.newPassword1 !== this.state.newPassword2 ) {
            toastr['error']('رمز ها مطابقت ندارند');
        } else {

            const params = {
                "UserId" : this.state.userId,
                "Token" : this.state.token,
                "NewPass" : this.state.newPassword1
            };            

            try {

                const response = await api.post('https://hamidgolestani.ir/api/Home/ChangePassword', params, {
                    headers: {
                        'content-type' : 'application/json'
                    }
                });

                this.setState({ whatShow: 'success' });

            } catch (e) {
                toastr['error']('خطا');
            }

        }

    }

    conRender() {
        if (this.state.whatShow === 'content') {
            return (
                <div className="login-box">
                <i className="material-icons clear" onClick={this.onClearClick}>clear</i><br />
                <i className="material-icons login-icon">vpn_key</i>
            
                <form onSubmit={this.changePass}>
                    <input 
                        className="inputs" 
                        type="text" 
                        name="pass" 
                        placeholder="رمز جدید" 
                        value= {this.state.newPassword1}
                        onChange= { e => this.setState({ newPassword1: e.target.value }) }
                        required
                    />
                    <input 
                        className="inputs" 
                        type="text" 
                        name="pass_again" 
                        placeholder="تکرار رمز جدید"
                        value= {this.state.newPassword2}
                        onChange= { e => this.setState({ newPassword2: e.target.value }) }
                        required
                    />

                    <input className="submit-btn" type="submit" value="تایید" />
                </form>
            </div>
            );
        } else {
            return (
                <div className="reset-content">
                    <p>رمز با موفقیت تغییر یافت</p>
                    <Link to="/login">ورود</Link>
                </div>
            );
        }
    }

    render() {
        return <div> {this.conRender()} </div> 
    }

}

export default ForgotPassword;