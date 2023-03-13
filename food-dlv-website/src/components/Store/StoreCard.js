import React from "react";
import { Link } from "react-router-dom";

const StoreComponent = ({ data }) => {
  const image = require(`../../assets/images/public/Stores/${data.photo}`);
  const categories = data.categoryName;
  const distance = Math.round(data.distance) * 5 + 20;
  const test = 20;

  return (
    <div className="overflow-hidden mx-2 mb-4 w-80 h-72">
      <Link to={"/product/" + data.id}>
        <figure className="h-3/5 overflow-hidden relative">
          <img
            src={image}
            alt=""
            className="w-full h-full  object-cover relative transition hover:scale-110"
          />
          <div className="h-1/4 w-1/6 bg-white absolute top-0 right-0 flex justify-center items-center">
            <p>{distance}分</p>
          </div>
        </figure>
        <div className="px-6 py-4">
          <div className="font-bold text-xl">{data.storeName}</div>
        </div>
        <div className="px-6 pt-2 pb-2">
          <span className="inline-block bg-gray-200 rounded-full px-3 py-1 text-sm font-semibold text-gray-700 mr-2 mb-2">
            #photography
          </span>
        </div>
      </Link>
    </div>
  );
};

export default StoreComponent;
