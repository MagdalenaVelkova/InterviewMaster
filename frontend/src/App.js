import AOS from "aos";
import "aos/dist/aos.css"; // You can also use <link> for styles
import "bootstrap/dist/css/bootstrap.min.css";
import React from "react";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import "./App.css";
import FixedTopContainer from "./components/FixedTopContainer";
import store from "./redux/store";
import Routes from "./router/Routes";

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