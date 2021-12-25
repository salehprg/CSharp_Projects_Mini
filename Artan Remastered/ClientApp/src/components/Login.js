import './Login.css';
import React from 'react';
import api from "../Api/api";
import Toastr from 'toastr';
import { Link } from 'react-router-dom';

class Login extends React.Component {

    state = { uname: '' , pword: '', passVisibility: false };

    componentDidMount() {
        Toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "3000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };
    }

    onFormSubmit = (event) => {
        event.preventDefault();
        this.SearchUsers();
    }

    SearchUsers = async () => {

        const params = {
            "Username" : this.state.uname,
            "Password" : this.state.pword
        }

        var response = null;
        var response2 = null;
        
        try {
            response = await api.post('/Home', params, {
                headers: {
                    'content-type' : 'application/json'
                }
            });
        } catch (e) {
            Toastr["error"]("نام کاربری یا رمز عبور اشتباه است");
            return;
        }

        try {
            response2 = await api.get('/Users', {
                headers: {
                    Authorization: `Bearer ${response.data.token}` 
                }
            });

            this.props.history.push({
                pathname: '/userpanel',
                state: { detail: response2.data }
            })

            return;

        } catch (e) {  }

        try {
            response2 = await api.get('/Admin/checkPermision', {
                headers: {
                    Authorization: `Bearer ${response.data.token}` 
                }
            }); 

            this.props.onSet(response.data.token);
            
            this.props.history.push('/Admin');

            return;

        } catch (e) {
            console.log(e.response)
            return;
        }

    }

    onClearClick = () => this.props.history.push("/");

    toggleVisible = () => this.setState({ passVisibility: !this.state.passVisibility });

    render() {
        return (
            <div className="login-box">
                <i className="material-icons clear" onClick={this.onClearClick}>clear</i><br />
                <i className="material-icons login-icon">account_circle</i>
            
                <form onSubmit={this.onFormSubmit}>
                    <input 
                        className="inputs" 
                        type="text" name="username" 
                        placeholder="نام کاربری" 
                        value= {this.state.uname}
                        onChange= { e => this.setState({ uname: e.target.value }) }
                        required
                    />
                    <input 
                        className="inputs pass-input" 
                        type={(this.state.passVisibility ? 'text' : 'password')}
                        name="password" 
                        placeholder="رمز عبور"
                        value= {this.state.pword}
                        onChange= { e => this.setState({ pword: e.target.value }) }
                        required
                    />
                    <i className="material-icons pass-icon" onClick={this.toggleVisible}>{(this.state.passVisibility ? 'visibility_off' : 'visibility')}</i>

                    <input className="submit-btn" type="submit" value="ورود" />
                </form>

                <div className="txts">
                    <Link to="/resetpass" id="forgot-pass">فراموشی رمز</Link><br />
                    <Link to="/signup">ساخت حساب کاربری</Link>
                </div>
            </div>
         );
    }
}

export default Login;