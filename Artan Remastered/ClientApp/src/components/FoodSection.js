import React from 'react';
import FoodContainer from './FoodContainer';

class FoodSection extends React.Component {

    render() {
        return(
            <FoodContainer userData={this.props.userData} />
        );
    }

}

export default FoodSection;