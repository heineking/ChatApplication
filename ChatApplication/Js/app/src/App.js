import React, { Component } from 'react';
import NavBar from './header/NavBar';
import AppBar from 'material-ui/AppBar';
import RoomList from './rooms/RoomList';
import './App.css';

class App extends Component {
  render() {
    return (
      <div className="App">
        <div className="App-header">
          <NavBar />
        </div>
        <p className="App-intro">
          <RoomList />
        </p>
      </div>
    );
  }
}

export default App;
