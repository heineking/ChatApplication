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

const ROOM_REQUEST = 'rooms/api/ROOM_REQUEST';
const ROOM_SUCCESS = 'rooms/api/ROOM_SUCCESS';
const ROOM_FAILURE = 'rooms/api/ROOM_FAILURE';

export const getRoomAction = (roomId) => {
  return {
    [CALL_API]: {
      endpoint: `http://localhost:64784/api/v1/rooms/${roomId}`,
      method: 'GET',
      headers: {
        'Content-Type': 'application/json'
      },
      types: [ROOM_REQUEST, ROOM_SUCCESS, ROOM_FAILURE]
    }
  }
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
};

const REQUEST_MESSAGE_CREATE = 'login/api/REQUEST_MESSAGE_CREATE';
const CREATE_MESSAGE_SUCCESS = 'login/api/CREATE_MESSAGE_SUCCESS';
const CREATE_MESSAGE_FAILURE = 'login/api/CREATE_MESSAGE_FAILURE';

export const createMessageAction = (message, roomId) => {
  return {
    [CALL_API]: {
      endpoint: `http://localhost:64784/api/v1/rooms/${roomId}`,
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: sessionStorage.getItem('auth'),
        Accept: 'application/json'
      },
      body: JSON.stringify({ text: message }),
      types: [
        REQUEST_MESSAGE_CREATE,
        CREATE_MESSAGE_SUCCESS,
        CREATE_ROOM_FAILURE
      ]
    }
  };
}

export const getRoomById = (state, roomId) => state.find(room => room.roomId === Number(roomId)) || {};

const rooms = (state = [], action) => {
  switch (action.type) {
    case ROOMS_SUCCESS:
      const { payload: { rooms } } = action;
      return rooms;
    case ROOM_SUCCESS:
        const { payload: { room: newRoom, roomId } } = action;
        const roomIdx = state.findIndex(room => room.roomId === roomId);
        if (roomIdx === -1) {
          return state.concat(newRoom);
        }
        return [
          ...state.slice(0, roomIdx),
          newRoom,
          ...state.slice(roomIdx + 1)
        ];
    default:
      return state;
  }
};

export default combineReducers({
  rooms
});
