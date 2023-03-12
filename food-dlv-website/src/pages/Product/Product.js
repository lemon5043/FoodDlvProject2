import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import StoreService from "../../services/Store/store.service";

const Product = () => {
  const params = useParams();
  const storeId = params.storeId;

  let [data, setData] = useState([]);

  const displayProduct = async (id) => {
    const res = await StoreService.getStoreDetail(id);
    setData(res.data);
    console.log(res.data);
  };

  useEffect(() => {
    displayProduct(storeId);
  }, []);

  return <div>Product</div>;
};

export default Product;
