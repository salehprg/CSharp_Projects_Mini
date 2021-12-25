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


const AdminNavbar = (props) => {
  const [isOpen, setIsOpen] = useState(false);

  const toggle = () => setIsOpen(!isOpen);

  return (
    <div id="NavTop" className="fixed-top">
      <Navbar color="light" light expand="md" className="navbar">
        <NavbarBrand href="/Admin" style={{color : "black", fontSize: "120%"}}> سیستم مدیریت باشگاه ورزشی آرتان</NavbarBrand>
      </Navbar>
    </div>
  );
}

export default AdminNavbar;
