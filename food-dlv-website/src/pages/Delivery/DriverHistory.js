import React, { useState } from "react";
import driverAuthService from "../../services/Delivery/driverAuth.service";
import delieveryRecordsService from "../../services/Delivery/delieveryRecords.service";


const DriverHistory = () => {
  let [month,setMonth] = useState("")
  let [year,setYear] = useState("")
  let [errorMessage, setErrorMessage] = useState("");
//測試用
// const y = 2023;
// const m = 3;
// const token = localStorage.getItem("driver")
// const driver =  driverAuthService.GetDriver(token).then(
//   function(response){
//     const driverRecords = delieveryRecordsService.GetAllRecrodes(response.data.driverId,token).then(
//       function(details){
//         console.log(details)
//       }       
//     );

//     const driverDetail = delieveryRecordsService.GetRecrodesByMonth(y,m,response.data.driverId,token).then(
//       function(details){
//         console.log(details)
//       }       
//     )     
//   }
// )

const GetAllRecrodes = async (e)=>{
  try {
    const token = localStorage.getItem("driver")
    let driver = (await driverAuthService.GetDriver(token))
    const driverDetail = await (await delieveryRecordsService.GetAllRecrodes(driver.data.driverId,token)).data
  }
  catch{
    setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
  }
}
  


const GetRecrodesByMonth = async (e)=>{
  try {
    const token = localStorage.getItem("driver")
    let driver = (await driverAuthService.GetDriver(token))
    const driverDetail = await (await delieveryRecordsService.GetRecrodesByMonth(setYear,setMonth,driver.data.driverId,token)).data
  }
  catch{
    setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
  }
}
  return <div className="text-white">DriverHistory</div>;

};
export default DriverHistory;
