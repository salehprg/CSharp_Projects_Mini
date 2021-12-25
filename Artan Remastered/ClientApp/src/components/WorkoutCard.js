import './WorkoutCard.css';
import React from 'react';

class WorkoutCard extends React.Component {

    componentDidMount() {
        console.log(this.props.userData);
    }

    renderInfo = (name) => {
        if (name !== null) {
            return ( 
                    <i className="material-icons info-icon">info</i>
            );
        } else {
            return null;
        }
    }

    renderWorkouts = () => {

        var data = this.props.userData.userExreciseView;
        var workouts = [];

        var colorCode = 1;
        var counter = 1;

        for (let i = 0; i < data.length; i++) {

            var isFirst = true;
            var only = false;
            
            if ( data[i].planId === this.props.plan ) {

                if (data.length !== 1) {
                    if (i !== 0 && i !== data.length - 1) {
                        if (data[i].planId === data[i-1].planId && data[i].setid === data[i-1].setid ) {
                            isFirst = false;
                        } else {
                            if ( data[i].planId === data[i+1].planId &&  data[i].setid !== data[i+1].setid ) {
                                only = true;
                            } 
                            if ( data[i].planId !== data[i+1].planId &&  data[i].setid !== data[i-1].setid ) {
                                only = true;
                            } 
                        }
                    }
                    else {
                        if (i === 0) {                            
                            if ( data[i].setid !== data[i+1].setid ) only = true;
                            else if ( data[i].planId !== data[i+1].planId ) only = true;
                        }
                        else if (i === data.length - 1) {
                            if (data[i].planId === data[i-1].planId && data[i].setid !== data[i-1].setid) only = true;
                            if (data[i].planId === data[i-1].planId && data[i].setid === data[i-1].setid) isFirst = false;
                        }
                    }
                }
                else {
                    only = true;
                }
                
                workouts.push(
                    <tr key={data[i].id} className={"color" + colorCode}>
                        <td >{((isFirst && !only) ? 'super set' : '')}</td>
                        <td>{counter++}</td>
                        <td>{data[i].exercisename}</td>
                        <td>{data[i].count}</td>
                        <td>{(isFirst ? data[i].repeat : '')}</td>
                        <td>{(isFirst ? data[i].rest : '')}</td>
                        <td onClick={(e) => {
                            e.stopPropagation()
                            this.props.onInfoClick(data[i].excerciseGif)
                        }}>{this.renderInfo(data[i].excerciseGif)}</td>
                    </tr>
                );

            }

            if (i !== data.length - 1) {
                if ( data[i].setid !== data[i+1].setid ) {
                    if ( colorCode === 3 ) colorCode = 1;
                    else colorCode++;
                }
            }

        }
    
        return <React.Fragment>{workouts}</React.Fragment>

    }

    render() {
        return <React.Fragment>{this.renderWorkouts()}</React.Fragment>
    }

}

export default WorkoutCard;  