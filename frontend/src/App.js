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
import BookEditPage from "components/Admin/Book/BookEditPage";
import ProfilePage from "components/Profile/ProfilePage";

function App() {
  return (
    <div className="App">
      <div style={{ minHeight: "50rem" }}>
        <MyNavBar />
        <Routes>
          <Route path="/" element={<Homepage />} />
          <Route path="/MyBooks" element={<Browse />} />
          <Route path="/AdminTable" element={<AdminBookTable />} />
          <Route path="/SignIn" element={<SignIn />} />
          <Route path="/Profile" element={<ProfilePage />} />
          <Route path="/SignUp" element={<SignUp />} />
          <Route path="/books/:id/edit" element={<BookEditPage />} />
        </Routes>
      </div>
      <Footer />
    </div>
  );
}

export default App;
