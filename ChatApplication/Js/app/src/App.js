import React, { Component } from 'react';
import {
  BrowserRouter as Router,
  Route
} from 'react-router-dom';
import NavBar from './header/NavBar';
import NewRoom from './room/NewRoom';
import RoomList from './rooms/RoomList';
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
            <Route exact path="/" component={RoomList} />
            <Route path="/new-post" component={NewRoom} />
            <Route path="/login" component={Login} />
          </div>
        </div>
      </Router>
    );
  }
}

export default App;
