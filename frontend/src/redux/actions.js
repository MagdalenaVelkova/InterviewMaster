import {
  ADD_ERROR,
  REMOVE_ERROR,
  LOGIN,
  LOGOUT,
  USER_LOADING,
} from "./actionTypes";
import axios from "axios";

/**
 * Calls the backend to verify that the current user is authenticated with a valid Json Web token.
 * It dispatches a Login and Logout reducer to update the state of the web-client.
 */
export const loginUser = (email, password) => async (dispatch) => {
  try {
    const payload = new FormData();
    payload.append("username", email);
    payload.append("password", password);
    const res = await axios.post("http://localhost:8000/api/token", payload, {
      "Content-Type": "multipart/form-data",
    });
    sessionStorage.setItem("token", res.data.access_token);
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
      role: values.role,
    };
    const res = await axios.post(payload);
    dispatch(loginUser(values));
  } catch (error) {}
};

export const logoutUser = () => async (dispatch) => {
  try {
    sessionStorage.removeItem("token");
    const res = await axios.post();
  } catch (error) {
    const err = error;
  } finally {
    dispatch({ type: LOGOUT });
  }
};

export const isUserAthenticated = () => async (dispatch) => {
  try {
    dispatch({ type: USER_LOADING });
    const res = await axios.get();
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
