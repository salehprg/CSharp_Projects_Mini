import React from 'react';
import WorkoutContainer from './WorkoutContainer';

class WorkoutSection extends React.Component {

    render() {
        return(
            <WorkoutContainer userData={this.props.userData} />
        );
    }

}

export default WorkoutSection;