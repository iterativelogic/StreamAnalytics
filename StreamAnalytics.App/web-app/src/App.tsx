import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { Routes, Route, Link } from "react-router-dom";
import { EventLog } from "./main/event-log/components";
import CssBaseline from "@mui/material/CssBaseline";
import Dashboard from "./Dashboard";

function App() {
  return (
    <>
      <div className="App">
        <Dashboard />
        {/* <Routes>
          <Route path="/eventlog" element={<EventLog />}></Route>
        </Routes>
        <Link to="/eventlog">Event Log</Link> */}
      </div>
    </>
  );
}

export default App;
