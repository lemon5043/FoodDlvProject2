import axios from "axios";

const API_URL = "https://localhost:7093/api/Cart";

class CartService {
  postAddToCart(memberId, storeId, productId, itemId, qty) {
    const response = axios.post(API_URL + "/AddToCart", {
      MemberId: memberId,
      StoreId: storeId,
      ProductId: productId,
      ItemsId: itemId,
      Qty: qty,
    });
    return response;
  }

  getCartInfo(memberId, storeId) {
    const response = axios.get(
      API_URL + `/CartInfo?memberId=${memberId}&storeId=${storeId}`
    );
    return response;
  }

  postUpdateCart(identifyNum, storeId, productId, itemId, qty) {
    const response = axios.post(API_URL + "/UpdateCart", {
      IdentifyNum: identifyNum,
      StordId: storeId,
      ProductId: productId,
      ItemsId: itemId,
      Qty: qty,
    });
    return response;
  }

  async postRemoveDetail(identifyNum) {
    const response = await axios.post(API_URL + "/RemoveDetail", {
      identifyNum: identifyNum,
    });
    return response;
  }

  deleteDeleteCart(memberId, storeId) {
    const response = axios.delete(API_URL + "/DeleteCart", {
      memberId,
      storeId,
    });
    return response;
  }
}

export default new CartService();
