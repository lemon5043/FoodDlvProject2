import React, { useEffect, useState } from "react";
import StoreService from "../../services/store.service";
import StoreComponent from "./storeComponent";
import { Box } from "../../components/Style/form-styling";

const Store = (props) => {
  let [data, setData] = useState([]);
  // let [page, setPage] = useState(1);
  // let [enableLoadMoreData, setEnableLoadMoreData] = useState(false);

  const search = async () => {
    setData(props.location.state.storeData);
    console.log(data);
    // setData(store);
    // setEnableLoadMoreData(true);
  };
  useEffect(() => {
    search();
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
