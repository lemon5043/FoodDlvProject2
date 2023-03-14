import axios from "axios";

const API_URL = "https://localhost:7093/api/Order";

class OrderService{
    getOrderInfo(cartId, address, fee){
        const response = axios.get((API_URL)+ `/OrderInfo?cartId=${cartId}&address=${address}&fee=${fee}`);
        return response;
    }

    postOrderEstablished(memberId, storeId, fee , address) 


};

export default new OrderService();