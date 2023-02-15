import { Outlet, Link } from "react-router-dom";
import React from "react";

// 這個檔案是導覽列設定
const Layout = () => {
  return (
    <div>
      <nav className="bg-theme-color">
        <ul className="flex justify-end mr-4">
          <li className="p-4">
            {/* 會員及設定超連結*/}
            <Link to="/user" className="text-2xl">
              <i className="fa-solid fa-user"></i>
            </Link>
          </li>
          {/* 購物車超連結 */}
          <li className="p-4">
            <Link to="/cart" className="text-2xl">
              <i class="fa-solid fa-bag-shopping"></i>
            </Link>
          </li>
        </ul>
      </nav>
      <Outlet />
    </div>
  );
};

export default Layout;
