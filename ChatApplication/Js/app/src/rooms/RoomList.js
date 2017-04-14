import React from 'react';
import Paper from 'material-ui/Paper';
import Divider from 'material-ui/Divider';
import './RoomList.css';

const RoomCard = ({ room }) => {
  const { name, description, dateCreated, messageCount, userName } = room;
  return (
    <div className="room-card">
      <div className="top">
        <span className="title">{name}</span>
        <span className="bullet">&#8226;</span>
        <span className="date">{new Date(dateCreated).toLocaleDateString()}</span>
        <span className="bullet">&#8226;</span>
        <span className="user">{userName}</span>
      </div>
      <div className="main">
        {description}
      </div>
      <div className="bottom">
        <span className="comments">{messageCount} Comments</span>
      </div>
    </div>
  );
};

const RoomList = ({ rooms }) => {
  return (
    <div>
      {rooms.map(room =>
        <div>
          <RoomCard {...{ room }} />
          <Divider />
        </div>
      )}
    </div>
  );
};

export default RoomList;
