import React, { useEffect } from "react";
import StoreService from "../../services/store.service";

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
