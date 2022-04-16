import logo from "./logo.svg";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import Homepage from "./components/Homepage";
import MyNavBar from "./components/navigation/MyNavBar";
import SignIn from "./components/login/SignIn";
import SignUp from "./components/login/SignUp";
import React from "react";
import Footer from "components/Footer/Footer";

function App() {
  return (
    <div className="App">
      <MyNavBar />
      <Routes>
        <Route path="/" element={<Homepage />} />
        <Route path="/SignIn" element={<SignIn />} />
        <Route path="/SignUp" element={<SignUp />} />
      </Routes>
      <Footer />
    </div>
  );
}

export default App;
