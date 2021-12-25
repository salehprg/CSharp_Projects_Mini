import './Sidebar.css';
import React from 'react';

class Sidebar extends React.Component {

    state = { SidebarIsShown: false , whichPanel: 'food' };

    showSidebar = () => {
        this.setState({ SidebarIsShown: true });
    }

    hideSidebar = () => {
        this.setState({ SidebarIsShown: false });
    }

    togglePanelToFood = () => {
        this.setState({ SidebarIsShown: false , whichPanel: 'food' }); 
        this.props.onClick('food'); 
    }

    togglePanelToWorkout = () => {
        this.setState({ SidebarIsShown: false , whichPanel: 'workout' });
        this.props.onClick('workout'); 
    }

    togglePanelToSettings = () => {
        this.setState({ SidebarIsShown: false , whichPanel: 'settings' });
        this.props.onClick('settings'); 
    }

    render() {
        return (
            <div>
                <i className="material-icons menu-icon " onClick={this.showSidebar}>reorder</i>
                <div className={"sidebar " + (this.state.SidebarIsShown ? 'sidebar-show' : 'sidebar-hide') }>
                    <div className="close-box">
                        <i className="material-icons close-icon" onClick={this.hideSidebar}>close</i><br />
                    </div>

                    <div className="hero-box">
                        <div className="hero-text">
                            <h1>باشگاه آرتان</h1>
                        </div>
                    </div>

                    <div 
                    className={"item-box food-box " + (this.state.whichPanel === 'food' ? 'show-box' : '')}
                    onClick={this.togglePanelToFood}
                    >
                        <i className="material-icons item-icon">fastfood</i>
                        <span>برنامه غذایی</span>
                    </div>

                    <div 
                    className={"item-box workout-box " + (this.state.whichPanel === 'workout' ? 'show-box' : '')}
                    onClick={this.togglePanelToWorkout}
                    >
                        <i className="material-icons item-icon">fitness_center</i>
                        <span>برنامه ورزشی</span>
                    </div>

                    <div 
                    className={"item-box settings-box " + (this.state.whichPanel === 'settings' ? 'show-box' : '')}
                    onClick={this.togglePanelToSettings}
                    >
                        <i className="material-icons item-icon">settings</i>
                        <span>تنظیمات</span>
                    </div>
                </div>
            </div>
        );
    }

}

export default Sidebar;