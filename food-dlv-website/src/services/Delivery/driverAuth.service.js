import axios from "axios";

const API_URL = "https://localhost:7093/api/DeliveryDrivers";

class DriverAuthService {
  register(formData) {
    return axios.post(API_URL + "/register", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  }

  login(account, password) {
    const response = axios.post(API_URL + "/login", { account, password });
    return response;
  }
  GetDriver(response) {
    const res = axios.get(API_URL, {
      headers: {
        responseType: "json",
        Authorization: `bearer ${response}`,
      },
    });
    return res;
  }

  logout() {
    localStorage.removeItem("driver");
  }

  getCurrentDriver() {
    return JSON.parse(localStorage.getItem("driver"));
  }
}

export default new DriverAuthService();
