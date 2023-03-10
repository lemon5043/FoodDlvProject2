import axios from "axios";

const API_URL = "https://localhost:7093/api/delievery";

class DelieveryService {
  ChangeWorkingStatus(formData){
    return axios.put(API_URL + "/ChangeWorkingStatus",formData ,{
      headers: {
        "Content-Type": "application/json",
      },
    });   
  }

  UpdateLocation(formData){
    return axios.put(API_URL + "/UpdateLocation",formData ,{
      headers: {
        "Content-Type": "application/json",
      },
    });   
  }

  OrderAasignment(orderId){
    const response = axios.get(API_URL + "/OrderAasignment" + "/" + orderId);
    return response   
  }

  OrderAccept(orderId,driverId){
    const response = axios.get(API_URL + "/OrderAccept" + "/" + orderId+ "/" + driverId);
    return response   
  }

  GetCancellation(){
    const response = axios.get(API_URL + "/Cancellation");
    return response   
  }

  SaveCancellation(formData){
    return axios.post(API_URL + "/Cancellation",formData,{
      headers: {
        "Content-Type": "application/json",
      },
    });
  }

  OrderArrive(orderId){
    const response = axios.get(API_URL + "/" + orderId);
    return response   
  }

  DeliveryArrive(formData){
    return axios.put(API_URL + "/" ,formData ,{
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });   
  }
}

export default new DelieveryService();
