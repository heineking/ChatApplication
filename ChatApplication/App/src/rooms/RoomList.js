import React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { deleteRoomAction } from '../redux/reducers/rooms';
import Divider from 'material-ui/Divider';
import './RoomList.css';

const navigateToRoom = (history, roomId) => history.push(`/room/${roomId}`);

const RoomCard = withRouter(({ room, history, deleting, isAdmin, handleDelete }) => {
  const { requesting } = deleting;
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
        {(!requesting) &&
          <span
            className="delete"
            onClick={(e) => handleDelete(e)}
          >
            delete
          </span>}
      </div>
    </div>
  );
});

const createDelete = (dispatch, roomId) => (e) => {
  e.preventDefault();
  e.stopPropagation();
  dispatch(deleteRoomAction(roomId));
}

const RoomList = (props) => {
  const { rooms, deleting, isAdmin, dispatch } = props;
  return (
    <div>
      {rooms.map(room =>
        <div key={room.roomId}>
          <RoomCard
            {...{
              room,
              isAdmin,
              deleting,
              handleDelete: createDelete(dispatch, room.roomId)
            }}
          />
          <Divider />
        </div>
      )}
    </div>
  );
};

const mapStateToProps = state => {
  return {
    deleting: state.rooms.deleteRoom,
    isAdmin: state.login.user.isAdmin
  };
};

export default connect(mapStateToProps)(RoomList);
