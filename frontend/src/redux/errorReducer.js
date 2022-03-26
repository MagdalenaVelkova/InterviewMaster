import { ADD_ERROR, REMOVE_ERROR } from "./actionTypes";

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
