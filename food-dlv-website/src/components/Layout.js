import { Outlet, Link, useNavigate } from "react-router-dom";
import React, { useState } from "react";
import Logo from "../assets/images/logo.svg";
import bag from "../assets/icons/bag.svg";
import magnifyingGlass from "../assets/icons/magnifying-glass-solid.svg";
import User from "../assets/icons/user.svg";
import Swal from "sweetalert2";
import userAuthService from "../services/User/userAuth.service";
import StoreService from "../services/store.service";

const Layout = ({ currentUser, setCurrentUser }) => {
  const navigate = useNavigate();
  let [storeData, setStoreData] = useState([]);
  let [address, setAddress] = useState("");

  const addressHandler = async () => {
    let res = await StoreService.getByAddress(address);
    if (typeof res !== "undefined") {
      console.log(res.data);
      setStoreData(res.data);
      navigate("/store", { state: { storeData: res.data } });
    }
  };

  //搜尋欄，按下 enter 就會自動搜尋
  const enterHandler = (event) => {
    if (event.key === "Enter") {
      addressHandler();
    }
  };

  const logoutHandler = () => {
    Swal.fire({
      title: "您確定要登出嗎?",
      text: "期待下次跟您相見!",
      heightAuto: false,
      icon: "question",
      showCancelButton: true,
      confirmButtonColor: "#050505",
      cancelButtonColor: "#aeb2b6",
      confirmButtonText: "登出",
      cancelButtonText: "取消",
    }).then((result) => {
      if (result.isConfirmed) {
        userAuthService.logout();
        setCurrentUser(null);
        navigate("/");
      }
    });
  };

  return (
    <div className="h-full">
      <nav
        style={{ height: "6%" }}
        className="bg-theme-color sticky top-0 z-10"
      >
        <ul className="flex h-full justify-between items-center w-full mr-4">
          {/* logo 標題 搜尋欄 */}
          <li className="flex items-center">
            <Link to="" className="text-2xl flex items-center px-4">
              {/* logo */}
              <div>
                <img src={Logo} alt="logo.svg" className="w-10 -rotate-12" />
              </div>
              {/* 標題 */}
              <div className="text-2xl pl-4 font-extrabold font-nunito">
                FASPAN
              </div>
            </Link>
            {/* 搜尋欄 */}
            <div className="pl-16 relative text-gray-600 flex items-center">
              <input
                onChange={(e) => setAddress(e.target.value)}
                onKeyDown={enterHandler}
                className="border-2 border-gray-300 bg-white h-10 w-80 px-2 rounded-lg text-sm focus:border-neutral-400 focus:ring-neutral-300 focus:outline-none focus:ring focus:ring-opacity-40"
                type="address"
                name="address"
                placeholder="要送到哪呢"
              />
              <button
                onClick={addressHandler}
                type="submit"
                className="absolute right-0 top-0 mr-3"
              >
                <img
                  src={magnifyingGlass}
                  alt="magnifyingGlass.svg"
                  className="py-3 w-4"
                />
                {/* <i className="fa-solid fa-magnifying-glass py-3"></i> */}
              </button>
            </div>
          </li>

          {/* 導覽列右側 nav */}
          <li className="flex items-center mr-4">
            <div className="relative group">
              {/* (沒登入)登入超連結*/}
              {!currentUser && (
                <div className="pr-4">
                  <Link
                    to="/login"
                    className="text-base bg-zinc-300 hover:bg-zinc-400 font-semibold py-2 px-4 rounded-full flex justify-center"
                  >
                    <img
                      src={User}
                      alt="user.svg"
                      className="w-4 mr-2 inline"
                    />
                    {/* <i className="fa-solid  fa-user pr-2"></i> */}
                    登入/ 註冊
                  </Link>
                </div>
              )}
              {currentUser && (
                <div>
                  <button className=" w-full px-4 font-medium">
                    <span>Hi!, {currentUser.userAccount}</span>
                  </button>
                  <div className="absolute z-10 hidden bg-grey-200 group-hover:block">
                    <div className="w-28 pt-2 pb-4 bg-white shadow-lg flex justify-center">
                      <ul className="w-full text-center">
                        <li className=" py-2 transition-colors hover:bg-zinc-200 hover:text-emerald-600">
                          <Link to="/cart" className="text-base">
                            個人檔案
                          </Link>
                        </li>
                        <li className=" py-2 transition-colors hover:bg-zinc-200 hover:text-emerald-600">
                          <Link to="/cart" className="text-base">
                            我的訂單
                          </Link>
                        </li>
                        <li className=" py-2 transition-colors hover:bg-zinc-200 hover:text-emerald-600">
                          <Link to="/cart" className="text-base">
                            客服中心
                          </Link>
                        </li>
                        <li className=" py-2 transition-colors hover:bg-zinc-200 hover:text-emerald-600">
                          <Link onClick={logoutHandler} className="text-base">
                            登出
                          </Link>
                        </li>
                      </ul>
                    </div>
                  </div>
                </div>
              )}
            </div>
            {/* 購物車超連結 */}
            <div className="p-4">
              <Link to="/cart" className="text-base">
                <img src={bag} alt="bag.svg" className="w-4" />
                {/* <i className="fa-solid fa-bag-shopping"></i> */}
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
