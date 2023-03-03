import React, { useEffect } from "react";
import StoreService from "../../services/store.service";
// import testPhoto from "../../../../FoodDlvProject2/wwwroot/img/stores/Boujee 佐-烤肉飯20233214460桃園市中壢區新生路三段12號.jpg";

const Store = () => {
  const search = async () => {
    let result = await StoreService.getStore();
    console.log(result.data);
  };
  // useEffect(() => {
  //   search();
  // }, []);

  return (
    <div>
      <button onClick={search}>測試</button>
    </div>
  );
};

export default Store;
