import React, { Component } from 'react';
import { connect } from 'react-redux';
import AppBar from 'material-ui/AppBar';
import { Link } from 'react-router-dom';
import Divider from 'material-ui/Divider';
import IconMenu from 'material-ui/IconMenu';
import IconButton from 'material-ui/IconButton';
import MenuItem from 'material-ui/MenuItem';
import Toggle from 'material-ui/Toggle';
import MoreVertIcon from 'material-ui/svg-icons/navigation/more-vert';
import NavigationClose from 'material-ui/svg-icons/navigation/close';
import FlatButton from 'material-ui/FlatButton';
import './NavBar.css';

const Login = (props) => {
  return (
    <Link to="/login">
      <FlatButton {...props} label="Login" />
    </Link>
  );
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
      <Link to="/new-post" className="logged-action">
        <MenuItem primaryText="New Post" />
      </Link>
      <Divider />
      <MenuItem primaryText="Sign out" />
    </IconMenu>
  );
};

Logged.muiName = 'IconMenu';

const NavBar = (props) => {
  const { loggedIn, ...passProps } = props;
  return (
    <AppBar
      title={
        <Link className="header" to="/">Chat Application</Link>
      }
      iconElementRight={loggedIn ? <Logged {...passProps} /> : <Login />}
    />
  );
};

const mapStateToProps = (state) => {
  return {
    ...state.login.api,
    ...state.login.user
  };
}

export default connect(mapStateToProps)(NavBar);
