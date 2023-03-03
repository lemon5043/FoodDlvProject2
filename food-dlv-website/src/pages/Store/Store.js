import React, { useEffect, useState } from "react";
import StoreService from "../../services/store.service";
import StoreComponent from "../../components/Store/storeComponent";

const Store = () => {
  let [data, setData] = useState([]);
  // let [page, setPage] = useState(1);
  // let [enableLoadMoreData, setEnableLoadMoreData] = useState(false);

  const search = async () => {
    let result = await StoreService.getStore();
    setData(result.data);
    // setEnableLoadMoreData(true);
  };
  useEffect(() => {
    search();
  }, []);

  return (
    <div>
      <button onClick={search}>測試</button>
      <div className="flex flex-wrap">
        {data.map((d) => {
          return <StoreComponent data={d} />;
        })}
      </div>
    </div>
  );
};

export default Store;
