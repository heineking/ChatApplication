import { combineReducers } from 'redux';
import { CALL_API } from 'redux-api-middleware';
import createApiReducer from './api';

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

const REQUEST_MESSAGE_CREATE = 'rooms/api/REQUEST_MESSAGE_CREATE';
const CREATE_MESSAGE_SUCCESS = 'rooms/api/CREATE_MESSAGE_SUCCESS';
const CREATE_MESSAGE_FAILURE = 'rooms/api/CREATE_MESSAGE_FAILURE';

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
        CREATE_MESSAGE_FAILURE
      ]
    }
  };
}

const DELETE_ROOM = 'rooms/api/DELETE_ROOM';
const DELETE_ROOM_SUCCESS = 'rooms/api/DELETE_ROOM_SUCCESS';
const DELETE_ROOM_FAILURE = 'rooms/api/DELETE_ROOM_FAILURE';

export const deleteRoomAction = (roomId) => {
  return {
    [CALL_API]: {
      endpoint: `http://localhost:64784/api/v1/rooms/delete/${roomId}`,
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
        Authorization: sessionStorage.getItem('auth'),
        Accept: 'application/json',
      },
      types: [
        DELETE_ROOM,
        DELETE_ROOM_SUCCESS,
        DELETE_ROOM_FAILURE
      ]
    }
  };
}

export const getRoomById = (state, roomId) => state.find(room => room.roomId === Number(roomId)) || {};

const behaviors = {
  [ROOMS_SUCCESS](state, action) {
    const { payload: { rooms } } = action;
    return rooms;
  },
  [ROOM_SUCCESS](state, action) {
    const { payload: { roomId  } } = action;
    const roomIdx = state.findIndex(room => room.roomId === roomId);
    const { payload: { room: newRoom } } = action;
    if (roomIdx === -1) {
      return state.concat(newRoom);
    }
    return [
      ...state.slice(0, roomIdx),
      newRoom,
      ...state.slice(roomIdx + 1)
    ];
  },
  [DELETE_ROOM_SUCCESS](state, action) {
    const { payload: { roomId } } = action;
    return state.filter(room => room.roomId !== roomId);
  },
  [CREATE_MESSAGE_SUCCESS](state, action) {
    const { payload: { roomId } } = action;
    const roomIdx = state.findIndex(room => room.roomId === roomId);
    const { payload: { message } } = action;
    if (roomIdx > -1) {
      const room = state[roomIdx];
      return [
        ...state.slice(0, roomIdx),
        {
          ...room,
          messages: [
            ...room.messages,
            message
          ]
        },
        ...state.slice(roomIdx + 1)
      ];
    }
    throw new Error(`Expected to find room with id ${roomId}`);
  }
};

const roomsReducer = (state = [], action = {}) => {
  const behavior = behaviors[action.type];
  return behavior ? behavior(state, action) : state;
};

export default combineReducers({
  rooms: roomsReducer,
  /* GET reducers */
  getRooms: createApiReducer([ROOMS_REQUEST, ROOMS_SUCCESS, ROOMS_FAILURE]),
  getRoom: createApiReducer([ROOM_REQUEST, ROOM_SUCCESS, ROOM_FAILURE]),
  /* POST reducers */
  postMessage: createApiReducer([REQUEST_MESSAGE_CREATE, CREATE_MESSAGE_SUCCESS, CREATE_MESSAGE_FAILURE]),
  postRoom: createApiReducer([CREATE_ROOM_REQUEST, CREATE_ROOM_SUCCESS, CREATE_ROOM_FAILURE]),
  /* Delete reducers */
  deleteRoom: createApiReducer([DELETE_ROOM, DELETE_ROOM_SUCCESS, DELETE_ROOM_FAILURE])
});
