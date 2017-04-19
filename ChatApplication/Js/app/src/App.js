import React, { Component } from 'react';
import {
  BrowserRouter as Router,
  Route
} from 'react-router-dom';
import NavBar from './header/NavBar';
import NewRoom from './room/NewRoom';
import Room from './room/Room';
import Rooms from './rooms/Rooms';
import Login from './login/Login';
import './App.css';

class App extends Component {
  componentDidMount() {
    sessionStorage.removeItem('auth');
  }
  render() {
    return (
      <Router>
        <div className="App">
          <div className="App-header">
            <NavBar />
          </div>
          <div className="App-intro">
            <Route exact path="/" component={Rooms} />
            <Route path="/room/:id" component={Room} />
            <Route path="/login" component={Login} />

            <Route path="/new-post" component={NewRoom} />
          </div>
        </div>
      </Router>
    );
  }
}

export default App;
