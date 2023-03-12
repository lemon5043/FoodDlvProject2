import React, { useState, useEffect, useRef } from "react";
import { Label, Input, Button } from "../../components/Delivery/form-styling";
import driverAuthService from "../../services/Delivery/driverAuth.service";
import delieveryService from "../../services/Delivery/delievery.service";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr"; //signalr使用
import { Wrapper, Status } from "@googlemaps/react-wrapper";


const DriverMap = () => {
  let [orderId, setOrderId] = useState("")
  let [workingStatus, setWorkingStatus] = useState(false)
  let [longitude, setlongitude] = useState("")
  let [latitude, setlatitude] = useState("")
  let [errorMessage, setErrorMessage] = useState("");

  const ref = useRef(null);
  const [map, setMap] = useState();
  const token = localStorage.getItem("driver")


  useEffect(() => {
    if (ref.current && !map) {
      setMap(new window.google.maps.Map(ref.current, {}));
    }
    //取得經緯度
    setInterval(() => {
      navigator.geolocation.getCurrentPosition((position) => {
        setlongitude(position.coords.longitude)
        setlatitude(position.coords.latitude)
      })
    }, 60000)
  }, [ref, map])

  //上、下線
  const ChangeWorkingStatus = async () => {
    try {
      const driver = (await driverAuthService.GetDriver(token)).data
      await delieveryService.ChangeWorkingStatus(driver.driverId, longitude, latitude)
      if (workingStatus === false) {
        setWorkingStatus(true)
        JoinGroup()
      }
      else {
        setWorkingStatus(false)
        LeaveGrop()
      }
    }
    catch (e) {
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }

  //加入HubGroup
  const JoinGroup = async () => {
    try {
      const driver = (await driverAuthService.GetDriver(token)).data
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7093/OrderHub")
        .configureLogging(LogLevel.Information)
        .build()
      console.log(driver)
      //監聽由商店傳來的OrderId
      connection.on("AssignOrder", (OrderId) => {
        console.log(OrderId)
        const storeInfo = delieveryService.OrderAasignment(OrderId)
        setOrderId(storeInfo.orderId)
        //todo 跳出視窗顯示接受or取消
      })
      await connection.start()
      await connection.invoke("JoinGroup", (driver.driverId, driver.role))
    }
    catch (e) {
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }

  //離開HubGroup
  const LeaveGrop = async () => {
    try {
      const driver = (await driverAuthService.GetDriver(token)).data
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7093/OrderHub")
        .configureLogging(LogLevel.Information)
        .build()

      await connection.invoke("LeaveGroup", (driver.driverId, driver.role))
    }
    catch (e) {
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }

  return <div ref={ref}>DriverMap
    <form className="mt-6" onSubmit={ChangeWorkingStatus}>
      <Button type="submit">上線</Button>
    </form>
  </div>

    ;
};

export default DriverMap;
