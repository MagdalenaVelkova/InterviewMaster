import { LOGIN, LOGOUT, USER_LOADING } from "./actionTypes";

/**
 * It holds the default state of a user
 */
export const initialState = {
  isAuthenticated: false,
  loading: true,
};

/**
 * It updates the state object of the to either authenticated or not authenticated
 * @param 'initialState' - The current state of the user
 * @param 'action' - The action dispatched
 */
const userReducer = (state = initialState, action) => {
  switch (action.type) {
    case LOGIN:
      return {
        ...state,
        isAuthenticated: true,
        loading: false,
      };
    case LOGOUT:
      return {
        initialState,
      };
    case USER_LOADING:
      return {
        ...state,
        loading: true,
        isAuthenticated: false,
      };
    default:
      return state;
  }
};

export default userReducer;
