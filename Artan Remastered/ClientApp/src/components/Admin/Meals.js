import React, { Component } from 'react'
import { Table, Button, Form, Input } from 'reactstrap'
import api from "../../Api/api";

export default class Meals extends React.Component {

    constructor(props) {
        super(props);
        this.state = { MealTypes: [], MealName: "", MealUnit: ""};
        this.Token = this.props.AuToken;
        this.BaseUrl = this.props.BaseUrl;
        this.config = {
            headers: { 'Authorization' : 'Bearer ' + this.Token }
        }

        if (!this.Token) this.props.history.push("/");
    }

    BindData = (Data) => {
        const meal = Data;
        this.setState({ MealTypes : meal });
    }

    AddMeal = () => {

        api.put("/Admin/AddMealType", {
            mealName: this.state.MealName,
            Mealunit : this.state.MealUnit
        } , this.config)
          .then(response => {this.BindData(response.data)})
          .catch(error => console.log(error))
    }

    SaveMeal = () => {
        api.post("/Admin/SaveMealType", this.state.MealTypes , this.config)
          .then(response => {this.BindData(response.data)})
          .catch(error => console.log(error))
    }

    RemoveMeal = (MealId) => {
        api.delete("/Admin/RemoveMealType?MealId=" + MealId , this.config)
          .then(response => {this.BindData(response.data)})
          .catch(error => console.log(error))
    }

    handleSaveMealTypeInput = (e , idx) =>
    {
        const newMeals = this.state.MealTypes.map((meal) => {
            if (idx !== meal.id) return meal;
            const name = e.target.name;
            const value = e.target.value;
            return { ...meal, [name] : value };
          });
      
        this.setState({ MealTypes: newMeals });
    }

    componentDidMount() {
        api.get("/Admin/Meals", this.config)
            .then(response => {
                console.log(response.data)
                this.BindData(response.data)
            })
          .catch(error => console.log(error))
      }
    
    render() {
        return (
            <div style={{width : "auto"}} className="admin-container">
            <div class="col-12">
                <div class="card text-right">
                    <h5 class="card-header">وعده غذایی</h5>
                    <div class="card-body">
                        <div class="form-row">
                            <div class="col">
                                <label for="inputMealname">نام وعده</label>
                                <input id="inputMealname" name="MealName" placeholder="نام را وارد کنید " class="form-control" onChange={e => this.setState({ MealName: e.target.value })} value={this.state.MealName} />
                            </div>
                            <div class="col">
                                <label for="inputunit">واحد</label>
                                <input id="inputunit" name="Mealunit" placeholder="به عنوان مثال : قطعه ، کف دست ، لیوان ..." onChange={e => this.setState({ MealUnit: e.target.value })} value={this.state.MealUnit} class="form-control"></input>
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-sm-6 pl-0">
                                <p class="text-right">
                                    <button class="btn btn-space btn-primary" onClick={this.AddMeal}>اضافه کردن</button>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 text-right">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="mb-0">جدول وعده غذایی </h5>
                        </div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <Table hover size="md">
                                    <thead>
                                        <tr>
                                            <th>#</th>
                                            <th>نام</th>
                                            <th>واحد</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            this.state.MealTypes.map((meal) => (
                                                <tr key={meal.id}>
                                                    <th scope="row">{meal.id}</th>
                                                    <td><Input id="inputText4" name="mealName" className="form-control"  onChange={e => this.handleSaveMealTypeInput(e ,meal.id)} value={meal.mealName}></Input></td>
                                                    <td><Input id="inputText4" name="mealunit" className="form-control"  onChange={e => this.handleSaveMealTypeInput(e , meal.id)} value={meal.mealunit ? meal.mealunit : 'ندارد'}></Input></td>
                                                    <td><Button id="delete" className="btn" style={{ top: "48%", position: "relative", color: "red", backgroundColor: "white" }} onClick={() => this.RemoveMeal(meal.id)}>X</Button></td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>       
                                </Table>
                            </div>
                            </div>
                            <div className="card-footer">
                                <div className="col-1 mb-3">
                                    <Button className="btn btn-primary btn-success mt-4" onClick={this.SaveMeal}>ذخیره</Button>
                                </div>
                            </div>
                    </div>
                </div>    
                </div>
                </div>
        );
    }
}