import axios from 'axios';

const AddToCart_API_URL = "https://localhost:7093/api/Cart/AddToCart";
const CartInfo_API_URL = "https://localhost:7093/api/Cart/CartInfo";
const UpdateCart_API_URL = "https://localhost:7093/api/Cart/UpdateCart";
const RemoveDetail_API_URL = "https://localhost:7093/api/Cart/RemoveDetail";
const DeleteCart_API_URL = "https://localhost:7093/api/Cart/DeleteCart";

class CartService{
    getAddToCart(memberId, storeId, productId, itemId, qty){
        return axios.post(
            `${AddToCart_API_URL}?memberId=${memberId}&storeId=${storeId}&productId=${productId}&itemId=${itemId}&qty=${qty}`
        );
    };
    getCartInfo(memberId, storeId){
        return axios.get(
            `${CartInfo_API_URL}?memberId=${memberId}&storeId=${storeId}`
        );
    };
    getUpdateCart(identifyNum, storeId, productId, itemId, qty){
        return axios.post(
            `${UpdateCart_API_URL}?identifyNum=${identifyNum}&storeId=${storeId}&productId=${productId}&itemId=${itemId}&qty=${qty}`
        );
    };
    getRemoveDetail(identifyNum){
        return axios.post(
            `${RemoveDetail_API_URL}?identifyNum=${identifyNum}`
        );
    };
    getDeleteCart(memberId, storeId){
        return axios.delete(
            `${CartInfo_API_URL}?memberId=${memberId}&storeId=${storeId}`
        );
    };    
}

export default new CartService();