import { Outlet, Link } from "react-router-dom";
import React from "react";

// 這個檔案是導覽列設定
const Layout = () => {
  return (
    <div>
      <nav className="bg-theme-color">
        <ul className="flex mr-4">
          <div className="logo flex">
            <a
              class="sidebar-brand d-flex align-items-center justify-content-center"
              href="/Home/Index"
            >
              <div class="text-4xl">
                <i class="fa-solid fa-truck-fast"></i>
              </div>
            </a>
            <div>Faspan</div>
          </div>
          <div className="navbar flex">
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
          </div>
        </ul>
      </nav>
      <Outlet />
    </div>
  );
};

export default Layout;
