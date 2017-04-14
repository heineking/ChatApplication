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
  componentWillReceiveProps
  handleLogin() {
    const { userInput, passwordInput } = this;
    const { dispatch } = this.props;

    const user = userInput.input;
    const password = passwordInput.input;
    console.log(`username: ${user.value}, pwd: ${password.value}`);
    dispatch(loginAction(user.value, password.value));
  }
  render() {
    const { loggingIn, loggedIn } = this.props;
    return (
      <form>
        <div style={{ textAlign: 'center', margin: 20 }}>
          <div>
            <TextField
              ref={(input) => { this.userInput = input; }}
              floatingLabelText="Username"
            />
            <TextField
              ref={(input) => { this.passwordInput = input; }}
              floatingLabelText="Password"
              type="password"
            />
          </div>
          <div style={{ marginTop: 20 }}>
            {!loggingIn &&
              <RaisedButton
                onClick={this.handleLogin}
                primary
                label="Login"
              />
            }
          </div>
        </div>
        {loggedIn && <Redirect to="/" />}
      </form>
    );
  }
}

const mapStateToProps = state => ({
  ...state.login.api
});

export default connect(mapStateToProps)(Login);
