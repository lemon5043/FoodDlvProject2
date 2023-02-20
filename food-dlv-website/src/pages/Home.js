import React, { useState } from "react";
import "tw-elements";
import HomeCard from "../components/HomeCard";
import Macu from "../images/macu.jpg";
import PrinceValley from "../images/王子神谷.jpg";
import Threemom from "../images/threemom.jpeg";
import Mcdonald from "../images/mcdonald.jpg";
import Table from "../images/table.jpg";
import Honmono from "../images/honmono.jpg";

const Home = () => {
  // 測試用 array，等 API 寫好後會採用隨機店家挑選
  const stores = [
    {
      id: 1,
      image: Macu,
      title: "麻古茶坊-中壢中正店",
      type: "飲料",
      time: 25,
    },
    {
      id: 2,
      image: PrinceValley,
      title: "王子神谷",
      type: "甜點",
      time: 50,
    },
    {
      id: 3,
      image: Threemom,
      title: "三媽臭臭鍋-實踐店",
      type: "火鍋",
      time: 35,
    },
    {
      id: 4,
      image: Mcdonald,
      title: "麥當勞-中壢新生店",
      type: "飲料",
      time: 30,
    },
    {
      id: 5,
      image: Honmono,
      title: "本物洋食",
      type: "義式",
      time: 50,
    },
  ];

  return (
    <div className="bg-theme-color">
      <div className="flex flex-row pt-20 justify-center overflow-x-auto">
        {stores.map((d) => {
          return <HomeCard store={d} key={d.id} />;
        })}
      </div>
      <div>
        <div className="flex">
          <div className="slogan absolute bottom-36 left-24">
            <h1 className=" text-4xl">想點什麼呢?</h1>
            <h5>
              以上的推薦您滿意嗎?
              <br />
              如不符合您需求請點這裡
            </h5>
          </div>
          <img className="w-1/5 m-auto" src={Table} alt="" />
        </div>
      </div>
    </div>
  );
};

export default Home;
