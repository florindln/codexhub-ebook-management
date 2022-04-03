import logo from "./logo.svg";
import "./App.css";
import { Route, Routes } from "react-router-dom";
import Hello from "./components/Hello";
import MyNavBar from "./components/navigation/MyNavBar";

function App() {
  return (
    <div className="App">
      <MyNavBar />
      <Routes>
        <Route path="/" exact element={<Hello />} />
      </Routes>
    </div>
  );
}

export default App;
