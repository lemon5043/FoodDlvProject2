import React from 'react';
import CartService from '../../services/Cart/cart.service';

const Cart = () => {
    let[cartData, setCartData] = useState([]);
    let memberId = "test-memberId";
    let storeId = "test-storeId";
  
    const addToCart = () => {
      let request = CartService.getAddToCart();
      setCartData(request.cartData);
    }
  
    useEffect(() => {
      addToCart();
     },[]);
}