import React from 'react';

class FoodCard extends React.Component {

    renderFoods = () => {

        var x = false;
        var f = true;

        this.props.userData.userMealView.sort(function(a, b){return a.mealNumber - b.mealNumber});

        var foods = [];
         for (let i = 0; i < this.props.userData.userMealView.length; i++) {

            if ( i !== this.props.userData.userMealView.length - 1 ) {
                if (this.props.userData.userMealView[i].mealNumber === this.props.userData.userMealView[i + 1].mealNumber) {
                    x = true;
                }
                else {
                    x = false;
                }
            } else { x = false }

            if (i !== 0) {
                if ( this.props.userData.userMealView[i].mealNumber !== this.props.userData.userMealView[i - 1].mealNumber ) {
                    f = true;
                }
                else { f = false }
            }
        
        foods.push(
                <React.Fragment key={this.props.userData.userMealView[i].id}>
                    <tr className={(!x ? 'test' : '')} key={this.props.userData.userMealView[i].id}>
                        <td >{(f ? this.props.userData.userMealView[i].mealNumber : '')}</td>
                        <td>{this.props.userData.userMealView[i].mealName}</td>
                        <td>{this.props.userData.userMealView[i].mealunit}</td>
                        <td>{this.props.userData.userMealView[i].mealcount}</td>
                    </tr>
             </React.Fragment>
        );
        
    }
    
        return <React.Fragment>{foods}</React.Fragment>

    }

    render() {
        return <React.Fragment>{this.renderFoods()}</React.Fragment>
    }

}

export default FoodCard;  