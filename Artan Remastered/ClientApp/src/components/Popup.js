import React from 'react';  
import './Popup.css';  

class Popup extends React.Component {  
  render() {  
    return (  
        <div className='popup'>  
            <div className='popup\_inner'>  
                <img alt="Movement gif" src={`/Gifs/${this.props.source}`} width="100%" onClick={(e) => e.stopPropagation()} />  
            </div>  
        </div>  
    );  
    }  
}  

export default Popup;