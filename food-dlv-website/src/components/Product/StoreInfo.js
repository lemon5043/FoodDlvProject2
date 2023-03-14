import React from "react";

const StoreInfo = ({ data }) => {
  return (
    <div className="p-6 border-b-2">
      <h1 className="text-2xl font-semibold">{data.storeName}</h1>
      {data.categoryName.length !== 0 && (
        <p className="text-slate-400">{data.categoryName.join()}</p>
      )}
      <p className="">{data.address}</p>
    </div>
  );
};

export default StoreInfo;
