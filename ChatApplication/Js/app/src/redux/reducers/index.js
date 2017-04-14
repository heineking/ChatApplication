import { combineReducers } from 'redux';
import login from './login';
import rooms from './rooms';

export default combineReducers({
  login,
  rooms
});
