import './Signup.css';
import api from "../Api/api";
import React from 'react';
import Toastr from 'toastr';

class Signup extends React.Component {

    state = { fname: '', lname: '', phone: '', uname: '' , pword: '' , email: '', passVisibility: false, whatQ: 1 };

    componentDidMount() {
        Toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "10000",
            "hideDuration": "1000",
            "timeOut": "3000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }

    showError = (code) => {
        switch (code) {
            case 1:
                return 'نام کاربری موجود است';  
            case 2:
                return 'ایمیل تکراری است'
            case 3:
                return 'ایمیل نامعتبر';
            case 4:
                return 'نام کاربری نا معتبر';
            case 5:
                return 'رمز عبور اشتباه است';
            case 6:
                return 'رمز عبور باید دارای اعداد باشد';
            case 7:
                return 'رمز عبور باید دارای حروف کوچک باشد';
            case 8:
                return 'خظا';
            case 9:
                return 'رمز عبور باید دارای !@@#$%^&* باشد';
            case 10:
                return 'رمز عبور باید دارای حروف بزرگ باشد';
            case 11:
                return 'رمز عبور کوتاه است';
            default:
                return 'error';
        }
    }

    onFormSubmit = async (event) => {
        event.preventDefault();

        if (this.state.whatQ === 1) {
            this.setState({ whatQ: 2 });
            return;
        }

        var response = null;

        const params = {
            "Username" : this.state.uname,
            "Password" : this.state.pword,
            "Email" : this.state.email,
            "Firstname" : this.state.fname,
            "Lastname" : this.state.lname,
            "PhoneNumber" : this.state.phone
        }

        try {
            response = await api.put('/Home', params, {
                headers: {
                    'content-type' : 'application/json'
                }
            });

            Toastr["success"]("ثبت نام با موفقیت انجام شد. لطفا از بخش ورود وارد شوید");

        } catch (e) {
            this.setState({whatQ: 1});

            console.log(e)
    
            Toastr["error"](e.response.data);
            return;
        }
    }

    onClearClick = () => this.props.history.push("/");

    toggleVisible = () => this.setState({ passVisibility: !this.state.passVisibility });

    renderQ1 = () => {
        if (this.state.whatQ === 1) {
            return (
                <React.Fragment>
                    <input 
                        className="inputs" 
                        type="text" name="firstname" 
                        placeholder="نام" 
                        value= {this.state.fname}
                        onChange= { e => this.setState({ fname: e.target.value }) }
                        required
                    />
                    <input 
                        className="inputs" 
                        type="text"
                        name="lname" 
                        placeholder="نام خانوادگی"
                        value= {this.state.lname}
                        onChange= { e => this.setState({ lname: e.target.value }) }
                        required
                    />
                    <input 
                        className="inputs" 
                        type="phone" 
                        name="phone" 
                        placeholder="شماره همراه"
                        value= {this.state.phone}
                        onChange= { e => this.setState({ phone: e.target.value }) }
                        required
                    />
    
                    <input className="submit-btn" type="submit" value="بعدی" />
                </React.Fragment>
            );
        }
        else {
            return (
               <React.Fragment>
                   <input 
                        className="inputs" 
                        type="text" name="username" 
                        placeholder="نام کاربری" 
                        value= {this.state.uname}
                        onChange= { e => this.setState({ uname: e.target.value }) }
                        required
                    />
                    <input 
                        className="inputs" 
                        type={(this.state.passVisibility ? 'text' : 'password')}
                        name="password" 
                        placeholder="رمز عبور"
                        value= {this.state.pword}
                        onChange= { e => this.setState({ pword: e.target.value }) }
                        required
                    />
                    <i className="material-icons pass-icon" onClick={this.toggleVisible}>{(this.state.passVisibility ? 'visibility_off' : 'visibility')}</i>
                    <input 
                        className="inputs" 
                        type="email" 
                        name="email" 
                        placeholder="ایمیل"
                        value= {this.state.email}
                        onChange= { e => this.setState({ email: e.target.value }) }
                        required
                    />
    
                    <input className="submit-btn" type="submit" value="ثبت نام" />
               </React.Fragment>
            );
        }
    }

    render() {
        return (
            <div className="signup-box">
                    <i className="material-icons clear" onClick={this.onClearClick}>clear</i><br />
                    <i className="material-icons signup-icon">account_box</i>
    
                    <form onSubmit={this.onFormSubmit}>
                        {this.renderQ1()}
                    </form>
                </div>
        );
    }

}

export default Signup;