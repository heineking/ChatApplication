import React, { Component } from 'react';
import { connect } from 'react-redux';
import RaisedButton from 'material-ui/RaisedButton';
import MessageList from './MessageList';
import NewMessage from './NewMessage';
import {
  getRoomById,
  getRoomAction,
  createMessageAction
} from '../redux/reducers/rooms';
import './Room.css';

class Room extends Component {
  constructor() {
    super();
    this.state = {
      newMessage: false
    };
    this.refMessage = this.refMessage.bind(this);
    this.handlePostMessage = this.handlePostMessage.bind(this);
    this.handleNewClick = this.handleNewClick.bind(this);
  }
  componentDidMount() {
    const { match: { params: { id } }, dispatch } = this.props;
    dispatch(getRoomAction(id));
  }
  handleNewClick() {
    this.setState({
      newMessage: !this.state.newMessage
    });
  }
  handlePostMessage() {
    const { messageInput } = this;
    const { dispatch, match: { params: { id } } } = this.props;
    dispatch(createMessageAction(messageInput.value, id));
    this.setState({ newMessage: false });
  }
  refMessage(input) {
    this.messageInput = input;
  }
  render() {
    const { match: { params }, room } = this.props;
    const { newMessage } = this.state;

    return (
      <div className="room">
        <header>
          <h2>{room.name}</h2>
        </header>
        <MessageList messages={room.messages} />
        {newMessage &&
          <NewMessage
            bindText={this.refMessage}
          />
        }
        <div
          style={{ margin: 10 }}
          className="actions"
        >
          {newMessage &&
            <RaisedButton
              style={{ marginRight: 10 }}
              onClick={() => this.handlePostMessage()}
              label="Post"
              primary
            />
          }
          <RaisedButton
            onClick={() => this.handleNewClick()}
            label={newMessage ? 'Cancel' : 'New'}
            secondary={newMessage}
            primary={!newMessage}
          />
        </div>
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
