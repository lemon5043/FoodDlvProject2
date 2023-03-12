import React, { useEffect, useState } from "react";
import StoreService from "../../services/Store/store.service";
import StoreCard from "./storeCard";
import { Box } from "../../components/Style/form-styling";
import { useParams } from "react-router-dom";

const Store = () => {
  const params = useParams();
  const addressName = params.addressName;

  let [data, setData] = useState([]);
  let [ctg, setCtg] = useState([]);
  // let [page, setPage] = useState(1);
  // let [enableLoadMoreData, setEnableLoadMoreData] = useState(false);

  const selectCategory = async () => {
    const category = await StoreService.getCategories();
    setCtg(category.data);
  };

  const search = async (addr) => {
    const res = await StoreService.getByAddress(addr);
    setData(res.data);
  };
  useEffect(() => {
    search(addressName);
    selectCategory();
  }, []);

  return (
    <div className="mx-12 h-full">
      <main className="flex h-full mt-6">
        {/* 篩選部分 */}
        <section className="max-w-xs h-3/4 sticky border-2 border-solid border-black">
          <div className="text-2xl font-bold">所有店家</div>
          {ctg &&
            ctg.map((d) => {
              return <p key={d.id}>{d.categoryName}</p>;
            })}
          {/**下面的這個 div 做完要刪掉 */}
          <div className=" w-64"></div>
        </section>

        {/* 餐廳選擇部分 */}
        <section className="flex flex-wrap justify-center">
          {data &&
            data.map((d) => {
              return <StoreCard data={d} key={d.id} />;
            })}
        </section>
      </main>
    </div>
  );
};

export default Store;
