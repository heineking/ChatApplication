import React, { Component } from 'react';
import { connect } from 'react-redux';
import MessageList from './MessageList';
import NewMessage from './NewMessage';
import {
  getRoomById,
  getRoomAction
} from '../redux/reducers/rooms';
import './Room.css';

class Room extends Component {
  componentDidMount() {
    const { match: { params: { id } }, dispatch } = this.props;
    dispatch(getRoomAction(id));
  }
  render() {
    const { room, match: { params: { id } } } = this.props;

    return (
      <div className="room">
        <header>
          <h2>{room.name}</h2>
        </header>
        <MessageList messages={room.messages} />
        <NewMessage
          bindText={this.refMessage}
          roomId={id}
        />
      </div>
    );
  }
}

const mapStateToProps = (state, ownProps) => {
  const { match: { params: { id } } } = ownProps;
  return {
    room: getRoomById(state.rooms.rooms, id)
  };
}

export default connect(mapStateToProps)(Room);
