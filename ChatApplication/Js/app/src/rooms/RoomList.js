import React from 'react';
import { withRouter, Route, Link } from 'react-router-dom';
import Paper from 'material-ui/Paper';
import Divider from 'material-ui/Divider';
import './RoomList.css';

const navigateToRoom = (history, roomId) => history.push(`/room/${roomId}`);

const RoomCard = withRouter(({ room, history }) => {
  const { name, roomId, description, dateCreated, messageCount, userName } = room;
  return (
    <div
      className="room-card"
      onClick={() => navigateToRoom(history, roomId)}
    >
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
});

const RoomList = ({ rooms }) => {
  return (
    <div>
      {rooms.map(room =>
        <div key={room.roomId}>
          <RoomCard {...{ room }} />
          <Divider />
        </div>
      )}
    </div>
  );
};

export default RoomList;
