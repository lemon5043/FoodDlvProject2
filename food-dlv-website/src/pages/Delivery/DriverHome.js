import React, { useState } from "react";
import driverAuthService from "../../services/Delivery/driverAuth.service";
import driverService from "../../services/Delivery/driver.service";



const DriverHome = () => {
  
  let [errorMessage, setErrorMessage] = useState("");
  //測試用
  // const token = localStorage.getItem("driver")
  // const driverId =  driverAuthService.GetDriver(token).then(
  //   function(response){
  //     console.log(response.data.driverId)
  //     const driverDetail = driverService.GetDetails(response.data.driverId,token).then(
  //       function(details){
  //         console.log(details.data)
  //       }       
  //     )    
  //   }
  // )

  
  const GetDriverDetail = async (e)=>{
    try {
      const token = localStorage.getItem("driver")
      let driver = (await driverAuthService.GetDriver(token))
      console.log(driver)
      const driverDetail = await (await driverService.GetDetails(driver.data.driverId,token)).data
      console.log(driverDetail)
    }
    catch{
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }
   
  return (
    <div className="text-white h-full overflow-scroll">

    </div>
  );
};

export default DriverHome;
