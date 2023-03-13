import axios from 'axios';

const API_URL = "https://localhost:7093/api/Cart";

class CartService{
    postAddToCart(memberId, storeId, productId, itemId, qty){
        const response = axios.post(API_URL + '/AddToCart',{memberId, storeId, productId, itemId, qty})
        return response;    
    };
    getCartInfo(memberId, storeId){
        const response = axios.get(API_URL + `/CartInfo?memberId=${memberId}&storeId=${storeId}`)
        return response;
    };
    getUpdateCart(identifyNum, storeId, productId, itemId, qty){
        const response = axios.post(API_URL + '/UpdateCart',{identifyNum, storeId, productId, itemId, qty})
        return response;  
    };
    getRemoveDetail(identifyNum){
        const response = axios.post(API_URL + '/RemoveDetail',{identifyNum})
        return response;
    };
    getDeleteCart(memberId, storeId){
        const response = axios.delete(API_URL + '/DeleteCart',{memberId, storeId})
        return response;
    };    
}

export default new CartService();