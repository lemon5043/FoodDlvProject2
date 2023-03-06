import axios from "axios";

const API_URL = "https://localhost:7093/api/Stores/";

class StoreService {
  //找到所有課程，這邊之後會改寫成地址範圍內所有課程
  getStore() {
    return axios.get(API_URL);
  }
}

export default new StoreService();
