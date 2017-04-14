import React, { Component } from 'react';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';

class Login extends Component {
  constructor() {
    super();
    this.handleLogin = this.handleLogin.bind(this);
  }
  handleLogin() {
    const { userInput, passwordInput } = this;
    const user = userInput.input;
    const password = passwordInput.input;
    console.log(`username: ${user.value}, pwd: ${password.value}`)
  }
  render() {
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
            <RaisedButton
              onClick={this.handleLogin}
              primary
              label="Login"
            />
          </div>
        </div>
      </form>
    );
  }
}

export default Login;
