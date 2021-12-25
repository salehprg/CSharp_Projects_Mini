import './Forget.css';
import React from 'react';
import api from "../Api/api";
import toastr from 'toastr';
import { Link } from 'react-router-dom';

class Forget extends React.Component {

    state = { email: '', showWhat: 'content' };

    onSubmitClicked = (event) => {

        event.preventDefault();
        this.checkEmail();

    }

    checkEmail = async () => {

        try {
            const response = await api.post(`https://hamidgolestani.ir/api/Home/ForgotPassword?EmailAddr=${this.state.email}`);            
            this.setState({ showWhat: '' });
        } catch (e) {
            toastr['error']('ایمیل در سیستم وجود ندارد');
            return;
        }

    }

    conRender() {
        if (this.state.showWhat === 'content') {
            return (
                <div className="reset-content">
                    <i className="material-icons lock-icon">lock</i>
                    <p>رمز عبور خود را فراموش کرده اید؟</p>
                    <form onSubmit={this.onSubmitClicked}>
                        <input 
                        className="r-input" 
                        type="email" 
                        onChange={ e => this.setState({ email: e.target.value }) }
                        value={this.state.email}
                        placeholder="ایمیل"
                        required
                        />

                        <br />
                        
                        <input className="r-btn" type="submit" value="تایید" />
                    </form>
                </div>
            );
        } else {
            return (
                <div className="reset-container">
                <div className="reset-content">
                    <p>لینک تغییر رمز عبور برای شما ارسال شد</p>
                    <p>فرایند دریافت ایمیل ممکن است چندین دقیقه طول بکشد</p>
                    <p>صبور باشید و پوشه اسپم را هم چک کنید</p>
                    <Link to="/login">ورود</Link>
                </div>
            </div>
            );
        }
    }

    render() {
        return <div> {this.conRender()} </div>
    }

}

export default Forget;