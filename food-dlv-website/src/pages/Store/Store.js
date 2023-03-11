import React, { useEffect, useState } from "react";
import StoreService from "../../services/Store/store.service";
import StoreComponent from "./storeComponent";
import { Box } from "../../components/Style/form-styling";
import { useLocation } from "react-router-dom";

const Store = () => {
  let [data, setData] = useState("");
  // let [page, setPage] = useState(1);
  // let [enableLoadMoreData, setEnableLoadMoreData] = useState(false);

  const location = useLocation();

  const search = async () => {
    console.log(data);
    // setData(store);
    // setEnableLoadMoreData(true);
  };
  useEffect(() => {
    console.log(location.state);
    if (location.state) {
      setData(location.state);
    }
    console.log(data);
  }, []);

  return (
    <div className="mx-12 h-full">
      <button onClick={search}>測試</button>
      <main className="flex h-full">
        {/* 篩選部分 */}
        <div className="text-2xl font-bold">所有店家</div>
        {/* <Box></Box> */}
        {/* <section className="max-w-xs h-3/4 sticky border-2 border-solid border-black">
          {/**下面的這個 div 做完要刪掉 */}
        {/* <div className=" w-64"></div> */}

        {/* 餐廳選擇部分 */}
        <section className="flex flex-wrap justify-center">
          {/* {data.map((d) => {
            return <StoreComponent data={d} />;
          })} */}
        </section>
      </main>
    </div>
  );
};

export default Store;
