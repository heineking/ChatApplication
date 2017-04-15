import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';
import { loginAction } from '../redux/reducers/login';

class Login extends Component {
  constructor() {
    super();
    this.handleLogin = this.handleLogin.bind(this);
  }
  handleLogin(e) {
    e.preventDefault();
    const { userInput, passwordInput } = this;
    const { dispatch } = this.props;

    const user = userInput.input;
    const password = passwordInput.input;
    dispatch(loginAction(user.value, password.value));
    user.value = '';
    password.value = '';
    return false;
  }
  render() {
    const { api: { requesting, failure }, user: { userId } } = this.props;
    return (
      <form onSubmit={e => this.handleLogin(e)}>
        <div style={{ textAlign: 'center', margin: 20 }}>
          <div>
            <TextField
              required
              ref={(input) => { this.userInput = input; }}
              floatingLabelText="Username"
            />
            <TextField
              ref={(input) => { this.passwordInput = input; }}
              required
              floatingLabelText="Password"
              type="password"
            />
          </div>
          <div style={{ marginTop: 20 }}>
            {!requesting &&
              <RaisedButton
                type="submit"
                primary
                label="Login"
              />
            }
            {failure && <div style={{ color: 'red' }}>Login Failed!</div>}
          </div>
        </div>
        {userId && <Redirect to="/" />}
      </form>
    );
  }
}

const mapStateToProps = state => ({
  ...state.login
});

export default connect(mapStateToProps)(Login);
