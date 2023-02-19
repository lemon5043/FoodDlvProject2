import React from "react";

const HomeCard = ({ store }) => {
  console.log(store);
  return (
    <div className="flex-shrink-0 w-80 p-4">
      <div className="bg-white h-96 shadow-lg rounded-lg overflow-hidden">
        <img
          className="w-full h-48 object-cover object-center"
          src={store.image}
          alt="food recommendation"
        />
        <div className="p-4">
          <h3 className="font-bold text-gray-800 text-xl mb-2">
            {store.title}
          </h3>
          <p className="text-gray-600 text-sm">{store.type}</p>
          <p className="text-gray-600 text-sm">送達時間約{store.time}分鐘</p>
        </div>
      </div>
    </div>
  );
};

export default HomeCard;
