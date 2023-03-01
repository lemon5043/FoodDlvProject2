import { Outlet, Link } from "react-router-dom";
import React, { useState } from "react";

const DriverLayout = () => {
  const [active, setActive] = useState(0);
  function activeLink(e) {
    console.log(active);
    setActive(Number(e.target.value));
    console.log(e.target);
  }

  return (
    <div>
      <Outlet />
      <footer className="flex justify-center items-center min-h-screen bg-black">
        <div className="driverNav w-96 h-16 relative bg-white rounded-lg flex justify-center items-center">
          <ul className="flex w-80">
            <li
              value={0}
              className={active === 0 ? "list active" : "list"}
              onClick={activeLink}
            >
              <Link to="/delivery">
                <span className="icon">
                  <i className="fa-solid fa-user"></i>
                </span>
                <span className="text">狀態</span>
              </Link>
            </li>
            <li
              value={1}
              className={active === 1 ? "list active" : "list"}
              onClick={activeLink}
            >
              <Link to="/delivery/order">
                <span className="icon">
                  <i className="fa-solid fa-bicycle"></i>
                </span>
                <span className="text">訂單</span>
              </Link>
            </li>
            <li
              value={2}
              className={active === 2 ? "list active" : "list"}
              onClick={activeLink}
            >
              <Link to="/delivery/map">
                <span className="icon">
                  <i className="fa-regular fa-map"></i>
                </span>
                <span className="text">地圖</span>
              </Link>
            </li>
            <li
              value={3}
              className={active === 3 ? "list active" : "list"}
              onClick={activeLink}
            >
              <Link to="/delivery/history">
                <span className="icon">
                  <i className="fa-solid fa-clock-rotate-left"></i>
                </span>
                <span className="text">紀錄</span>
              </Link>
            </li>
            <li
              value={4}
              className={active === 4 ? "list active" : "list"}
              onClick={activeLink}
            >
              <Link to="/delivery/wallet">
                <span className="icon">
                  <i className="fa-solid fa-wallet"></i>
                </span>
                <span className="text">錢包</span>
              </Link>
            </li>
            <div className="indicator"></div>
          </ul>
        </div>
      </footer>
    </div>
  );
};

export default DriverLayout;
