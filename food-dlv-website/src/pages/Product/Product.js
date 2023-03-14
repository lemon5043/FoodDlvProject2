import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import StoreService from "../../services/Store/store.service";
import ProductPage from "./ProductPage";
import Cart from "../Cart/Cart";

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
    <div className="flex">
      {data.length !== 0 && (
        <section className="left w-4/5">
          <ProductPage data={data} />
        </section>
      )}
      <aside className="right sticky top-4 w-1/5">
        <Cart />
      </aside>
    </div>
  );
};

export default Product;
