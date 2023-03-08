import axios from "axios";

const API_URL = "https://localhost:7093/api/Stores/";

class StoreService {
  //找到所有店家，這邊之後會改寫成地址範圍內所有店家
  getByAddress(address) {
    if (address === "") return;

    return axios.get(
      API_URL + "getSomeStoresIfIMAt/" + address + "?pageNum=1&storeNum=10"
    );
  }
}

export default new StoreService();
