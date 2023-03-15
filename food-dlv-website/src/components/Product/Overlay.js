import { Fragment } from "react";

const Overlay = ({ isOpen, onClose, currentUser }) => {
  return (
    <Fragment>
      {isOpen && (
        <div className="overlay">
          <div
            className="bg-black/50 w-screen h-screen fixed top-0 left-0 cursor-pointer z-20"
            onClick={onClose}
          />
          <div className="bg-white fixed top-0 right-0 bottom-0 left-0 m-auto z-30 p-7 h-fit w-fit">
            <div className="flex justify-end items-center">{currentUser}</div>
          </div>
        </div>
      )}
    </Fragment>
  );
};

export default Overlay;
