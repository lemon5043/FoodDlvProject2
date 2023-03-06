import React, { useState } from "react";
import { useNavigate } from "react-router";
import { Label, Input } from "../../components/Delivery/form-styling";

const DriverRegister = () => {
  //*states
  const navigate = useNavigate();
  let [account, setAccount] = useState("");
  let [password, setPassword] = useState("");
  let [firstName, setFirstName] = useState("");
  let [lastName, setLastName] = useState("");
  let [phone, setPhone] = useState("");
  let [bankAccount, setBankAccount] = useState("");
  let [Idcard, setIdCard] = useState("");
  let [vehicleRegistration, setVehicleRegistration] = useState("");
  let [DriverLicense, setDriverLicense] = useState("");

  const isMatch = (e) => {
    return e.target.value === password;
  };

  return (
    <div className="flex justify-center bg-black">
      <div className="bg-white h-screen w-full max-w-md">
        <div className="relative flex flex-col justify-center overflow-hidden">
          <div className=" w-full px-6 m-auto rounded-md max-w-md">
            <p className="mt-8 text-2xl font-semibold text-center text-black">
              加入我們
            </p>
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
                <Label htmlFor="account">密碼</Label>
                <Input
                  autoComplete="new-password"
                  name="password"
                  type="password"
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="conFirmpassword">確認密碼</Label>
                <Input
                  autoComplete="new-password"
                  name="conFirmpassword"
                  type="password"
                  onChange={isMatch}
                />
              </div>
              <Label htmlFor="lastName">姓</Label>
              <Input
                name="lastName"
                type="text"
                onChange={(e) => setLastName(e.target.value)}
              />
              <Label htmlFor="lastName">名</Label>
              <Input
                name="lastName"
                type="text"
                onChange={(e) => setFirstName(e.target.value)}
              />
              <Label htmlFor="phone">手機號碼</Label>
              <Input
                name="phone"
                type="text"
                onChange={(e) => setPhone(e.target.value)}
              />
              <Label htmlFor="bankAccount">銀行帳戶</Label>
              <Input
                name="bankAccount"
                type="text"
                onChange={(e) => setBankAccount(e.target.value)}
              />
              <div className="mt-6">
                <button className="w-full px-4 py-2 tracking-wide text-white transition-colors duration-200 transform bg-black rounded-md hover:bg-zinc-600 focus:outline-none focus:bg-zinc-600">
                  下一步
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DriverRegister;
