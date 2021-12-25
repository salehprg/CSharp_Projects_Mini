import './FoodContainer.css';
import React from 'react';
import FoodCard from './FoodCard';

class FoodContainer extends React.Component {

    render() {
        return(
            <div className="food-container">
                <div className="food-content">
                    <div className="food-title">
                        <span>نام مربی:</span>
                    </div>

                    <div className="list">
                        <table className="list-table">
                            <thead>
                                <tr>
                                    <th>وعده</th>
                                    <th>نام غذا</th>
                                    <th>واحد</th>
                                    <th>تعداد</th>
                                </tr>
                            </thead>
                            <tbody>
                                <FoodCard userData={this.props.userData} />
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        );
    }

}

export default FoodContainer;