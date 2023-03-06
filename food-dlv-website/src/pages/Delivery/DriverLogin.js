import { Link, useNavigate } from "react-router-dom";
import React from "react";
import Bike from "../../assets/images/delivery/bike.svg";
import { Label, Input } from "../../components/Delivery/form-styling";

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
    <div className="flex justify-center bg-black">
      <div className="bg-white h-screen w-full max-w-md">
        <figure className=" flex justify-center pt-3">
          <img src={Bike} alt="bike" className="w-full max-w-xs h-72" />
        </figure>
        <h2 className="pl-6 text-xl mt-3 text-black">
          午安! <br />
          <span className="font-extrabold font-nunito">FASPAN</span> 的外送夥伴
        </h2>
        <div className="relative flex flex-col justify-center overflow-hidden">
          <div className=" w-full px-6 m-auto rounded-md max-w-md">
            <form className="mt-6">
              <div className="mb-2">
                <Label htmlFor="account">email / 帳號</Label>
                <Input
                  autoComplete="username"
                  name="account"
                  type="email"
                  onChange={(e) => setAccount(e.target.value)}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="password">密碼</Label>
                <Input
                  autoComplete="password"
                  name="password"
                  type="password"
                  onChange={(e) => setPassword(e.target.value)}
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
                to="/delivery/register"
                className="font-medium text-black hover:underline"
              >
                註冊
              </Link>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DriverLogin;
