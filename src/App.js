import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./Layout";
import User from "./pages/User";
import Error from "./pages/Error";
import Home from "./pages/Home";
import Cart from "./pages/Cart";
import "./styles/tailwind.css";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />}></Route>
          <Route path="user" element={<User />}></Route>
          <Route path="cart" element={<Cart />}></Route>
          <Route path="*" element={<Error />}></Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
