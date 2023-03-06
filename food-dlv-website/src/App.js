import React from "react";
import { BrowserRouter, Routes, Route } from "react-router-dom";
//*用戶介面
//layout
import Layout from "./components/Layout";
//登入註冊
import Login from "./pages/Login";
//頁面
import User from "./pages/User";
import Error from "./pages/Error";
import Home from "./pages/Home/Home";
import Cart from "./pages/Cart";
import Store from "./pages/Store/Store";
// *外送員介面
//layout
import DriverLayout from "./components/DriverLayout";
//登入註冊
import DriverRegister from "./pages/Delivery/DriverRegister";
import DriverLogin from "./pages/Delivery/DriverLogin";
//頁面
import DriverHome from "./pages/Delivery/DriverHome";
import DriverMap from "./pages/Delivery/DriverMap";
import DriverOrder from "./pages/Delivery/DriverOrder";
import DriverWallet from "./pages/Delivery/DriverWallet";
import DriverHistory from "./pages/Delivery/DriverHistory";
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
          <Route path="store" element={<Store />}></Route>
          <Route path="*" element={<Error />}></Route>
        </Route>
        {/* 外送員頁面 */}
        {/* 註冊登入 */}
        <Route path="/delivery/register" element={<DriverRegister />}></Route>
        <Route path="/delivery/login" element={<DriverLogin />}></Route>
        <Route element={<DriverLayout />}>
          <Route path="/delivery" element={<DriverHome />}></Route>
          <Route path="/deliveryOrder" element={<DriverOrder />}></Route>
          <Route path="/deliveryMap" element={<DriverMap />}></Route>
          <Route path="/deliveryHistory" element={<DriverHistory />}></Route>
          <Route path="/deliveryWallet" element={<DriverWallet />}></Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
