import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { Routes, Route, Link } from "react-router-dom";
import { EventLog } from "./main/event-log/components";

function App() {
  return (
    <>
      <Routes>
        <Route path="/eventlog" element={<EventLog />}></Route>
      </Routes>

      <div className="App">
        <header className="App-header">
          {/* <img src={logo} className="App-logo" alt="logo" /> */}
          <p>
            Edit <code>src/App.tsx</code> and save to reload.
          </p>
          <a
            className="App-link"
            href="https://reactjs.org"
            target="_blank"
            rel="noopener noreferrer"
          >
            Learn React
          </a>
          <Link to="/eventlog">Event Log</Link>
        </header>
      </div>
    </>
  );
}

export default App;
