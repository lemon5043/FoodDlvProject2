import Item from './Item';

const List = ({items, getItem}) => { 
    return(
        <div>
            <h1>List</h1>
            {
                items.map(item => {
                   return <Item key={item.id} item={item} getItem={getItem} />
                })
            }
        </div>
    )
}

export default List;