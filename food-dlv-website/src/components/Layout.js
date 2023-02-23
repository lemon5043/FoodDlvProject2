import { Outlet, Link } from "react-router-dom";
import React from "react";
import Logo from "../assets/images/logo.svg";
import index from "../hooks/useAxios";

// 這個檔案是導覽列設定
const Layout = () => {
  return (
    <div>
      <nav className="bg-theme-color">
        <ul className="flex justify-between w-full mr-4">
          {/* logo 標題 搜尋欄 */}
          <li className="flex items-center">
            <Link to="/" className="text-2xl flex items-center px-4">
              {/* logo */}
              <div>
                <img src={Logo} alt="logo.svg" className="w-10 -rotate-12" />
              </div>
              {/* 標題 */}
              <div className="text-2xl pl-4 pr-16 font-extrabold font-nunito">
                FASPAN
              </div>
            </Link>
            {/* 搜尋欄 */}
            <div className="relative text-gray-600 flex items-center">
              <input
                className="border-2 border-gray-300 bg-white h-10 w-80 px-2 rounded-lg text-sm focus:border-neutral-400 focus:ring-neutral-300 focus:outline-none focus:ring focus:ring-opacity-40"
                type="address"
                name="address"
                placeholder="要送到哪呢"
              />
              <button type="submit" className="absolute right-0 top-0 mr-3">
                <i className="fa-solid fa-magnifying-glass py-3"></i>
              </button>
            </div>
          </li>

          {/* 導覽列右側 nav */}
          <li className="flex items-center">
            {/* (沒登入)登入超連結*/}
            <div className="pr-4">
              <Link
                to="/login"
                className="text-base bg-zinc-300 hover:bg-zinc-400 font-semibold py-2 px-4 rounded-full"
              >
                <i className="fa-solid  fa-user pr-2"></i>
                登入/ 註冊
              </Link>
            </div>
            {/* (有登入)會員及設定，等會認證後再說 */}
            {/* 購物車超連結 */}
            <div className="p-4">
              <Link to="/cart" className="text-base">
                <i className="fa-solid fa-bag-shopping"></i>
              </Link>
            </div>
          </li>
        </ul>
      </nav>
      <Outlet />
    </div>
  );
};

export default Layout;
