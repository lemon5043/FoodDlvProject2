import React, { useState } from "react";
import Radio from "@mui/material/Radio";
import RadioGroup from "@mui/material/RadioGroup";
import FormControlLabel from "@mui/material/FormControlLabel";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { useNavigate } from "react-router";
import InputComponent from "../../components/Delivery/inputComponent";

const step1 = () => {};

const DriverRegister = () => {
  const navigate = useNavigate();
  let [account, setAccount] = useState("");
  let [password, setPassword] = useState("");
  let [firstName, setFirstName] = useState("");
  let [lastName, setLastName] = useState("");
  let [phone, setPhone] = useState("");
  let [gender, setGender] = useState(null);
  let [bankAccount, setBankAccount] = useState("");
  let [Idcard, setIdCard] = useState("");
  let [Birthday, setBirthday] = useState(null);
  let [Email, setEmail] = useState("");
  let [vehicleRegistration, setVehicleRegistration] = useState("");
  let [DriverLicense, setDriverLicense] = useState("");

  return (
    <div className="flex justify-center bg-black">
      <div className="bg-white h-screen w-full max-w-md">
        <div className="relative flex flex-col justify-center overflow-hidden">
          <div className=" w-full px-6 m-auto rounded-md max-w-md">
            <p className="mt-8 text-2xl font-semibold text-center text-black">
              加入我們
            </p>
            <form className="mt-6">
              <InputComponent
                text="帳號"
                name="account"
                type="text"
                setInput={setAccount}
              />
              <InputComponent
                text="email"
                name="email"
                type="text"
                setInput={setLastName}
              />
              <InputComponent
                text="密碼"
                name="password"
                type="password"
                setInput={setPassword}
              />
              <InputComponent
                text="確認密碼"
                name="comfirmPassword"
                type="password"
                setInput={setEmail}
              />
              <div className="mt-6">
                <button className="w-full px-4 py-2 tracking-wide text-white transition-colors duration-200 transform bg-black rounded-md hover:bg-zinc-600 focus:outline-none focus:bg-zinc-600">
                  下一步
                </button>
              </div>
              <InputComponent
                text="姓"
                name="lastName"
                type="text"
                setInput={setLastName}
              />
              <InputComponent
                text="名"
                name="firstName"
                type="text"
                setInput={setFirstName}
              />
              <InputComponent
                text="手機號碼"
                name="phone"
                type="number"
                setInput={setPhone}
              />
              <InputComponent
                text="銀行帳戶"
                name="bankAccount"
                type="number"
                setInput={setBankAccount}
              />
              <label
                htmlFor="gender"
                className="block text-base font-semibold text-gray-800"
              >
                性別
              </label>
              <RadioGroup
                row
                aria-labelledby="demo-row-radio-buttons-group-label"
                name="row-radio-buttons-group"
                onChange={(newGender) => setGender(newGender)}
              >
                <FormControlLabel
                  value="false"
                  control={<Radio />}
                  label="女"
                />
                <FormControlLabel value="true" control={<Radio />} label="男" />
              </RadioGroup>
              <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DemoContainer components={["DatePicker"]}>
                  <DatePicker
                    onChange={(newBirthday) => setBirthday(newBirthday)}
                    label="生日"
                  />
                </DemoContainer>
              </LocalizationProvider>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DriverRegister;
