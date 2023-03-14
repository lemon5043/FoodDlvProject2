import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import StoreService from "../../services/Store/store.service";
import ProductPage from "./ProductPage";

const Product = () => {
  const params = useParams();
  const storeId = params.storeId;
  let [data, setData] = useState([]);

  const displayProduct = async (id) => {
    const res = await StoreService.getStoreDetail(id);
    console.log(res.data[0]);
    setData(res.data[0]);
  };

  useEffect(() => {
    displayProduct(storeId);
  }, []);

  return (
    <div>
      {data.length !== 0 && (
        <section className="left">
          <ProductPage data={data} />
        </section>
      )}
      <section></section>
    </div>
  );
};

export default Product;
