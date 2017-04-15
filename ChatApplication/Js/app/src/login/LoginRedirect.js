import React from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router';

const LoginRedirect = ({ loggedIn }) => (
  <div>
    {!loggedIn &&
      <Redirect to="/login" from={window.location} />
    }
  </div>
);

const mapStateToProps = state => ({
  loggedIn: state.login.api.loggedIn
});

export default connect(mapStateToProps)(LoginRedirect);
