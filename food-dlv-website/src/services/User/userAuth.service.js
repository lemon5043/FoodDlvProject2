import axios from "axios";

const API_URL = "https://localhost:7093/api/Members";

class UserAuthService {
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
    localStorage.removeItem("user");
  }

  getCurrentUser() {
    return JSON.parse(localStorage.getItem("user"));
  }
}

export default new UserAuthService();
