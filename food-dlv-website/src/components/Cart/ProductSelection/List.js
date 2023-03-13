import {useState} from 'react';
import Item from './Item';

const List = ({items, ItemsForSelected}) => { 

    const[selectItems, setSelectItems] = useState([]);

    function ItemsForSelected(itemId, isChecked){
        if(isChecked){
            setSelectItems([...selectItems, itemId]);
            
        }else{
            setSelectItems(selectItems.filter((id) => id !== itemId));            
        };
    }

    return(
        <div>
            <h1>List</h1>
            {
                items.map(item => {
                   return <Item key={item.id} item={item} ItemsForSelected={ItemsForSelected} />
                })
            }
            {selectItems}        
        </div>
    )
}

export default List;