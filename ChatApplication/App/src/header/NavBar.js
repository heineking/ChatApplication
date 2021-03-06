import React from 'react';
import { connect } from 'react-redux';
import AppBar from 'material-ui/AppBar';
import { Link } from 'react-router-dom';
import Divider from 'material-ui/Divider';
import IconMenu from 'material-ui/IconMenu';
import IconButton from 'material-ui/IconButton';
import MenuItem from 'material-ui/MenuItem';
import MoreVertIcon from 'material-ui/svg-icons/navigation/more-vert';
import FlatButton from 'material-ui/FlatButton';
import { logOutAction } from '../redux/reducers/login';
import './NavBar.css';

const Login = (props) => {
  return (
    <Link to="/login">
      <FlatButton {...props} label="Login" />
    </Link>
  );
};
Login.muiName = 'FlatButton';

const Logged = connect()((props) => {
  const { dispatch, ...passProps } = props;
  return (
    <IconMenu
      {...passProps}
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
      <MenuItem
        onClick={e => dispatch(logOutAction())}
        primaryText="Log Out"
      />
    </IconMenu>
  );
});

Logged.muiName = 'IconMenu';

const NavBar = (props) => {
  /* eslint-disable */
  const { api, user: { userId }, ...passProps } = props;
  /* eslint-enable */
  return (
    <AppBar
      title={
        <Link className="header" to="/">Chat Application</Link>
      }
      iconElementRight={userId ? <Logged {...passProps} /> : <Login />}
    />
  );
};

const mapStateToProps = (state) => {
  return {
    ...state.login,
  };
}

export default connect(mapStateToProps)(NavBar);
