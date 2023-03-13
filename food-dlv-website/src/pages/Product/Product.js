import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import StoreService from "../../services/Store/store.service";
import BgImage from "../../components/Product/BgImage";

const Product = () => {
  const params = useParams();
  const storeId = params.storeId;
  let [data, setData] = useState([]);

  const displayProduct = async (id) => {
    const res = await StoreService.getStoreDetail(id);
    setData(res.data[0]);
  };

  useEffect(() => {
    displayProduct(storeId);
  }, []);

  return (
    <div>
      {data.length !== 0 && <BgImage data={data} />}
      123
    </div>
  );
};

export default Product;
