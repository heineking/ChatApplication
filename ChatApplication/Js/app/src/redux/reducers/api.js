const apiState = {
  requesting: false,
  success: false,
  failure: false,
};

const RESET_API = 'api/RESET_API';

export const resetApiAction = () => {
  return ({
    type: RESET_API
  });
}

const genericBehaviors = (types = []) => {
  const [ REQUESTING, SUCCESS, FAILURE ] = types;
  return {
    [RESET_API](state, action) {
      if (state.success) {
        return apiState;
      }
      return state;
    },
    [REQUESTING](state, action) {
      return {
        ...state,
        success: false,
        failure: false,
        requesting: true
      };
    },
    [SUCCESS](state, action) {
      return {
        ...state,
        requesting: false,
        success: true
      };
    },
    [FAILURE](state, action) {
      const { payload: error } = action;
      return {
        ...state,
        requesting: false,
        success: false,
        failure: true,
        error
      };
    }
  };
}

const createReducer = (types) => {
  const behaviors = genericBehaviors(types);
  return (state = apiState, action = {}) => {
    const behavior = behaviors[action.type];
    return behavior ? behavior(state, action) : state;
  };
};

export default createReducer;
