import './AdminNavbarRight.css'
import React, { useState } from 'react';
import {
  Collapse,
  Navbar,
  NavbarToggler,
  NavbarBrand,
  Nav,
  NavItem,
  NavLink,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  NavbarText
} from 'reactstrap';
import styled from 'styled-components'
import { Link } from 'react-router-dom';

const Styles = {
    position : 'fixed !important',
    height: '100%',
    flexDirection : 'column',
    right : '0'
};

const AdminNavbarRight = (props) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <i className="material-icons menu-icon admin-menu-icon" onClick={toggle}>reorder</i>
      <div className={"sidebar " + (isOpen ? "sidebar-show" : "sidebar-hide")}>
        <div className="close-box admin-close-box">
          <i className="material-icons close-icon" onClick={toggle}>close</i><br />
        </div>

        <div 
          className="item-box admin-food-box"
          >
          <Link className="goto" to="/Admin/Meals">وعده ها</Link>
        </div>

        <div 
          className="item-box admin-food-box"
          >
          <Link className="goto" to="/Admin/Excercises">ورزش ها</Link>
        </div>
      </div>
    </div>
    // <div id="NavRight" style={{width : "250px" , position : "fixed" , height : "100%" , top : "50px" , zIndex : "100"}}>
    //   <Navbar color="dark" light expand="md" style={Styles} className="col-lg-10">
        
    //     <NavbarToggler onClick={toggle} />
    //       <Collapse isOpen={isOpen} navbar className="flex-column">
          
    //       <Nav className="mr-auto" navbar vertical >
    //         <NavItem>
    //           <NavLink href="/Admin/Meals" style={{color : "white"}}>وعده ها</NavLink>
    //         </NavItem>
    //         <NavItem>
    //           <NavLink href="/Admin/Excercises" style={{color : "white"}}>ورزش ها</NavLink>
    //         </NavItem>
    //       </Nav>
    //     </Collapse>
    //   </Navbar>
    //   </div>
  );
}

export default AdminNavbarRight;
