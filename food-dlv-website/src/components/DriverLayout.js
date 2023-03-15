import { Outlet, Link, useResolvedPath, useMatch } from "react-router-dom";
import React from "react";
import "../assets/styles/DriverLayout.css";

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
    <div className="flex h-screen justify-center bg-black">
      <div className="h-full max-w-md bg-slate-900">
        <main style={{ height: "90%" }}>
          <Outlet className="text-white h-full overflow-scroll" />
        </main>
        <footer
          className="flex justify-center items-end bg-slate-900"
          style={{ height: "10%" }}
        >
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
    </div>
  );
};

export default DriverLayout;
