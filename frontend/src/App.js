import logo from "./logo.svg";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import Hello from "./components/Hello";
import MyNavBar from "./components/navigation/MyNavBar";
import SignIn from "./components/login/SignIn";
import SignUp from "./components/login/SignUp";
import React from "react";

function App() {
  return (
    <div className="App">
      <MyNavBar />
      <Routes>
        <Route path="/" element={<Hello />} />
        <Route path="/SignIn" element={<SignIn />} />
        <Route path="/SignUp" element={<SignUp />} />
      </Routes>
    </div>
  );
}

export default App;
