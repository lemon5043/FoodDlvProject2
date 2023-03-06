import axios from "axios";

const API_URL = "https://localhost:7093/api/DeliveryDrivers";

class DriverAuthService {
  register(
    Account,
    Password,
    FirstName,
    LastName,
    Phone,
    BankAccount,
    Idcard,
    VehicleRegistration,
    DriverLicense
  ) {
    return axios.post(API_URL + "/register", {
      Account,
      Password,
      FirstName,
      LastName,
      Phone,
      BankAccount,
      Idcard,
      VehicleRegistration,
      DriverLicense,
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
