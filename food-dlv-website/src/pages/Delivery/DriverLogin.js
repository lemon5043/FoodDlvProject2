import { Link, useNavigate } from "react-router-dom";
import React from "react";
import Bike from "../../assets/images/delivery/bike.svg";

const DriverLogin = () => {
  // navigate 是控制重新導向的東西
  const navigate = useNavigate();

  //判斷是否登入成功
  const loginHandler = () => {
    if (true) {
      navigate("/delivery");
    }
  };

  return (
    <div>
      <figure className=" flex justify-center pt-3">
        <img src={Bike} alt="bike" className="w-3/5" />
      </figure>
      <h2 className="pl-6 text-xl mt-3">
        午安! <br />
        <span className="font-extrabold font-nunito">FASPAN</span> 的外送夥伴
      </h2>
      <div className="relative flex flex-col justify-center overflow-hidden">
        <div className=" w-full px-6 m-auto rounded-md max-w-md">
          <form className="mt-6">
            <div className="mb-2">
              <label
                htmlFor="account"
                className="block text-base font-semibold text-gray-800"
              >
                帳號
              </label>
              <input
                name="account"
                type="email"
                className="block w-full px-4 py-2 mt-2 text-neutral-700 bg-white border rounded-md focus:border-neutral-400 focus:ring-neutral-300 focus:outline-none focus:ring focus:ring-opacity-40"
              />
            </div>
            <div className="mb-2">
              <label
                htmlFor="password"
                className="block text-base font-semibold text-gray-800"
              >
                密碼
              </label>
              <input
                name="password"
                type="password"
                className="block w-full px-4 py-2 mt-2 text-neutral-700 bg-white border rounded-md focus:border-neutral-400 focus:ring-neutral-300 focus:outline-none focus:ring focus:ring-opacity-40"
              />
            </div>
            <Link to="/" className="text-sm text-neutral-600 hover:underline">
              忘記密碼?
            </Link>
            <div className="mt-6">
              <button
                onClick={loginHandler}
                className="w-full px-4 py-2 tracking-wide text-white transition-colors duration-200 transform bg-black rounded-md hover:bg-zinc-600 focus:outline-none focus:bg-zinc-600"
              >
                登入
              </button>
            </div>
          </form>
          <p className="mt-8 text-sm font-light text-center text-gray-700">
            {" "}
            還沒有帳號嗎?{" "}
            <Link
              to="/delivery"
              className="font-medium text-black hover:underline"
            >
              註冊
            </Link>
          </p>
        </div>
      </div>
    </div>
  );
};

export default DriverLogin;
