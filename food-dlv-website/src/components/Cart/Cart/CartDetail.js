import {useState} from 'react';

import cartService from '../../../services/Cart/cart.service';
const detail = ({detail, getCartDetail}) => {
        
    function UpdateDetail(detail){
        cartService.getUpdateCart(
            detail.identifyNum, detail.storeId, detail.productId, detail.itemId, detail.qty
        )
    };

    function RemoveDetail(detail){
        cartService.getRemoveDetail(
            detail.identifyNum
        )
    };
    
    return(
        <div>
            <p>{detail.productName}</p>
            <p>{detail.itemName}</p>
            <p>{detail.qty}</p>
            <p>{detail.subTotal}</p>
            <button>Update</button>
            <button>Delete</button>
        </div>
    )
}

export default detail;