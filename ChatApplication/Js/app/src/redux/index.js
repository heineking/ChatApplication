import { createStore, applyMiddleware, compose } from 'redux';
import { apiMiddleware } from 'redux-api-middleware';
import reducer from './reducers/index';

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;

const middleware = [
  apiMiddleware
];

const store = createStore(
  reducer,
  composeEnhancers(
    applyMiddleware(...middleware)
  ),  
);

export default store;
