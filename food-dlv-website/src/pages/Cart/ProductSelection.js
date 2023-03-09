import React, { useEffect, useState } from "react";
import ProductSelectionService from "../../services/Cart/productSelection.service";

const ProductSelect = ({productId, state}) => {
  useEffect(() => {
    ProductSelectionService.getProductSelect(2, true)
      .then((response) =>{
        console.log(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  },[productId, state]);

  return(
    //渲染資料
    null
  );
}

export default ProductSelect;