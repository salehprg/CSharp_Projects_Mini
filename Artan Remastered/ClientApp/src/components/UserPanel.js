import React from 'react';
import Sidebar from './Sidebar';
import FoodSection from './FoodSection';
import WorkoutSection from './WorkoutSection';
import Settings from './Settings';

class UserPanel extends React.Component {

    state = { whichPanel: 'food' };

    componentDidMount() {

        var x = null;
        
        try {
            x = this.props.location.state.detail;
            console.log(x);
        } catch(e) {
            this.props.history.push("/");
        }    
                
    }

    changePanel = (term) => this.setState({ whichPanel: term });

    showPanel = () => {
        if (this.state.whichPanel === 'food') return <FoodSection userData={this.props.location.state.detail} />;
        if (this.state.whichPanel === 'workout') return <WorkoutSection userData={this.props.location.state.detail} />;
        if (this.state.whichPanel === 'settings') return <Settings userData={this.props.location.state.detail} />;
    }

    render() {
        return (
            <div>
                <Sidebar username={this.props.location.state.detail.username} onClick={this.changePanel} />
                <div> {this.showPanel()} </div>
            </div>
        );  
    }

}

export default UserPanel;