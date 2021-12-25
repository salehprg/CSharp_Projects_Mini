import React from 'react';

class PlanSelector extends React.Component {

    renderPlans = () => {

        this.props.userData.userExreciseView.sort(function(a, b){return a.planId - b.planId});
        var data = this.props.userData.userExreciseView;

        var planNumbers = [];

        for (let i = 0; i < data.length; i++) {
            if (!planNumbers.includes(data[i].planId)) planNumbers.push(data[i].planId);
        }      
        
        var plans = [];

        for (let i = 0; i < planNumbers.length; i++) {
            plans.push(
                <option key={planNumbers[i]} value={planNumbers[i]}>برنامه {planNumbers[i]}</option>
            );
        }

        return <React.Fragment>{plans}</React.Fragment>;
    }

    render(){
        return <React.Fragment>{this.renderPlans()}</React.Fragment>
    }

}

export default PlanSelector;