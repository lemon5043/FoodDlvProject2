import axios from "axios";

const API_URL = "https://localhost:7093/api/MemberAddress";

class AddressService {
  getAddress(memberId) {
    const response = axios.get(API_URL + "/index" + `?memberId=${memberId}`);
    return response;
  }
}
