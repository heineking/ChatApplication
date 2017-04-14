import React, { Component } from 'react';
import {
  BrowserRouter as Router,
  Route
} from 'react-router-dom';
import NavBar from './header/NavBar';
import NewRoom from './room/NewRoom';
import Rooms from './rooms/Rooms';
import LoginRedirect from './login/LoginRedirect';
import Login from './login/Login';
import './App.css';

class App extends Component {
  render() {
    return (
      <Router>
        <div className="App">
          <div className="App-header">
            <NavBar />
          </div>
          <div className="App-intro">
            <Route path="/login" component={Login} />
            <Route exact path="/" component={Rooms} />
            <Route path="/new-post" component={NewRoom} />
          </div>
        </div>
      </Router>
    );
  }
}

export default App;
