import React, { Component } from 'react'
import { Dropdown, Input, Button, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import api from "../../Api/api";

function MealTypeOpt(CurMealType, MealTypeId, MealName) {
    if (CurMealType == MealTypeId) {
        return(
            <option value={MealTypeId} selected>
                {MealName}
            </option>
        )
    }
    else
    {
        return(
            <option value={MealTypeId}>
                {MealName}
            </option>
        )
    }
}

export default class UserSetMeal extends Component {
    constructor(props) {
        super(props);
        this.state = { Meals: [], MealsType: [], UserName: '', MealNumbers: [], AddMeal: { MealType: "0", MealNumber: "0", Count: "0" }};
        this.Token = this.props.AuToken;
        this.BaseUrl = this.props.BaseUrl;
        this.config = {
            headers: { 'Authorization' : 'Bearer ' + this.Token }
        }

        if (!this.Token) this.props.history.push("/");
    }

    handleAddMealInput = (e) =>
    {
        const { AddMeal } = this.state;
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ AddMeal: { ...AddMeal, [name]: value } })

    }

    handleSaveMealInput = (e , idx) =>
    {
        const newMeals = this.state.Meals.map((meal) => {
            if (idx !== meal.id) return meal;
            const name = e.target.name;
            const value = e.target.value;
            return { ...meal, [name] : parseFloat(value) };
          });
      
        this.setState({ Meals: newMeals });
    }

    handleDeleteMealBtn = (mealid) => {    
        
        api.delete("/Admin/DeleteMeal?mealid=" + mealid, this.config)
            .then(response => {
            console.log(response.data);
              this.setState({
                  Meals: response.data.meal,
                  MealsType: response.data.mealTypes,
                  UserName: response.data.userName,
                  MealNumbers : response.data.mealnumbers
              });

          })
            .catch(error => console.log(error))
    }

    handleSaveMealBtn = () => {    
        const UserId = this.props.location.id;
        
        console.log(this.state.Meals)

        api.post("/Admin/SaveMeal?UserId=" + UserId, {
            meal : this.state.Meals
        }, this.config)
            .then(response => {
              this.setState({
                  Meals: response.data.meal,
                  MealsType: response.data.mealTypes,
                  UserName: response.data.userName,
                  MealNumbers : response.data.mealnumbers
              });

            })
            .then(
                console.log("Saved!")
            )
            .catch(error => console.log(error.response))
    }


    handleAddMealBtn = () => {
        const id = this.props.location.id;


        api.put("/Admin/AddUserMeal", {
            mealtype: parseInt(this.state.AddMeal.MealType),
            userid: id,
            mealcount: parseFloat(this.state.AddMeal.Count),
            MealNumber : parseInt(this.state.AddMeal.MealNumber)
        }, this.config)
            .then(response => {
              this.setState({
                  Meals: response.data.meal,
                  MealsType: response.data.mealTypes,
                  UserName: response.data.userName,
                  MealNumbers : response.data.mealnumbers
              });

          })
            .catch(error => console.log(error.response))
    }

    componentDidMount() {
        const id = this.props.location.id;
        
        api.get("/Admin/Setmeal?id=" + id, this.config)
            .then(response => {
              this.setState({
                  Meals: response.data.meal,
                  MealsType: response.data.mealTypes,
                  UserName: response.data.userName,
                  MealNumbers : response.data.mealnumbers
              });

          })
            .catch(error => console.log(error))
         
    }

    render() {
        return (
            <div className="row text-right admin-container">
        <div className="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
                <div className="section-block" id="basicform">
                    <h3 className="section-title">برنامه غذایی آقای {this.state.UserName}</h3>
                </div>
                <div className="card-body border-bottom row">
                    <div className="col-12">
                        <div className="form-row">
                            <div className="col-lg-4 col-sm-6">
                                    <label className="col-form-label">نوبت وعده</label>
                                    <select className="form-control" name="MealNumber" id="input-select" onChange={e => this.handleAddMealInput(e)}>
                                                <option value="0">
                                                    انتخاب کنید...
                                                </option>
                                        {
                                            this.state.MealNumbers.map((MealNum) => (
                                                <option value={MealNum}>
                                                    {MealNum}
                                                </option>
                                            ))
                                        }
                                    </select>
                                <span className="badge badge-info mt-2" style={{wordWrap : "break-word" ,whiteSpace : "unset" , fontSize : "14px" , fontWeight : "normal"}}>اگر میخواهید وعده جدید اضافه کنید نوبت وعده را روی انتخاب کنید... قراردهید</span>
                            </div>

                            <div className="col-lg-2 col-sm-6">
                                <label className="col-form-label">نام وعده غذایی</label>
                                    <select className="form-control" name="MealType" onChange={e => this.handleAddMealInput(e)}>
                                                <option value="0">
                                                    انتخاب کنید...
                                                </option>
                                {
                                    this.state.MealsType.map((MealType) => (
                                        <option value={MealType.id}>
                                            {MealType.mealName}
                                        </option>
                                    ))
                                }
                                </select>
                            </div>
                            <div className="col-lg-2 col-sm-12">
                                <label className="col-form-label">مقدار</label>
                                <Input id="inputText4" name="Count" className="form-control" placeholder="مقدار وعده"  onChange={e => this.handleAddMealInput(e)}></Input>
                            </div>
                            <div className="col-2" style={{ height: "50%"}}>
                                <Button className="btn btn-primary btn-success" style={{ height: "70%", marginTop: "2rem" }} onClick={() => this.handleAddMealBtn()}>+</Button>
                            </div>
                        </div>             
                    </div>
                </div>
                <div className="card">
                    <h5 className="card-header">برنامه غذایی</h5>
                            
                        {
                                
                                this.state.MealNumbers.map((CurMealNum) => (
                                    <div className="card-body border-bottom row" key={CurMealNum.id}>
                                        <div className="col-2 border-left text-center">
                                            <span style={{ position: "relative", top: "45%", fontFamily: "B Titr", fontsize: "20px", wordwrap: "normal" }}>وعده  {CurMealNum} </span>
                                        </div>
                                        <div className="col-10">

                                            {
                                                this.state.Meals.filter(x => x.mealNumber == CurMealNum).map((CurMeal) => (
                                            <div className="form-row"  key={CurMeal.id}>
                                                <div className="col-2">
                                                    <label className="col-form-label">تغذیه</label>
                                                        <select name="mealtype" className="form-control" id="input-select"  onChange={e => this.handleSaveMealInput(e , CurMeal.id)}> 
                                                            {
                                                                this.state.MealsType.map((MealType) => (
                                                                    MealTypeOpt(CurMeal.mealtype , MealType.id , MealType.mealName)
                                                                ))
                                                            }
                                                    </select>
                                                </div>
                                                <div className="col">
                                                    <label className="col-form-label">مقدار</label>
                                                        <Input id="inputText4" name="mealcount" className="form-control" placeholder="مقدار تغذیه" onChange={e => this.handleSaveMealInput(e , CurMeal.id)} value={CurMeal.mealcount}/>
                                                </div>
                                                <div className="col">
                                                    <label className="col-form-label">توضیحات</label>
                                                        <Input id="inputPassword" name="description" placeholder="توضیحات" className="form-control" onChange={e => this.handleSaveMealInput(e , CurMeal.id)} value={CurMeal.description}/>
                                                </div>
                                                <div className="col-1">
                                                    <Button id="delete" className="btn" style={{ top: "48%", position: "relative", color: "red", backgroundColor: "white" }} onClick={() => this.handleDeleteMealBtn(CurMeal.id)}>X</Button>
                                                </div>
                                            </div>
                                            ))
                                            }
                                        </div>

                                    </div>
                                ))
                            }
                            

                    <div className="col-1 mb-3">
                            <Button className="btn btn-primary btn-success mt-4" onClick={this.handleSaveMealBtn}>ذخیره</Button>
                    </div>
                </div>
        </div>
    </div>
        )
    }
}
