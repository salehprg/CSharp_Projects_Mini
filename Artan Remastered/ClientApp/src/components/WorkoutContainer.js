import './WorkoutContainer.css';
import React from 'react';
import WorkoutCard from './WorkoutCard';
import PlanSelector from './PlanSelector';
import Popup from './Popup';

class WorkoutContainer extends React.Component {

    state = { showPlan: null, showGif: false, whatGif: null };

    componentDidMount() {

        this.props.userData.userExreciseView.sort(function(a, b){
            if ( a.planId === b.planId ) {
                return (a.setid > b.setid) ? 1 : (a.setid < b.setid) ? -1 : 0;
            }else {
                return (a.planId < b.planId) ? -1 : 1;
            }
        });

        var firstPlan = null;

        try {
            firstPlan = this.props.userData.userExreciseView[0].planId;

            for (let i = 1; i < this.props.userData.userExreciseView.length; i++) {
                if ( this.props.userData.userExreciseView[i].planId < firstPlan ) firstPlan = this.props.userData.userExreciseView[i].planId;
            }
        } catch (e) {}

        this.setState({ showPlan: firstPlan });

    }

    changePlan = (e) => {
        const planNumber = parseInt(e.target.value);
        this.setState({ showPlan: planNumber });
    }

    showInfo = (gifname) => {
        
        if ( gifname === null ) return null;

        this.setState({ showGif: true, whatGif: gifname });
    }

    hideInfo = () => {
        this.setState({ showGif: false, whatGif: null });
    }

    renderContent = () => {
        if (this.state.showGif) {
            return (
                <Popup 
                source={this.state.whatGif}
                />
              );
        }
    }

    render() {
        return(
            <div className="food-container" onClick={this.hideInfo}>
                <div className="food-content">
                    <div className="food-title">
                        <span>نام مربی:</span>
                    </div>
                    {this.renderContent()}
                    <div>
                    <select className="plan-list" onChange={this.changePlan}>
                        <PlanSelector userData={this.props.userData} planNum={this.state.showPlan} />
                    </select>

                    <div className="w-list">
                        <div className="jjj">
                        <table className="workout-table">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th>شماره</th>
                                    <th>حرکت</th>
                                    <th>مقدار</th>
                                    <th>تکرار</th>
                                    <th>استراحت</th>
                                    <th>اطلاعات</th>
                                </tr>
                            </thead>
                            <tbody>
                                <WorkoutCard 
                                userData={this.props.userData} 
                                plan={this.state.showPlan} 
                                onInfoClick={this.showInfo}
                                />
                            </tbody>
                        </table>
                        </div>
                    </div>
                </div>
                </div>
            </div>
        );
    }

}

export default WorkoutContainer;