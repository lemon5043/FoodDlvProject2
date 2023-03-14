import React from "react";

const ProductInfo = ({ products }) => {
  return (
    <div className="p-6 border-b-2">
      <h3 className="font-semibold text-lg">{products.productName}</h3>
      <p className="text-slate-400">{products.productContent}</p>
    </div>
  );
};

export default ProductInfo;
