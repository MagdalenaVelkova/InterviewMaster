import axios from "axios";
import {
  ADD_ERROR,
  LOGIN,
  LOGOUT,
  REMOVE_ERROR,
  USER_LOADING,
} from "./actionTypes";

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

export const registerUser = (values) => async (dispatch) => {
  try {
    const payload = {
      emailAddress: values.email,
      password: values.password,
    };
    const res = await axios.post(payload);
    dispatch(loginUser(values));
  } catch (error) {}
};

export const logoutUser = () => async (dispatch) => {
  console.log("dispatching logout");
  localStorage.clear();
  dispatch({ type: LOGOUT });
};

export const validateToken = () => async (dispatch) => {
  try {
    dispatch({ type: USER_LOADING });
    const res = await axios.get("http://localhost:5000/authorised");
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
