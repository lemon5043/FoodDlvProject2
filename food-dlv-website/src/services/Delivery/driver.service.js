import axios from "axios";

const API_URL = "https://localhost:7093/api/DeliveryDrivers";

class DriverService {
  GetDetails( id, token ){
    const response = axios.get(API_URL + "/" + id ,{
      headers: {
        responseType: "json",
        Authorization: `bearer ${token}`,
      },
    });
    return response
  }
  DriverEditSaveChange(formData){
    return axios.put(API_URL ,formData ,{
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });   
  }

}

export default new DriverService();
