import React from "react";

const InputComponent = ({ text, name, type, setInput }) => {
  const inputHandler = (e) => {
    setInput(e.target.value);
  };
  return (
    <div className="mb-2">
      <label
        htmlFor={name}
        className="block text-base font-semibold text-gray-800"
      >
        {text}
      </label>
      <input
        required
        onChange={inputHandler}
        name={name}
        type={type}
        autoComplete="on"
        className="block w-full px-4 py-2 mt-2 text-neutral-700 bg-white border rounded-md focus:border-neutral-400 focus:ring-neutral-300 focus:outline-none focus:ring focus:ring-opacity-40"
      />
    </div>
  );
};

export default InputComponent;
