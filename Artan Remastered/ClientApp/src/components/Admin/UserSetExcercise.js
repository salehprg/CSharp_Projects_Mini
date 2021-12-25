import React, { Component } from 'react'
import { Dropdown, Input, Button, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
import Switch from '@material-ui/core/Switch';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import api from "../../Api/api";

function ExTypeOpt(CurExType, ExTypeId, ExName) {
    if (CurExType == ExTypeId) {
        return(
            <option key={ExTypeId} value={ExTypeId} selected>
                {ExName}
            </option>
        )
    }
    else
    {
        return(
            <option key={ExTypeId} value={ExTypeId}>
                {ExName}
            </option>
        )
    }
}

function SetidOpt(ExSetId , SetId)
{
    if (ExSetId === SetId)
    {
        return (
        <option key={SetId} value={SetId} selected>
            {SetId}
        </option>)
    }
    else {
        return (
            <option key={SetId} value={SetId}>
                {SetId}
            </option>
        )
    }
}

export default class UserSetExcercise extends Component {

    constructor(props) {
        super(props);
        this.state = {setId : [] , PlanId : [] , Exercises: [], ExerciseTypes: [], UserName: "", AddEx: { userid: 0, exerciseType: 0, count: 0.0, setid: 1 , planId: 1 , repeat : 1 , rest : 40} , SuperSet : false };
        this.Token = this.props.AuToken;
        this.BaseUrl = this.props.BaseUrl;
        this.config = {
            headers: { 'Authorization' : 'Bearer ' + this.Token }
        }

        if (!this.Token) this.props.history.push("/");
    }

    GetSetIds = (newEx , prevId) => {

        var newSetIds = newEx.map((Ex) => {
            return Ex.setid
        });
        
        const setId = Array.from(new Set(newSetIds.sort((a, b) => a - b)));

        console.log(newSetIds)
        this.setState({setId : setId})
    }

    bindData = (Data) => {
        this.setState({
            Exercises: Data.exercises,
            ExerciseTypes: Data.exerciseTypes,
            PlanId: Data.planId,
            UserName: Data.username
        });
        this.GetSetIds(this.state.Exercises);
        this.setState({ AddEx: { ...this.state.AddEx, exerciseType : this.state.ExerciseTypes[0].id } })
    }

    GetExcercisesByPlanId = (Id) => {
        const UserId = this.props.location.id;
        api.get("/Admin/SetExcercise?Id=" + UserId + "&planid=" + Id, this.config)
            .then(response => {
                this.bindData(response.data);
                this.ResetAddExFormInput();
            })
          .catch(error => console.log(error))
    }

    ResetAddExFormInput = () => {
        this.setState({AddEx :  { ...this.state.AddEx, exerciseType : 0 }})
        this.setState({AddEx :  { ...this.state.AddEx, count : 0 }})
        this.setState({AddEx :  { ...this.state.AddEx, setid : 0 }})
        this.setState({AddEx :  { ...this.state.AddEx, repeat : 0 }})
        this.setState({AddEx :  { ...this.state.AddEx, rest : 1 }})
    }

//#region Input

    handleAddExInput = (e) =>
    {
        const name = e.target.name;
        const value = e.target.value;
        this.setState({ AddEx: { ...this.state.AddEx, [name]: parseInt(value) } })
        
    }

    handleSaveExInput = (e , idx) =>
    {
        
        const newExs = this.state.Exercises.map((Excercise) => {
            if (idx !== Excercise.id) return Excercise;
            const name = e.target.name;
            const value = e.target.value;
            return { ...Excercise, [name] : parseInt(value) };
          });

        this.setState({ Exercises: newExs });

        if (e.target.name == "setid")
        {   
            this.GetSetIds(newExs);
        }
        
    }

//#endregion

    
    handleAddExcerciseBtn = () => {
        console.log("Tets")
        const UserId = this.props.location.id;
        console.log(this.state);
        api.put("/Admin/AddUserExcercise?UserId=" + UserId, this.state.AddEx , this.config)
            .then(response => {
                this.bindData(response.data)

                if (!this.state.SuperSet) {
                    var CurSetId = parseInt(this.state.AddEx.setid);
                    this.setState({ AddEx: { ...this.state.AddEx, setid : (CurSetId + 1)} })
                }
            }
            )
            .catch(error => console.log(error))
        
       
    }

    handleDeleteExcerciseBtn = (ExId) => {    
        
        api.delete("/Admin/DeleteUserExcercise?ExId=" + ExId, this.config)
            .then(response => {
                console.log(response.data);
                this.bindData(response.data);
            })
            .catch(error => console.log(error))
    }

    handleSaveExcerciseBtn = () => {    
        const UserId = this.props.location.id;

        api.post("/Admin/SaveUserExcercise", this.state.Exercises, this.config)
            .then(response => this.bindData(response.data))
            .then(
                console.log("Saved!")
            )
            .catch(error => console.log(error))
    }


    handlePlanIdChange = (e) => {
        this.setState({ AddEx: { ...this.state.AddEx , planId: parseInt(e.target.value) } })
        this.GetExcercisesByPlanId(e.target.value);
    }

    AddNewPlan = () => {
        var lastPlanId = 0;
        if (this.state.PlanId.length > 0) {
            lastPlanId = this.state.PlanId[this.state.PlanId.length - 1];
        }

        this.state.PlanId.push((parseInt(lastPlanId) + 1));
        this.setState({ PlanId: this.state.PlanId })
        this.GetExcercisesByPlanId(this.state.PlanId[this.state.PlanId.length - 1]);

        this.ResetAddExFormInput();

        this.setState({ AddEx: { ...this.state.AddEx, planId: parseInt(this.state.PlanId[this.state.PlanId.length - 1]) } })

        console.log(this.state)
    }

    DeletePlan = () => {
        const PlanId = this.state.AddEx.planId;
        const Userid = this.props.location.id
        api.delete("/Admin/DeleteUserPlan?PlanId=" + PlanId + "&UserId=" + Userid , this.config)
            .then(response => {
                this.bindData(response.data);
            })
          .catch(error => console.log(error))
    }
    

    componentDidMount() {
        const UserId = this.props.location.id;
        api.get("/Admin/SetExcercise?Id=" + UserId + "&planid=" + 1, this.config)
            .then(response => {
                
                this.bindData(response.data);
            })
          .catch(error => console.log(error))
    }


    render() {
        const Sets = [1,2,3,4,5,6,7,8,9,10]
        return (
            
            console.log(this.state),
        <div className="text-right">
            <div className="col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12">
            <div className="section-block" id="basicform">
                <h3 className="section-title">برنامه ورزشی آقای  {this.state.UserName}</h3>
            </div>
            <div className="card-body border-bottom row">
                <div className="col-12">
                    <div className="row">       
                                
                        <div className="col-xl-2 col-lg-2 col-sm-4 col-4">
                            <label className="col-form-label">شماره برنامه</label>
                            <select className="form-control" id="input-select" name="planId" onChange={e => this.handlePlanIdChange(e)} value={this.state.AddEx.planId}>
                                {
                                    this.state.PlanId.map((p) => (
                                        <option key={p} value={p}>
                                            {p}
                                        </option>
                                    ))
                                }
                            </select>
                        </div>
                                
                        <div className="col-xl-2 col-lg-3 col-sm-5 col-3" style={{ height: "50%"}}>
                            <Button className="btn btn-primary btn-success" style={{ height: "70%", marginTop: "2rem" }} onClick={this.AddNewPlan}>برنامه جدید</Button>
                        </div>
                                
                        <div className="col-xl-5 col-lg-4 col-sm-4 col-5" style={{ height: "50%"}}>
                            <Button className="btn btn-primary btn-danger" style={{ height: "70%", marginTop: "2rem" }} onClick={this.DeletePlan}>حذف برنامه فعلی</Button>
                        </div>
                                
                    </div>
                            
                    <div className="row">    
                        <div className="col-xl-2 col-lg-2 col-sm-4 col-4">
                            <label className="col-form-label">ست</label>
                            <select className="form-control" id="input-select" name="setid" disabled={this.state.SuperSet} onChange={e => this.handleAddExInput(e)} value={this.state.AddEx.setid}>
                                {
                                    Sets.map((s) => (
                                        <option key={s} value={s}>
                                            {s}
                                        </option>
                                    ))
                                }
                            </select>
                        </div>
                                

                        <div className="col-xl-2 col-lg-2 col-sm-8 col-8">
                            <label for="input-select" className="col-form-label">نوع ورزش</label>
                            <select className="form-control" id="input-select" name="exerciseType" onChange={e => this.handleAddExInput(e)} value={this.state.AddEx.exerciseType}>
                                {
                                    this.state.ExerciseTypes.map((ExNum) => (
                                        <option key={ExNum.id} value={ExNum.id}>
                                            {ExNum.exercisename}
                                        </option>
                                    ))
                                }
                            </select>
                        </div>
                                

                        <div className="col-xl-2 col-lg-2 col-sm-4 col-4">
                            <label className="col-form-label">تعداد</label>
                            <Input id="inputText4" className="form-control" placeholder="تعداد تمرین" min="0" type="number" name="count" onChange={e => this.handleAddExInput(e)} value={this.state.AddEx.count}/>
                        </div>
                        <div className="col-xl-2 col-lg-2 col-sm-4 col-4">
                            <label className="col-form-label">تکرار</label>
                            <Input id="inputText4" className="form-control" placeholder="تکرار تمرین " min="0" type="number" name="repeat" onChange={e => this.handleAddExInput(e)} value={this.state.AddEx.repeat}/>
                        </div>
                        <div className="col-xl-2 col-lg-2 col-sm-4 col-4">
                            <label className="col-form-label">استراحت</label>
                            <Input id="inputText4" className="form-control" placeholder="استراحت" min="0" type="number" name="rest" onChange={e => this.handleAddExInput(e)} value={this.state.AddEx.rest}/>
                        </div>
                        <div className="col-lg-2 col-sm-4 col-6" style={{ height: "50%" , paddingTop : "2rem" }}>
                        <FormControlLabel
                            control={<Switch checked={this.state.SuperSet} onChange={e => this.setState({ SuperSet: e.target.checked })} color="primary"></Switch>}
                            label="سوپر ست"
                        />
                            
                        </div>
                        <div className="col-lg-2 col-sm-2 col-2" style={{ height: "50%"}}>
                            <Button className="btn btn-primary btn-success" style={{ height: "70%", marginTop: "2rem" }} onClick={this.handleAddExcerciseBtn}>+</Button>
                        </div>
                    </div>
                </div>
            </div>
    
                    <div className="card">
                        <h5 className="card-header">برنامه ورزشی</h5>
                        {
                                this.state.setId.map((setid) => (
                            <div className="card-body border-bottom row ">
                                        <div className="col-12">
                                            {this.state.Exercises.filter(x => x.setid == setid).map((CurEx) => (
                                    <div className="form-row mb-4">
                                        
                                        <div className="col-lg-2 col-sm-6 col-5">
                                            <label className="col-form-label">ست</label>
                                            <select className="form-control" id="input-select" name="setid" onChange={e => this.handleSaveExInput(e , CurEx.id)}>
                                                {
                                                    Sets.map((s) => (
                                                        SetidOpt(CurEx.setid , s)
                                                    ))
                                                }
                                            </select>
                                        </div>
                                        <div className="col-lg-2 col-sm-6 col-7">
                                            <label className="col-form-label">نوع ورزش</label>
                                            <select className="form-control" id="input-select" name="exerciseType" onChange={e => this.handleSaveExInput(e , CurEx.id)}>
                                            {
                                                this.state.ExerciseTypes.map((ExType) => (
                                                    ExTypeOpt(CurEx.exerciseType , ExType.id , ExType.exercisename)
                                                ))
                                            }
                                            </select>
                                        </div>
                                        <div className="col-lg-2 col-sm-12 col-4">
                                            <label className="col-form-label">مقدار</label>
                                            <Input id="inputText4" className="form-control" type="number" min="0"  name="count" onChange={e => this.handleSaveExInput(e , CurEx.id)} value={CurEx.count} placeholder="تعداد تمرین"/>
                                        </div>
                                        <div className="col-lg-2 col-sm-12 col-4">
                                            <label className="col-form-label">تکرار</label>
                                            <Input id="inputText4" className="form-control" type="number"  min="0" name="repeat" onChange={e => this.handleSaveExInput(e , CurEx.id)} value={CurEx.repeat} placeholder="تعداد تمرین"/>
                                        </div>
                                        <div className="col-lg-2 col-sm-12 col-4">
                                            <label className="col-form-label">استراحت</label>
                                            <Input id="inputText4" className="form-control" type="number"  min="0" name="rest" onChange={e => this.handleSaveExInput(e , CurEx.id)} value={CurEx.rest} placeholder="تعداد تمرین"/>
                                        </div>
                                        <div className="col-1">
                                                        <Button id="delete" className="btn btn-danger" style={{ top: "48%", position: "relative" }} onClick={() => this.handleDeleteExcerciseBtn(CurEx.id)}>X</Button>
                                        </div>
                                    </div>
                                ))}
                                </div>
                            </div>
                                ))
                        }
                        <div className="card-footer">
                            <Button className="btn btn-primary btn-success mt-4" onClick={this.handleSaveExcerciseBtn}>ذخیره</Button>
                        </div>
                    </div>
                </div>
        </div>
        )
    }
}
