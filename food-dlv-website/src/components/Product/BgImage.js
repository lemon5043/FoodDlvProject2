import React from "react";

const BgImage = ({ data }) => {
  const storeImg = require(`../../assets/images/public/Stores/${data.photo}`);
  return (
    <div>
      <img src={storeImg} alt="" />
    </div>
  );
};

export default BgImage;
