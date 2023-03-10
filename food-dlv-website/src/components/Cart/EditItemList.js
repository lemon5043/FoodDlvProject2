import ProductItems from '../../components/Cart/ProductItems';


const arr = [];
const EditItemList = ({listData}) => {

   

    return(
        <div>
            <h1>EditItemList</h1>
            {
                listData.map(item => <ProductItems key={item.id} />)
            }
        </div>
    )
}

export default EditItemList;