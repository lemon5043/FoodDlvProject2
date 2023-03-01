import { Outlet, Link, useResolvedPath, useMatch } from "react-router-dom";
import React from "react";

const CustomLink = ({ to, children, setIcon }) => {
  const resolvePath = useResolvedPath(to);
  const isActive = useMatch({ path: resolvePath.pathname, end: true });

  return (
    <li value={0} className={isActive ? "list active" : "list"}>
      <Link to={to}>
        <span className="icon">
          <i className={setIcon}></i>
        </span>
        <span className="text">{children}</span>
      </Link>
    </li>
  );
};

const DriverLayout = () => {
  return (
    <div className="h-screen bg-black">
      <nav></nav>
      <main style={{ minHeight: "calc(100% - 8rem)" }}>
        <Outlet />
      </main>
      <footer className="flex justify-center items-end h-32 bg-black">
        <div className="driverNav w-96 h-16 relative bg-white rounded-lg flex justify-center items-center">
          <ul className="flex w-80">
            <CustomLink setIcon="fa-solid fa-user" to="/delivery">
              狀態
            </CustomLink>
            <CustomLink setIcon="fa-solid fa-bicycle" to="/deliveryOrder">
              訂單
            </CustomLink>
            <CustomLink setIcon="fa-regular fa-map" to="/deliveryMap">
              地圖
            </CustomLink>
            <CustomLink
              setIcon="fa-solid fa-clock-rotate-left"
              to="/deliveryHistory"
            >
              紀錄
            </CustomLink>
            <CustomLink setIcon="fa-solid fa-wallet" to="/deliveryWallet">
              錢包
            </CustomLink>
            <div className="indicator"></div>
          </ul>
        </div>
      </footer>
    </div>
  );
};

export default DriverLayout;
