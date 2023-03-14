import React from "react";

const BgImage = ({ data }) => {
  const storeImg = require(`../../assets/images/public/Stores/${data.photo}`);
  return (
    <div>
      <img
        src={storeImg}
        alt=""
        className="w-full object-cover object-center"
        style={{ height: "40vh" }}
      />
    </div>
  );
};

export default BgImage;
