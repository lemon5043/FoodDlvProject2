import { useState } from "react";

import CartService from '../../services/Cart/cart.service';
import CartDetail from '../../components/Cart/Cart/CartDetail';
import List from '../../components/Cart/Cart/List';

const Cart = () => {
  const [memberId, setMemberId] = useState('');
  const [storeId, setStoreId] = useState('');
  const [identifyNum, setIdentifyNum] = useState('');
  const [cartDetail, setCartDetail] = useState(null);

  function textMemberId(e){
    setMemberId(e.target.value);
  };
  function textStoreId(e){
    setStoreId(e.target.value);
  };

  function getCartInfo(){
    CartService.getCartInfo(memberId, storeId)
      .then(function(response){
        console.log(response.data);
        setCartDetail(response.data);
      })
      .catch(function(error){
        console.log(error);
      });
  };

  function getCartDetail(identifyNum){
    const detail = cartDetail.find((detail) => detail.identifyNum === identifyNum);
    return detail;
  }


  return (
    <div>
      <div>
        <label>MemberId:</label>
        <input type='text' value={memberId} onChange={textMemberId} />
      </div>
      <div>
        <label>StoreId:</label>
        <input type='text' value={storeId} onChange={textStoreId}/>
      </div>
      <button onClick={getCartInfo}>GetCartInfo</button>
      
      {cartDetail && (
        <div>
          <p>{cartDetail.storeName}</p>
          <p>{cartDetail.total}</p>
          <List details={cartDetail.cartDetails} getCartDetail={getCartDetail}/>
        </div>
      )}
    </div>
  );
};

export default Cart;
