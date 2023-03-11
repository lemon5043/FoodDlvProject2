import {useState} from 'react';

const Item = ({item, getItem}) => {

    const[checked, setChecked] = useState(false);
    function itemChange(e){
        setChecked(e.target.checked);
        getItem(item.id)
    }
     
    return(
        <div>
            <input type='checkbox' checked={checked} onChange={itemChange}/>
            <p>{item.itemName}</p>
            <p>{item.customizationItemPrice}</p>
        </div>
    )
}

export default Item;