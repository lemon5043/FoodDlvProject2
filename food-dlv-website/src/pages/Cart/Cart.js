import {useState} from 'react';

import ProductInfo from '../../components/Cart/ProductInfo';
import EditItemList from '../../components/Cart/EditItemList';
import ProductSelection from '../../pages/Cart/ProductSelection';


const Cart = () => {
  const[data, setData] = useSatate([])

  return <div>   
    <ProductInfo />
    <EditItemList listData={data} />
    <ProductSelection />
  </div>;
};

export default Cart;
