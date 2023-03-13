import React, { useState, useEffect, useRef } from "react";
import { Label, Input, Button } from "../../components/Delivery/form-styling";
import driverAuthService from "../../services/Delivery/driverAuth.service";
import delieveryService from "../../services/Delivery/delievery.service";
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr"; //signalr使用
import {useJsApiLoader,GoogleMap,Marker,DirectionsRenderer} from "@react-google-maps/api"//googleMap使用




const DriverMap = () => {
  const { isLoaded } = useJsApiLoader({
    googleMapsApiKey: process.env.REACT_APP_GOOGLE_MAPS_API_KEY,
  })
  let [orderId, setOrderId] = useState(0)
  let [memberId, setMemberId] = useState(0)
  let [storeName, setStoreName] = useState("")
  let [workingStatus, setWorkingStatus] = useState(false)
  let [longitude, setlongitude] = useState(0)
  let [latitude, setlatitude] = useState(0)
  let [errorMessage, setErrorMessage] = useState("");
  let [map, setMap] = useState(/** @type google.maps.Map */ (null))
  const [directionsResponse, setDirectionsResponse] = useState(null)
  const [distance, setDistance] = useState(0)
  const [duration, setDuration] = useState(0)
  //let center ={lat:latitude, lng:longitude}

  const token = localStorage.getItem("driver")


  
  useEffect(() => {
    //取得經緯度
    function GetLocation(){
      navigator.geolocation.getCurrentPosition((position) => {
        setlongitude(position.coords.longitude)
        setlatitude(position.coords.latitude)
    })}
    let timer;
    timer && clearInterval(timer)
    timer = setInterval(GetLocation(), 1000)
    },[])

    //路程計算
  async function calculateRoute(destination) {
    // eslint-disable-next-line no-undef
    const directionsService = new google.maps.DirectionsService()
    const results = await directionsService.route({
      origin: [latitude,longitude],
      destination: destination,
      // eslint-disable-next-line no-undef
      travelMode: google.maps.TravelMode.DRIVING,
    })
    setDirectionsResponse(results)
    setDistance(results.routes[0].legs[0].distance.text)
    setDuration(results.routes[0].legs[0].duration.text)
  }

  //清除路線
  function clearRoute() {
    setDirectionsResponse(null)
    setDistance('')
    setDuration('')
  }

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
  async function JoinGroup() {
    try {
      const driver = (await driverAuthService.GetDriver(token)).data
      const connection = new HubConnectionBuilder()
        .withUrl("https://localhost:7093/OrderHub")
        .configureLogging(LogLevel.Information)
        .build()
      console.log(driver)
      //監聽由商店傳來的OrderId
      connection.on("AssignOrder", async (OrderId) => {
        const storeInfo = await delieveryService.OrderAasignment(OrderId)
        setOrderId(storeInfo.orderId)
        //todo 跳出視窗顯示=>接受
        NavationToStore(OrderId,driver.driverId)
        //todo 跳出視窗顯示=>取消
      })
      await connection.start()
      await connection.invoke("JoinGroup", (driver.driverId, driver.role))
    }
    catch (e) {
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }

  //離開HubGroup
  async function LeaveGrop(){
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

  //向餐廳導航
  async function NavationToStore(orderId,driverId) {
    try{
        const orderDetail = await delieveryService.OrderAccept(orderId,driverId)
        setStoreName(orderDetail.StoreName)
        setMemberId(orderDetail.MemberId)
        calculateRoute(orderDetail.StoreAddress)
    }catch(e){
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }      
  }

  //確認取餐，回傳外送地址
  async function NavationToCustomer(orderId) {
    try{
      clearRoute()
      const customerAddress = await delieveryService.NavationToCustomer(orderId)
      calculateRoute(customerAddress)
    }catch(e){
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }      
  }

  //todo 餐點抵達
  async function OrderArrive(){
    try {
      clearRoute()
      const driver = (await driverAuthService.GetDriver(token)).data
      await delieveryService.DeliveryArrive(orderId, driver.driverId,distance)

      const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7093/OrderHub")
      .configureLogging(LogLevel.Information)
      .build()
      await connection.start()
      await connection.invoke("JoinGroup", (memberId, "Member"))//連上MemberGroup
      await connection.invoke("OrderArrive", (memberId, orderId))//傳送訂單
      await connection.invoke("LeaveGroup", (memberId, "Member"))//離開MemberGroup
    }
    catch (e) {
      setErrorMessage(e.response.data.wrongAccountOrPassword[0]);
    }
  }

  return <GoogleMap 
            defaultCenter={{ lat: 0, lng: 0 }}
            center={{lat:latitude,lng:longitude}} zoom={16} 
            mapContainerStyle={{ width: '100%', height: '100%' }} 
            options={{
              zoomControl: false,
              streetViewControl: false,
              mapTypeControl: false,
              fullscreenControl: false,
              }}
            onLoad={map => setMap(map)}
          >DriverMap
    <Marker position={{lat:latitude,lng:longitude}} />

    <form className="mt-6" onSubmit={ChangeWorkingStatus}>
      <Button type="submit">上線</Button>
    </form>
   
       <Button
            aria-label='center back'
            isRound
            onClick={() => {
              map.panTo({lat:latitude,lng:longitude})
              map.setZoom(16)
            }}
          >置中</Button>
  </GoogleMap>

    ;
};

export default DriverMap;
