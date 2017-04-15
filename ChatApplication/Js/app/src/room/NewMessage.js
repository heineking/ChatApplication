import React, { Component } from 'react';
import TextField from 'material-ui/TextField';

class NewMessage extends Component {
  constructor() {
    super();
    this.bindMessage = this.bindMessage.bind(this);
  }
  bindMessage(input) {
    if (!input) return;
    const { input: { refs: { input: msgInput } } } = input;
    const { bindText } = this.props;
    bindText(msgInput);
  }
  render() {
    return (
      <div style={{ margin: 15 }}>
        <TextField
          ref={input => { this.bindMessage(input) } }
          floatingLabelText="Message"
          hintText="e.g., Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
            tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam"
          rows={4}
          floatingLabelFixed
          fullWidth
          multiLine
        />
      </div>
    );
  }
}

export default NewMessage;
