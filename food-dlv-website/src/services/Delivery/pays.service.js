import axios from "axios";

const API_URL = "https://localhost:7093/api/pays";

class PaysService {
    ///取得個人全部紀錄
  GetAllPayRecrodes( id, token ){
    const response = axios.get(API_URL + "/" + id ,{
      headers: {
        responseType: "json",
        Authorization: `bearer ${token}`,
      },
    });
    return response
  }
  ///取得個人當月紀錄
  GetPayRecrodesByMonth(year,month, id, token ){
    const response = axios.get(API_URL + "/" + year+ "/" + month+ "/" + id ,{
      headers: {
        responseType: "json",
        Authorization: `bearer ${token}`,
      },
    });
    return response
  }

}

export default new PaysService();