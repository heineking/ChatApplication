import { combineReducers } from 'redux';
import { CALL_API } from 'redux-api-middleware';

const LOGIN_REQUEST = 'login/api/REQUESTING_LOGIN';
const LOGIN_SUCCESSFUL = 'login/api/LOGIN_SUCCESSFUL';
const LOGIN_FAILURE = 'login/api/LOGIN_FAILURE';

export const loginAction = (email, password) => {
  return {
    [CALL_API]: {
      endpoint: 'http://localhost:64784/api/v1/auth/login.json',
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ email, password }),
      types: [
        {
          type: LOGIN_REQUEST,
          payload: { email }
        },
        LOGIN_SUCCESSFUL,
        LOGIN_FAILURE
      ]}
  };
}

const apiDefault = {
  loggedIn: false,
  loggingIn: false,
  loginError: false
};

const api = (state = apiDefault, action) => {
  switch (action.type) {
    case LOGIN_REQUEST:
      return {
        ...state,
        loggingIn: true
      };
    case LOGIN_SUCCESSFUL:
      const { payload: token } = action;
      sessionStorage.setItem("auth", token);
      return {
        ...state,
        loggingIn: false,
        loggedIn: true
      };
    case LOGIN_FAILURE:
      const { payload: error } = action;
      return {
        ...state,
        loggedIn: false,
        loggingIn: false,
        loginError: true,
        error
      };
    default:
      return state;
  }
};

const defaultUser = {
  user: ''
};

const user = (state = defaultUser, action) => {
  switch (action.type) {
    case LOGIN_REQUEST:
      const { payload: { email } } = action;
      return {
        ...state,
        user: email
      };
    case LOGIN_FAILURE:
      return {
        ...state,
        ...defaultUser
      }
    default:
      return state;
  }
}

export default combineReducers({
  api,
  user
});
