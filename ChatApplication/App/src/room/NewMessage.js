import React, { Component } from 'react';
import { connect } from 'react-redux';
import RaisedButton from 'material-ui/RaisedButton';
import TextField from 'material-ui/TextField';
import {
  createMessageAction
} from '../redux/reducers/rooms';
class NewMessage extends Component {
  constructor() {
    super();
    this.bindMessage = this.bindMessage.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }
  bindMessage(input) {
    if (!input) return;
    const { input: { refs: { input: msgInput } } } = input;
    this.msgInput = msgInput;
  }
  handleSubmit(e) {
    e.preventDefault();
    const { msgInput } = this;
    const { dispatch, roomId } = this.props;
    dispatch(createMessageAction(msgInput.value, roomId));
    msgInput.value = '';
    return false;
  }
  render() {
    return (
      <form style={{ margin: 15 }} onSubmit={e => this.handleSubmit(e)}>
        <TextField
          ref={input => { this.bindMessage(input) } }
          floatingLabelText="Message"
          hintText="e.g., Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
            tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam"
          rows={4}
          floatingLabelFixed
          fullWidth
          multiLine
          required
        />
        <RaisedButton
          label="Post"
          type="submit"
          primary
        />
      </form>
    );
  }
}

export default connect()(NewMessage);
