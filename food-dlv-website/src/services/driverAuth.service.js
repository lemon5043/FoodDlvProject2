import axios from "axios";

const API_URL = "https://localhost:7093/api/";

class DriverAuthService {
  login(account, password) {
    return axios.post(API_URL + "login", { account, password });
  }
}
