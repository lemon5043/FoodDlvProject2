import React from "react";
import driverAuthService from "../../services/Delivery/driverAuth.service";
import {HubConnectionBuilder,LogLevel} from "@microsoft/signalr"//signalr使用

const DriverMap = () => {
  const token = localStorage.getItem("driver")
  const JoinGroup =async()=>{
    try{
      const driver = (await driverAuthService.GetDriver(token)).data
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7093/OrderHub")
        .configureLogging(LogLevel.Information)
        .build()

        connection.on("AssignOrder",(OrderId)=>{
          console.log(OrderId)
          //todo 觸發回傳訂單
        })
         await connection.start()
         await connection.invoke("JoinGroup",(driver.driverId,driver.role))
    }
    catch(e){

    }
  }
  return <div>DriverMap</div>;
};

export default DriverMap;
