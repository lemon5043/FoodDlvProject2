import { Outlet, Link } from "react-router-dom";
import React from "react";
import Logo from "../images/logo.svg";
import index from "../hooks/useAxios";

// 這個檔案是導覽列設定
const Layout = () => {
  return (
    <div>
      <nav className="bg-theme-color">
        <ul className="flex justify-between w-full mr-4">
          {/* logo與文字 */}
          <li>
            <Link to="/" className="text-2xl flex items-center px-4 py-1">
              <div className="text-4xl">
                <img src={Logo} alt="logo.svg" className="w-14 -rotate-12" />
              </div>
              <div className="p-4 font-extrabold font-nunito">FASPAN</div>
            </Link>
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
