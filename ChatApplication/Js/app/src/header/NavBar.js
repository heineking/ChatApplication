import React, { Component } from 'react';
import AppBar from 'material-ui/AppBar';
import Divider from 'material-ui/Divider';
import IconMenu from 'material-ui/IconMenu';
import IconButton from 'material-ui/IconButton';
import MenuItem from 'material-ui/MenuItem';
import Toggle from 'material-ui/Toggle';
import MoreVertIcon from 'material-ui/svg-icons/navigation/more-vert';
import NavigationClose from 'material-ui/svg-icons/navigation/close';
import FlatButton from 'material-ui/FlatButton';

const Login = (props) => {
  return (<FlatButton {...props} label="Login" />);
};
Login.muiName = 'FlatButton';

const Logged = (props) => {
  return (
    <IconMenu
      {...props}
      iconButtonElement={
        <IconButton><MoreVertIcon /></IconButton>
      }
      targetOrigin={{horizontal: 'right', vertical: 'top'}}
      anchorOrigin={{horizontal: 'right', vertical: 'top'}}
    >
      <MenuItem primaryText="Refresh" />
      <MenuItem primaryText="New Post" />
      <Divider />
      <MenuItem primaryText="Sign out" />
    </IconMenu>
  );
};

Logged.muiName = 'IconMenu';

const NavBar = () => {
  return (
    <AppBar
      title="Chat Application"
      iconElementRight={<Logged />}
    />
  );
};

export default NavBar;
