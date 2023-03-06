import axios from "axios";

const API_URL = "https://localhost:7093/api/DeliveryDrivers";

class DriverAuthService {
  register(
    Account,
    Password,
    FirstName,
    LastName,
    Phone,
    Gender,
    BankAccount,
    Idcard,
    Birthday,
    Email,
    VehicleRegistration,
    DriverLicense
  ) {
    return axios.post(API_URL + "/register", {
      Account,
      Password,
      FirstName,
      LastName,
      Phone,
      Gender,
      BankAccount,
      Idcard,
      Birthday,
      Email,
      VehicleRegistration,
      DriverLicense,
    });
  }

  login(account, password) {
    return axios.post(API_URL + "login", { account, password });
  }
}

export default new StoreService();
