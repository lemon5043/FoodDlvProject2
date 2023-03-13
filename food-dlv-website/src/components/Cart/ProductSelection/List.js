import { useState } from "react";
import Item from "./Item";

const List = ({ items, toggleItem }) => {
  //   const [selectItems, setSelectItems] = useState([]);

  //   function ItemsForSelected(itemId, isChecked) {
  //     if (isChecked) {
  //       setSelectItems([...selectItems, itemId]);
  //     } else {
  //       setSelectItems(selectItems.filter((id) => id !== itemId));
  //     }
  //   }

  return (
    <div>
      <h1>List</h1>
      {items.map((item) => {
        return <Item key={item.id} item={item} toggleItem={toggleItem} />;
      })}
    </div>
  );
};

export default List;
