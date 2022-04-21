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
import BookManagePage from "components/Admin/Book/BookManagePage";
import ProfilePage from "components/Profile/ProfilePage";
import BookDetailsPage from "components/Book/BookDetailsPage";

function App() {
  return (
    <div className="App">
      <div style={{ minHeight: "45rem" }}>
        <MyNavBar />
        <Routes>
          <Route path="/" element={<Homepage />} />
          <Route path="/Browse" element={<Browse />} />
          <Route path="/Browse?name=:name" element={<Browse />} />
          <Route path="/AdminTable" element={<AdminBookTable />} />
          <Route path="/SignIn" element={<SignIn />} />
          <Route path="/Profile" element={<ProfilePage />} />
          <Route path="/SignUp" element={<SignUp />} />
          <Route path="/Books/add" element={<BookManagePage type="create" />} />
          <Route path="/Books/:id" element={<BookDetailsPage />} />
          <Route
            path="/Books/:id/edit"
            element={<BookManagePage type="edit" />}
          />
        </Routes>
      </div>
      <Footer />
    </div>
  );
}

export default App;
