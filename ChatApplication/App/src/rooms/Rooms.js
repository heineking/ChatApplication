import React, { Component } from 'react';
import { connect } from 'react-redux';
import { getRoomsAction } from '../redux/reducers/rooms';
import RoomList from './RoomList';

class Rooms extends Component {
  componentDidMount() {
    const { dispatch } = this.props;
    dispatch(getRoomsAction());
  }
  render() {
    const { rooms } = this.props;
    return (
      <RoomList {...{ rooms }} />
    );
  }
}

const mapStateToProps = state => {
  return ({
    rooms: state.rooms.rooms
  });
};

export default connect(mapStateToProps)(Rooms);
