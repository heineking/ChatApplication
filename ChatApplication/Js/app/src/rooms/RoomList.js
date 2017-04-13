import React from 'react';
import Paper from 'material-ui/Paper';
import Divider from 'material-ui/Divider';
import './RoomList.css';

const RoomCard = ({ room }) => {
  return (
    <div className="room-card">
      <div className="top">
        <span className="title">Room Subject</span>
        <span className="bullet">&#8226;</span>
        <span className="date">4h</span>
        <span className="bullet">&#8226;</span>
        <span className="user">u/User1</span>
      </div>
      <div className="main">
        Lorem ipsum dolor sit amet, consectetur adipiscing
        elit, sed do eiusmod tempor.
      </div>
      <div className="bottom">
        <span>(icon)</span>
        <span className="comments">534 Comments</span>
      </div>
    </div>
  );
};

const RoomList = ({ rooms }) => {
  return (
    <div>
      <Divider />
      <RoomCard />
      <Divider />
      <RoomCard />
      <Divider />
    </div>
  );
};

export default RoomList;
