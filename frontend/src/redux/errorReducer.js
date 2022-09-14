import { ADD_ERROR, REMOVE_ERROR } from "./actionTypes";
// Redux Tutorial - Beginner to Advanced - YouTube. [no date]. Available at: https://www.youtube.com/watch?v=zrs7u6bdbUw [Accessed: 14 September 2022].
// Sharma, R. 2022. How to use Redux Hooks in a React Native App (Login, Logout Example). Available at: https://blog.bitsrc.io/how-to-use-redux-hooks-in-a-react-native-app-login-logout-example-6dee84dee51b [Accessed: 14 September 2022].

/**
 * It holds the default state of a user
 */
export const initialState = {
  errorMessages: [],
};

/**
 * It updates the state object of the to either authenticated or not authenticated
 * @param 'initialState' - The current state of the user
 * @param 'action' - The action dispatched
 */
const errorReducer = (state = initialState, action) => {
  switch (action.type) {
    case ADD_ERROR:
      return {
        ...state,
        errorMessages: [...state.errorMessages, action.value],
      };
    case REMOVE_ERROR:
      return {
        ...state,
        errorMessages: state.errorMessages.filter(
          (item) => item !== action.value
        ),
      };
    default:
      return state;
  }
};

export default errorReducer;
