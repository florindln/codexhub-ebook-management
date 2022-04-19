import logo from "./logo.svg";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import MyNavBar from "./components/navigation/MyNavBar";
import SignIn from "./components/login/SignIn";
import SignUp from "./components/login/SignUp";
import React from "react";
import Footer from "components/Footer/Footer";
import Homepage from "components/homepage/Homepage";
import Browse from "components/Browse/Browse";
import AdminBookTable from "components/Admin/Book/AdminBookTable";

function App() {
  return (
    <div className="App">
      <MyNavBar />
      <Routes>
        <Route path="/" element={<Homepage />} />
        <Route path="/MyBooks" element={<Browse />} />
        <Route path="/AdminTable" element={<AdminBookTable />} />
        <Route path="/SignIn" element={<SignIn />} />
        <Route path="/SignUp" element={<SignUp />} />
      </Routes>
      {/* <Footer /> */}
    </div>
  );
}

export default App;
