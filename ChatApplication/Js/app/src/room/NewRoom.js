import React, { Component } from 'react';
import { connect } from 'react-redux';
import RaisedButton from 'material-ui/RaisedButton';
import Paper from 'material-ui/Paper';
import TextField from 'material-ui/TextField';
import { createRoomAction } from '../redux/reducers/rooms';
import './NewRoom.css';

class NewRoom extends Component {
  constructor() {
    super();
    this.handlePost = this.handlePost.bind(this);
  }
  handlePost(e) {
    const { subject, description: { input: { refs: { input: descInput } } } } = this;
    const { dispatch } = this.props;
    const subjectInput = subject.input;
    dispatch(createRoomAction(subjectInput.value, descInput.value));
  }
  render() {
    return (
      <div className="New-Room">
        <header>
          <h2>New Post</h2>
        </header>
        <form>
          <Paper zDepth={1}>
            <div className="content">
              <div className="title">
                <TextField
                  ref={input => { this.subject = input; } }
                  hintText="e.g., Senior Seminar"
                  floatingLabelText="Title"
                  floatingLabelFixed
                  fullWidth
                />
              </div>
              <div className="content">
                <div>
                  <TextField
                    ref={input => { this.description = input; } }
                    floatingLabelText="Description"
                    hintText="e.g., Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
                      tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam"
                    rows={4}
                    floatingLabelFixed
                    fullWidth
                    multiLine
                  />
                </div>
              </div>
            </div>
          </Paper>
          <RaisedButton
            onClick={e => this.handlePost(e)}
            primary
            label="Post"
          />
        </form>
      </div>
    );
  }
}

export default connect()(NewRoom);
