import "./App.css";
import NavBar from "./components/NavBar/NavBar";
import Hello from "./components/Hello";
import { BrowserRouter, Routes, Route } from "react-router-dom";

function App() {
  return (
    <div className="App">
      {/* <NavBar /> */}
      <BrowserRouter>
        <Routes>
          <Route path="/" exact element={<Hello />} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
