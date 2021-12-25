import React, { useState } from 'react';
import AdminNavbarRight from './AdminNavbarRight'
import AdminNavbarTop from './AdminNavbarTop'
import './AdminLayout.css';

const AdminLayout = (props) => {

  return (
      <div className="mb-4 text-right" style={{direction : "rtl"}}>
          <AdminNavbarTop/>
          <AdminNavbarRight />
          
          {props.children}
      </div>
  );
}

export default AdminLayout;
