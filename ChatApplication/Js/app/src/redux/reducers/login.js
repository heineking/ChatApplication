import { combineReducers } from 'redux';
import createApiReducer from './api';
import { CALL_API } from 'redux-api-middleware';

const LOGIN_REQUEST = 'login/api/REQUESTING_LOGIN';
const LOGIN_SUCCESSFUL = 'login/api/LOGIN_SUCCESSFUL';
const LOGIN_FAILURE = 'login/api/LOGIN_FAILURE';

export const loginAction = (email, password) => {
  sessionStorage.removeItem('auth');
  return {
    [CALL_API]: {
      endpoint: 'http://localhost:64784/api/v1/auth/login.json',
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ email, password }),
      types: [
        LOGIN_REQUEST,
        LOGIN_SUCCESSFUL,
        LOGIN_FAILURE
      ]}
  };
}

const LOGOUT = 'login/LOGOUT';

export const logOutAction = () => {
  return ({
    type: LOGOUT,
    payload: null
  });
};

const defaultUser = {
  userName: '',
  userId: '',
  isAdmin: false
};

const user = (state = defaultUser, action) => {
  switch (action.type) {
    case LOGIN_SUCCESSFUL:
      const { payload: { encodedToken, user } } = action;
      sessionStorage.setItem('auth', encodedToken);
      return {
        ...state,
        ...user
      };
    case LOGIN_FAILURE:
    case LOGOUT:
      sessionStorage.removeItem('auth');
      return {
        ...state,
        ...defaultUser
      };
    default:
      return state;
  }
}

export default combineReducers({
  api: createApiReducer([LOGIN_REQUEST, LOGIN_SUCCESSFUL, LOGIN_FAILURE]),
  user
});
