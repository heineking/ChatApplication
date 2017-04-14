import { combineReducers } from 'redux';
import { CALL_API } from 'redux-api-middleware';

const ROOMS_REQUEST = 'rooms/api/REQUEST_ROOMS';
const ROOMS_SUCCESS = 'rooms/api/SUCCESSFUL_ROOM';
const ROOMS_FAILURE = 'rooms/api/FAILURE_ROOM';

export const getRoomsAction = () => {
  return {
    [CALL_API]: {
      endpoint: 'http://localhost:64784/api/v1/rooms/all.json',
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
      types: [ROOMS_REQUEST, ROOMS_SUCCESS, ROOMS_FAILURE]
    }
  };
};


const CREATE_ROOM_REQUEST = 'rooms/api/CREATE_ROOM_REQUEST';
const CREATE_ROOM_SUCCESS = 'rooms/api/CREATE_ROOM_SUCCESS';
const CREATE_ROOM_FAILURE = 'rooms/api/CREATE_ROOM_FAILURE';

export const createRoomAction = (name, description) => {
  return {
    [CALL_API]: {
      endpoint: 'http://localhost:64784/api/v1/rooms/create',
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: sessionStorage.getItem('auth'),
        Accept: 'application/json'
      },
      body: JSON.stringify({ name, description }),
      types: [
        CREATE_ROOM_REQUEST,
        CREATE_ROOM_SUCCESS,
        CREATE_ROOM_FAILURE
      ]
    }
  };
}

const rooms = (state = [], action) => {
  switch (action.type) {
    case ROOMS_SUCCESS:
      const { payload: { rooms } } = action;
      return rooms;
    default:
      return state;
  }
};

export default combineReducers({
  rooms
});
