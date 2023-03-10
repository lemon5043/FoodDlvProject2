import axios from "axios";

const API_URL = "https://localhost:7093/api/DeliveryRecords";

class DeliveryRecordsService {
    ///取得個人全部紀錄
  GetAllRecrodes( id, token ){
    const response = axios.get(API_URL + "/" + id ,{
      headers: {
        responseType: "json",
        Authorization: `bearer ${token}`,
      },
    });
    return response
  }
  ///取得個人當月紀錄
  GetRecrodesByMonth(year,month, id, token ){
    const response = axios.get(API_URL + "/" + year+ "/" + month+ "/" + id ,{
      headers: {
        responseType: "json",
        Authorization: `bearer ${token}`,
      },
    });
    return response
  }

}

export default new DeliveryRecordsService();