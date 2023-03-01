import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
//用戶介面
import Layout from "./components/Layout";
import User from "./pages/User";
import Error from "./pages/Error";
import Home from "./pages/Home/Home";
import Cart from "./pages/Cart";
import Login from "./pages/Login";
//外送員介面
import DriverLayout from "./components/DriverLayout";
import DriverHome from "./pages/Delivery/DriverHome";
import DriverLogin from "./pages/Delivery/DriverLogin";
//css
import "./assets/styles/tailwind.css";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        {/* 使用者頁面 */}
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />}></Route>
          <Route path="user" element={<User />}></Route>
          <Route path="cart" element={<Cart />}></Route>
          <Route path="login" element={<Login />}></Route>
          <Route path="*" element={<Error />}></Route>
        </Route>
        {/* 外送員頁面 */}
        <Route path="driverLogin" element={<DriverLogin />}></Route>
        <Route path="/delivery" element={<DriverLayout />}>
          <Route index element={<DriverHome />}></Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
