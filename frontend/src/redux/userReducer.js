import { LOGIN, LOGOUT, USER_LOADING } from "./actionTypes";
// Redux Tutorial - Beginner to Advanced - YouTube. [no date]. Available at: https://www.youtube.com/watch?v=zrs7u6bdbUw [Accessed: 14 September 2022].
// Sharma, R. 2022. How to use Redux Hooks in a React Native App (Login, Logout Example). Available at: https://blog.bitsrc.io/how-to-use-redux-hooks-in-a-react-native-app-login-logout-example-6dee84dee51b [Accessed: 14 September 2022].

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
