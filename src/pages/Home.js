import React from "react";
import "tw-elements";
import Macu from "../images/macu.jpg";
import PrinceValley from "../images/本物洋食.png";
import Threemom from "../images/threemom.jpeg";
import Mcdonald from "../images/mcdonald.jpg";
import Table from "../images/table.jpg";

const Home = () => {
  return (
    // recommend restaurant carousel
    <div>
      <div className="flex flex-row mt-20 justify-center overflow-x-auto">
        <div className="flex-shrink-0 w-80 p-4">
          <div className="bg-white h-96 shadow-lg rounded-lg overflow-hidden">
            <img
              className="w-full h-48 object-cover object-center"
              src={Macu}
              alt="food recommendation"
            />
            <div className="p-4">
              <h3 className="font-bold text-gray-800 text-xl mb-2">
                麻古茶坊-中壢新生店
              </h3>
              <p className="text-gray-600 text-sm">飲料、</p>
            </div>
          </div>
        </div>

        <div className="flex-shrink-0 w-80 p-4">
          <div className="bg-white  h-96 shadow-lg rounded-lg overflow-hidden">
            <img
              className="w-full h-48 object-cover object-center"
              src={PrinceValley}
              alt="food recommendation"
            />
            <div className="p-4">
              <h3 className="font-bold text-gray-800 text-xl mb-2">Card 2</h3>
              <p className="text-gray-600 text-sm">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua.
              </p>
            </div>
          </div>
        </div>

        <div className="flex-shrink-0 w-80 p-4">
          <div className="bg-white  h-96 shadow-lg rounded-lg overflow-hidden">
            <img
              className="w-full h-48 object-cover object-center"
              src={Threemom}
              alt="food recommendation"
            />
            <div className="p-4">
              <h3 className="font-bold text-gray-800 text-xl mb-2">Card 3</h3>
              <p className="text-gray-600 text-sm">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua.
              </p>
            </div>
          </div>
        </div>

        <div className="flex-shrink-0 w-80 p-4">
          <div className="bg-white  h-96 shadow-lg rounded-lg overflow-hidden">
            <img
              className="w-full h-48 object-cover object-center"
              src={Mcdonald}
              alt="food recommendation"
            />
            <div className="p-4">
              <h3 className="font-bold text-gray-800 text-xl mb-2">Card 4</h3>
              <p className="text-gray-600 text-sm">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua.
              </p>
            </div>
          </div>
        </div>

        <div className="flex-shrink-0 w-80 p-4">
          <div className="bg-white  h-96 shadow-lg rounded-lg overflow-hidden">
            <img
              className="w-full h-48 object-cover object-center"
              src="https://source.unsplash.com/random/800x600"
              alt="food recommendation"
            />
            <div className="p-4">
              <h3 className="font-bold text-gray-800 text-xl mb-2">Card 5</h3>
              <p className="text-gray-600 text-sm">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do
                eiusmod tempor incididunt ut labore et dolore magna aliqua.
              </p>
            </div>
          </div>
        </div>
      </div>
      <div>
        <div className="flex">
          <img className="w-1/5 m-auto" src={Table} alt="" />
        </div>
      </div>
    </div>
  );
};

export default Home;
