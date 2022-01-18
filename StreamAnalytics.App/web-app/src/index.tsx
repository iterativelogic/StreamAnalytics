import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import reportWebVitals from "./reportWebVitals";
import * as Router from "react-router-dom";
import { store } from "./global/store";
import { Provider } from "react-redux";

const initRender = () => {
  return (
    <React.StrictMode>
      <Provider store={store}>
        <Router.BrowserRouter>
          <App />
        </Router.BrowserRouter>
      </Provider>
    </React.StrictMode>
  );
};

ReactDOM.render(initRender(), document.getElementById("root"));

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
