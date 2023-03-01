import { Outlet, Link } from "react-router-dom";
import React from "react";

const DriverLayout = () => {
  return (
    <div>
      <Outlet />
      <footer className="navigation">
        <ul>
          <li>
            <a href="">
              <span></span>
              <span></span>
            </a>
          </li>
          <li>
            <a href="">
              <span>
                <i class="fa-solid fa-user"></i>
              </span>
              <span>狀態</span>
            </a>
          </li>
          <li>
            <a href="">
              <span>
                <i class="fa-solid fa-user"></i>
              </span>
              <span></span>
            </a>
          </li>
          <li>
            <a href="">
              <span></span>
              <span></span>
            </a>
          </li>
          <li>
            <a href="">
              <span></span>
              <span></span>
            </a>
          </li>
        </ul>
      </footer>
    </div>
  );
};

export default DriverLayout;
