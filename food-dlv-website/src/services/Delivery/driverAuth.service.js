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
    return axios.post(API_URL + "/login", { account, password });
  }

  logout() {
    localStorage.removeItem("driver");
  }

  getCurrentDriver() {
    return JSON.parse(localStorage.getItem("driver"));
  }
}

export default new DriverAuthService();
