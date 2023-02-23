import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Layout from "./components/Layout";
import User from "./pages/User";
import Error from "./pages/Error";
import Home from "./pages/Home/Home";
import Cart from "./pages/Cart";
import Login from "./pages/Login";
import "./assets/styles/tailwind.css";
import Delivery from "./pages/Delivery/Delivery";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />}></Route>
          <Route path="user" element={<User />}></Route>
          <Route path="cart" element={<Cart />}></Route>
          <Route path="login" element={<Login />}></Route>
          <Route path="*" element={<Error />}></Route>
        </Route>
        <Route path="delivery" element={<Delivery />}></Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
