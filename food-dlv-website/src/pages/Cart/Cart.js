import { useState, useEffect } from "react";
import cartService from "../../services/Cart/cart.service";

import CartService from "../../services/Cart/cart.service";

const Cart = ({ currentUser, storeId }) => {
  const memberId = currentUser.userId;
  //State設定
  // const [memberId, setMemberId] = useState(currentUser.userId);
  // const [storeId, setStoreId] = useState("");
  const [identifyNum, setIdentifyNum] = useState("");
  const [cartDetail, setCartDetail] = useState(null);

  //替代'Member'點擊'購物車內容'需要的輸入值, 若串接完成, 可刪除
  // function textMemberId(e) {
  //   setMemberId(e.target.value);
  // }
  // function textStoreId(e) {
  //   setStoreId(e.target.value);
  // }

  //展示購物車內容
  function CartInfo() {
    CartService.getCartInfo(memberId, storeId)
      .then(function (response) {
        console.log(response.data);
        setCartDetail(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  //條件:在memberId或storeId改變時, 重新獲取購物車內容
  useEffect(function () {
    if (memberId && storeId) {
      CartInfo();
    }
  }, []);

  //條件:在cartDetail改變時, 重新獲取購物車的產品明細
  useEffect(
    function () {
      if (cartDetail) {
        setCartDetail(cartDetail);
      }
    },
    [cartDetail]
  );

  //修改購物車'被選取'商品明細
  function UpdateDetail(detail) {
    console.log(
      detail.identifyNum,
      detail.storeId,
      detail.productId,
      detail.itemId,
      detail.qty
    );
    CartService.postUpdateCart(
      detail.identifyNum,
      detail.storeId,
      detail.productId,
      detail.itemId,
      detail.qty
    )
      .then(function (response) {
        console.log(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  //刪除購物車'被選取'商品明細
  function RemoveDetail(identifyNum) {
    console.log(identifyNum);
    CartService.postRemoveDetail(identifyNum)
      .then(function (response) {
        console.log(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  //清空購物車
  function DeleteCart(memberId, storeId) {
    cartService
      .deleteDeleteCart(memberId, storeId)
      .then(function (response) {
        console.log(response.data);
      })
      .catch(function (error) {
        console.log(error);
      });
  }

  return (
    <div>
      <div>
        <label>MemberId:{currentUser.userId}</label>
        {/* <input type="text" value={memberId} onChange={textMemberId} /> */}
      </div>
      <div>
        <label>StoreId:{storeId}</label>
      </div>
      <button onClick={() => CartInfo()}>GetCartInfo</button>

      {cartDetail && (
        <div>
          <p>{cartDetail.storeName}</p>
          <button onClick={() => DeleteCart(memberId, cartDetail.storeId)}>
            DeleteCart
          </button>
          <p>{cartDetail.total}</p>

          {/* 展開購物車的CartDetail */}
          <div>
            {cartDetail.cartDetails.map((detail) => {
              return (
                <div key={detail.identifyNum}>
                  <p>{detail.productName}</p>
                  <p>{detail.itemName}</p>
                  <p>{detail.qty}</p>
                  <p>{detail.subTotal}</p>

                  {/* 按鈕Update:回到'ProductSelection'頁面, 並記憶該筆商品明細的'客製化選項'與'商品數量' */}
                  {/* 在該頁面重新選擇完成後, 按鈕'確認修改':onClick={UpdateDetail} */}
                  <button onClick={null}>Update</button>
                  <button onClick={() => RemoveDetail(detail.identifyNum)}>
                    Delete
                  </button>
                </div>
              );
            })}
          </div>
        </div>
      )}
    </div>
  );
};

export default Cart;
