import React, { useState } from "react";
import driverAuthService from "../../services/Delivery/driverAuth.service";
import paysService from "../../services/Delivery/pays.service";


const DriverWallet = () => {
  let [month, setMonth] = useState("")
  let [year, setYear] = useState("")
  let [errorMessage, setErrorMessage] = useState("");
  //測試用
  // const y = 2023;
  // const m = 1;
  // const token = localStorage.getItem("driver")
  // const driver =  driverAuthService.GetDriver(token).then(
  //   function(response){
  //     const driverRecords = paysService.GetAllPayRecrodes(response.data.driverId,token).then(
  //       function(details){
  //         console.log(details.data)
  //       }       
  //     );

  //     const driverDetail = paysService.GetPayRecrodesByMonth(y,m,response.data.driverId,token).then(
  //       function(details){
  //         console.log(details.data)
  //       }       
  //     )     
  //   }
  // )

  const GetAllRecrodes = async (e) => {
    try {
      const token = localStorage.getItem("driver")
      let driver = (await driverAuthService.GetDriver(token))
      const driverDetail = await (await paysService.GetAllPayRecrodes(driver.data.driverId, token)).data
    }
    catch {
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }

  const GetRecrodesByMonth = async (e) => {
    try {
      const token = localStorage.getItem("driver")
      let driver = (await driverAuthService.GetDriver(token))
      const driverDetail = await (await paysService.GetPayRecrodesByMonth(setYear, setMonth, driver.data.driverId, token)).data
    }
    catch {
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }
  return <div className="text-white">DriverWallet</div>;

};


export default DriverWallet;
