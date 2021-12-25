import React, { Component } from 'react'
import { Table, Button, Input } from 'reactstrap'
import api from "../../Api/api";

export default class Excercises extends React.Component {

    constructor(props) {
        super(props);
        this.state = { ExTypes: [], ExName: "", ExUnit: "" , GifFile : null , UploadState : false , imagePreviewUrl: ''};
        this.Token = this.props.AuToken;
        this.BaseUrl = this.props.BaseUrl;
        this.config = {
            headers: { 'Authorization' : 'Bearer ' + this.Token }
        }

        if (!this.Token) this.props.history.push("/");
    }

    BindData = (Data) => {
        const Ex = Data;
        this.setState({ ExTypes : Ex });
    }

    onFileSelected = (event) => {

        event.preventDefault();
        var Gif = event.target.files[0];

        let reader = new FileReader();

        reader.onloadend = () => {
        this.setState({
            GifFile: Gif,
            imagePreviewUrl: reader.result
        });
        }

        reader.readAsDataURL(Gif)
        
    }

    AddEx = () => {
        
        var GifName = "";
        if (this.state.GifFile != null)
        {
            GifName = this.state.GifFile.name;
        }

        api.put("/Admin/AddExcerciseType", {
                Exercisename: this.state.ExName,
                Exerciseunit: this.state.ExUnit,
                ExcerciseGif : GifName
            }, this.config)
            .then(response => {

                let form = new FormData();
                form.append('user', "Gif");
                form.append('file', this.state.GifFile);

                this.config = {
                    headers: {
                        'Authorization': 'Bearer ' + this.Token,
                        'content-type': 'multipart/form-data'
                    },
                };

                api.post("/Admin/UploadGif", form , this.config)
                    .then(r => { this.BindData(response.data) })
                .catch(error => console.log(error))
                
                

            })
            .catch(error => console.log(error))

    }

    SaveExType = () => {
        api.post("/Admin/SaveExcerciseType", this.state.ExTypes , this.config)
          .then(response => {this.BindData(response.data)})
          .catch(error => console.log(error))
    }

    RemoveExType = (ExId) => {
        api.delete("/Admin/RemoveExcerciseType?ExId=" + ExId , this.config)
          .then(response => {this.BindData(response.data)})
          .catch(error => console.log(error))
    }

    handleSaveExTypeInput = (e , idx) =>
    {
        const newExs = this.state.ExTypes.map((Ex) => {
            if (idx !== Ex.id) return Ex;
            const name = e.target.name;
            const value = e.target.value;
            return { ...Ex, [name] : value };
          });
      
        this.setState({ ExTypes: newExs });
    }

    componentDidMount() {

        api.get("/Admin/Excercise", this.config)
          .then(response => {
            const Excercise = response.data;
            this.setState({ ExTypes : Excercise });
          })
          .catch(error => console.log(error))
    }
    
    render() {

        let {imagePreviewUrl} = this.state;
        let $imagePreview = null;
        if (imagePreviewUrl) {
        $imagePreview = (<img src={imagePreviewUrl} width="200px" height="100px" />);
        }
        
        return (
            <div style={{width : "auto"}} className="admin-container">
            <div class="col-12">
                <div class="card text-right">
                    <h5 class="card-header">ورزش ها</h5>
                        <div class="card-body">
                        <div class="form-row">
                            <div class="col">
                                <label for="inputMealname">نام ورزش</label>
                                <input id="inputMealname" type="text" name="ExName"  placeholder="نام را وارد کنید "  onChange={e => this.setState({ ExName: e.target.value })} value={this.state.ExName} class="form-control"></input>
                            </div>
                            <div class="col">
                                <label for="inputunit">واحد</label>
                                <input id="inputunit" type="text" name="Exunit"  placeholder="به عنوان مثال : قطعه ، کف دست ، لیوان ..." onChange={e => this.setState({ ExUnit: e.target.value })} value={this.state.ExUnit} class="form-control"></input>
                            </div>
                            <div class="col form-group file">
                                <label for="inputunit">آموزش حرکت</label>
                                <input id="inputunit" type="file" name="ExGif" onChange={this.onFileSelected} class="form-control"></input>
                                {$imagePreview}
                            </div>
                        </div>
                        <div class="row mt-2">
                            <div class="col-sm-6 pl-0">
                                <p class="text-right">
                                    <button class="btn btn-space btn-primary" onClick={this.AddEx} >اضافه کردن</button>
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
                            <h5 class="mb-0">ورزش ها</h5>
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
                                            this.state.ExTypes.map((Excercise) => (
                                                <tr key={Excercise.id}>
                                                    <th scope="row">{Excercise.id}</th>
                                                    <td><Input id="inputText4" name="exercisename" className="form-control"  onChange={e => this.handleSaveExTypeInput(e , Excercise.id)} value={Excercise.exercisename}></Input></td>
                                                    <td><Input id="inputText4" name="exerciseunit" className="form-control" onChange={e => this.handleSaveExTypeInput(e , Excercise.id)} value={Excercise.exerciseunit ? Excercise.exerciseunit : 'ندارد'}></Input></td>
                                                    <td><Button id="delete" className="btn" style={{ top: "48%", position: "relative", color: "red", backgroundColor: "white" }} onClick={() => this.RemoveExType(Excercise.id)}>X</Button></td>
                                                </tr>
                                            ))
                                        }
                                    </tbody>       
                                </Table>
                            </div>
                            </div>
                            <div className="card-footer">
                                <div className="col-1 mb-3">
                                    <Button className="btn btn-primary btn-success mt-4" onClick={this.SaveExType}>ذخیره</Button>
                                </div>
                            </div>
                    </div>
                </div>    
                </div>
                </div>
        );
    }
}