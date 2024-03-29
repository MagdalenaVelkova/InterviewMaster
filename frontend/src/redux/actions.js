import axios from "axios";
import {
  ADD_ERROR,
  LOGIN,
  LOGOUT,
  REMOVE_ERROR,
  USER_LOADING,
} from "./actionTypes";
// Redux Tutorial - Beginner to Advanced - YouTube. [no date]. Available at: https://www.youtube.com/watch?v=zrs7u6bdbUw [Accessed: 14 September 2022].
// Sharma, R. 2022. How to use Redux Hooks in a React Native App (Login, Logout Example). Available at: https://blog.bitsrc.io/how-to-use-redux-hooks-in-a-react-native-app-login-logout-example-6dee84dee51b [Accessed: 14 September 2022].

/**
 * Calls the backend to verify that the current user is authenticated with a valid Json Web token.
 * It dispatches a Login and Logout reducer to update the state of the web-client.
 */
export const loginUser = (email, password) => async (dispatch) => {
  try {
    const payload = { email: email, password: password };
    const res = await axios.post(
      "http://localhost:5000/api/users/login",
      payload,
      {
        "Content-Type": "application/json",
      }
    );
    console.log(res.data);
    localStorage.setItem("token", res.data);

    dispatch({ type: LOGIN });
  } catch (error) {
    dispatch(addErrorMessage("Invalid credentials! Please, try again!"));
  }
};

export const registerUser =
  (email, password, firstName, lastName) => async (dispatch) => {
    try {
      const payload = {
        email: email,
        password: password,
        firstName: firstName,
        lastName: lastName,
      };
      const res = await axios.post(
        "http://localhost:5000/api/users/register",
        payload,
        {
          "Content-Type": "application/json",
        }
      );
      dispatch(loginUser(email, password));
      return res;
    } catch (error) {}
  };

export const logoutUser = () => async (dispatch) => {
  localStorage.clear();
  dispatch({ type: LOGOUT });
};

export const validateToken = () => async (dispatch) => {
  try {
    dispatch({ type: USER_LOADING });
    const res = await axios.get("http://localhost:5000/api/Users/authorised");
    dispatch({ type: LOGIN, data: res.data });
  } catch (error) {
    dispatch({ type: LOGOUT });
  }
};

export const login = () => {
  const action = { type: LOGIN, data: { authenticated: true, loading: false } };
  return action;
};

export const addErrorMessage = (errorMessage) => async (dispatch) => {
  dispatch({ type: ADD_ERROR, value: errorMessage });
  dispatch(removeErrorMessage(errorMessage));
};

const sleep = async () => await new Promise((r) => setTimeout(r, 7000));

const removeErrorMessage = (errorMessage) => async (dispatch) => {
  await sleep();
  dispatch({ type: REMOVE_ERROR, value: errorMessage });
};
