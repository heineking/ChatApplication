import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Redirect } from 'react-router-dom';
import RaisedButton from 'material-ui/RaisedButton';
import Paper from 'material-ui/Paper';
import TextField from 'material-ui/TextField';
import { createRoomAction } from '../redux/reducers/rooms';
import { resetApiAction } from '../redux/reducers/api';
import './NewRoom.css';

class NewRoom extends Component {
  constructor() {
    super();
    this.handlePost = this.handlePost.bind(this);
  }
  componentWillUnmount() {
    const { dispatch } = this.props;
    dispatch(resetApiAction());
  }
  handlePost(e) {
    e.preventDefault();
    const { subject, description: { input: { refs: { input: descInput } } } } = this;
    const { dispatch } = this.props;
    const subjectInput = subject.input;
    dispatch(createRoomAction(subjectInput.value, descInput.value));
    return false;
  }
  render() {
    const { posting: { requesting, success, failure } } = this.props;
    return (
      <div className="New-Room">
        <header>
          <h2>New Post</h2>
        </header>
        <form onSubmit={e => this.handlePost(e)}>
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
          {!requesting &&
            <RaisedButton
              type="submit"
              primary
              label="Post"
            />
          }
          {failure && <div style={{ color: 'red' }}>Post Failed!</div>}
          {success && <Redirect to="/" />}
        </form>
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    posting: state.rooms.postRoom
  };
}

export default connect(mapStateToProps)(NewRoom);
