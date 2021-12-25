import React from 'react';
import {BrowserRouter , Route} from 'react-router-dom';
import HomePage from './HomePage';
import Login from './Login';
import Signup from './Signup';
import UserPanel from './UserPanel';
import Forget from './Forget';
import AdminIndex from './Admin/AdminIndex';
import Meals from './Admin/Meals';
import Excercises from './Admin/Excercises';
import UserSetMeal from './Admin/UserSetMeal';
import UserSetExcercise from './Admin/UserSetExcercise';
import AdminLayout from './Admin/AdminLayout';
import ForgotPassword from './ForgotPassword';

class App extends React.Component {

    state = { Authenticatetoken: '', isAuth : false };

    componentWillMount() {
        localStorage.getItem('auth') && this.setState({
            Authenticatetoken: JSON.parse(localStorage.getItem('auth'))
        });
    }

    componentWillUpdate(nextProps, nextState) {
        localStorage.setItem('auth' , JSON.stringify(nextState.Authenticatetoken));
    }

    setToken = (t) => {
        this.setState({ Authenticatetoken: t, isAuth: true });
    }

    render() {
        const BaseUrl = 'https://hamidgolestani.ir/api/';
        return(
            <div>
                <BrowserRouter>
                    <div>
                        <Route path="/" exact component={HomePage} />
                        <Route path="/login" exact render= {(props) => <Login {...props} onSet={this.setToken} />} />
                        <Route path="/signup" exact component={Signup} />
                        <Route path="/userpanel" exact component={UserPanel} />
                        <Route path="/resetpass" exact component={Forget} />
                        <Route path="/ForgotPassword" exact component={ForgotPassword} />
                        <Route path="/Admin">
                            <AdminLayout>
                                <div className="container-fluid mb-3 mt-3" style={{marginRight : "250px" , width : "auto" , paddingTop : "50px"}}>
                                <Route exact path="/Admin" render= {(props) => <AdminIndex {...props} AuToken={this.state.Authenticatetoken} BaseUrl={BaseUrl}/>}/>
                                <Route path="/Admin/Meals" render= {(props) => <Meals {...props} AuToken={this.state.Authenticatetoken} BaseUrl={BaseUrl}/>} />
                                <Route path="/Admin/Excercises" render= {(props) => <Excercises {...props} AuToken={this.state.Authenticatetoken} BaseUrl={BaseUrl}/>}/>
                                <Route path="/Admin/UserSetMeal" render= {(props) => <UserSetMeal {...props} AuToken={this.state.Authenticatetoken} BaseUrl={BaseUrl}/>}/>
                                <Route path="/Admin/UserSetExcercise" render= {(props) => <UserSetExcercise {...props} AuToken={this.state.Authenticatetoken} BaseUrl={BaseUrl} />}/>
                                </div>
                            </AdminLayout>
                        </Route>
                    </div>
                </BrowserRouter>
            </div>
        );
    }

}

export default App;