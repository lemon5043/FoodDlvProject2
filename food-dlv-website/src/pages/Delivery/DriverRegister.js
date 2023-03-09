import React, { useState } from "react";
import { useNavigate } from "react-router";
import { Label, Input, Button } from "../../components/Delivery/form-styling";
import driverAuthService from "../../services/Delivery/driverAuth.service";

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
  let [driverLicense, setDriverLicense] = useState("");
  let [errorMessage, setErrorMessage] = useState("");
  let [serverErrorMessage, setServerErrorMessage] = useState("");

  const registerHandler = async (e) => {
    try {
      e.preventDefault();
      if (!isMatch) return;
      let formData = new FormData();
      formData.append("account", account);
      formData.append("password", password);
      formData.append("firstName", firstName);
      formData.append("lastName", lastName);
      formData.append("phone", phone);
      formData.append("bankAccount", bankAccount);
      formData.append("idcard", Idcard);
      formData.append("vehicleRegistration", vehicleRegistration);
      formData.append("driverLicense", driverLicense);
      await driverAuthService.register(formData);
      alert("註冊成功，還需要審核約1~2個工作天，屆時將以email通知，請耐心等候");
      navigate("/delivery/login");
    } catch (e) {
      setServerErrorMessage("信箱已被使用!");
      console.log(e);
    }
  };

  const isMatch = (e) => {
    if (e.target.value !== password) {
      setErrorMessage("密碼不相符");
      return false;
    }
    setErrorMessage("");
    return true;
  };

  return (
    <div className="flex justify-center h-full bg-black">
      <div className="bg-white h-screen w-full max-w-md overflow-scroll pb-4">
        <div className="relative flex flex-col justify-center overflow-hidden">
          <div className=" w-full px-6 m-auto rounded-md max-w-md">
            <p className="mt-6 text-2xl font-semibold text-center text-black">
              加入我們
            </p>
            <form className="mt-6" onSubmit={registerHandler}>
              <div className="mb-2">
                <Label htmlFor="account">email / 帳號</Label>
                <Input
                  required
                  pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$"
                  autoComplete="username"
                  name="account"
                  type="email"
                  onChange={(e) => setAccount(e.target.value)}
                />
                <p className="text-sm text-red-600">{serverErrorMessage}</p>
              </div>
              <div className="mb-2">
                <Label htmlFor="account">密碼</Label>
                <Input
                  required
                  autoComplete="new-password"
                  name="password"
                  type="password"
                  onChange={(e) => setPassword(e.target.value)}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="conFirmpassword">確認密碼</Label>
                <Input
                  required
                  autoComplete="new-password"
                  name="conFirmpassword"
                  type="password"
                  onChange={isMatch}
                />
                <p className="text-sm text-red-600">{errorMessage}</p>
              </div>
              <div className="mb-2">
                <Label htmlFor="lastName">姓</Label>
                <Input
                  required
                  name="lastName"
                  type="text"
                  maxLength="20"
                  onChange={(e) => setLastName(e.target.value)}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="lastName">名</Label>
                <Input
                  required
                  name="lastName"
                  type="text"
                  maxLength="20"
                  onChange={(e) => setFirstName(e.target.value)}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="phone">手機號碼</Label>
                <Input
                  required
                  name="phone"
                  type="text"
                  maxLength={10}
                  pattern="09\d{8}"
                  onChange={(e) => setPhone(e.target.value)}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="bankAccount">銀行帳戶</Label>
                <Input
                  required
                  name="bankAccount"
                  type="text"
                  onChange={(e) => setBankAccount(e.target.value)}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="idCard">身分證</Label>
                <input
                  required
                  type="file"
                  name="idCard"
                  onChange={(e) => setIdCard(e.target.files[0])}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="vehicleRegistration">行照</Label>
                <input
                  required
                  type="file"
                  name="vehicleRegistration"
                  onChange={(e) => setVehicleRegistration(e.target.files[0])}
                />
              </div>
              <div className="mb-2">
                <Label htmlFor="driverLicense">駕照</Label>
                <input
                  required
                  type="file"
                  name="driverLicense"
                  onChange={(e) => setDriverLicense(e.target.files[0])}
                />
              </div>
              <div className="mt-6">
                <Button type="submit">下一步</Button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DriverRegister;
