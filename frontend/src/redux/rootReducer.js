import { combineReducers } from "redux";
import errorReducer from "./errorReducer";
import userReducer from "./userReducer";
// Redux Tutorial - Beginner to Advanced - YouTube. [no date]. Available at: https://www.youtube.com/watch?v=zrs7u6bdbUw [Accessed: 14 September 2022].
// Sharma, R. 2022. How to use Redux Hooks in a React Native App (Login, Logout Example). Available at: https://blog.bitsrc.io/how-to-use-redux-hooks-in-a-react-native-app-login-logout-example-6dee84dee51b [Accessed: 14 September 2022].

export default combineReducers({
  userReducer,
  errorReducer,
});
