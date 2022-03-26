import "./App.css";
import React from "react";
import "bootstrap/dist/css/bootstrap.min.css";

import store from "./redux/store";
import { Provider } from "react-redux";

import Routes from "./router/routes";
import { BrowserRouter } from "react-router-dom";

import FixedTopContainer from "./components/FixedTopContainer";

import AOS from "aos";
import "aos/dist/aos.css"; // You can also use <link> for styles

AOS.init();

function App() {
  return (
    <Provider store={store}>
      <div className="App">
        <BrowserRouter>
          <Routes></Routes>
          <FixedTopContainer></FixedTopContainer>
        </BrowserRouter>
      </div>
    </Provider>
  );
}

export default App;
