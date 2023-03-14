import React from "react";
import BgImage from "../../components/Product/BgImage";
import ProductInfo from "../../components/Product/ProductInfo";
import StoreInfo from "../../components/Product/StoreInfo";

const ProductPage = ({ data }) => {
  return (
    <div>
      <BgImage data={data} />
      <StoreInfo data={data} />
      <ul className="flex flex-wrap justify-center">
        {data.products.length !== 0 &&
          data.products.map((d) => {
            return <ProductInfo products={d} key={d.id} />;
          })}
      </ul>
      {data.products.length === 0 && (
        <div className="flex items-center justify-center">
          該店家目前沒有產品上架喔!
        </div>
      )}
    </div>
  );
};

export default ProductPage;
