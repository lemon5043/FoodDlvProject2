import React from "react";

const StoreComponent = ({ data }) => {
  const image = require(`../../../assets/images/public/Stores/${data.photo}`);
  return (
    <div
      className="rounded overflow-hidden mx-2 mb-4 h-60"
      style={{ maxWidth: "15rem" }}
    >
      <img src={image} alt="" className="w-full object-cover h-32" />
      <div className="px-6 py-4">
        <div className="font-bold text-xl">{data.storeName}</div>
      </div>
      <div className="px-6 pt-2 pb-2">
        <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
          #photography
        </span>
        <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
          #travel
        </span>
        <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
          #winter
        </span>
      </div>
    </div>
  );
};

export default StoreComponent;
