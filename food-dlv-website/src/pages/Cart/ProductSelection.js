import {useState} from 'react';
import ProductSelectionService from '../../services/Cart/productSelection.service';
import CartService from '../../services/Cart/cart.service';
import ProductInfo from '../../components/Cart/ProductSelection/ProductInfo';
import List from '../../components/Cart/ProductSelection/List';
//import Item from '../../components/Cart/ProductSelection/Item';

const ProductSelection = () => {
  const[memberId, setMemberId] = useState('');
  const[productId, setProductId] = useState('');
  const[state, setState] = useState('');
  const[product, setProduct] = useState(null);
  const[items, setItems] = useState([]);
  const[selectItems,setSelectItems] = useState({});
  const[qty,setQty] = useState(1);

  function textProductId(e){
    setProductId(e.target.value);
  };
  function textState(e){
    setState(e.target.value);
  };
  function textMemberId(e){
    setMemberId(e.target.value);
  };

  function getProduct(){
    ProductSelectionService.getProductSelect(productId, state)
      .then(function(response){
        //console.log(response.data);
        setProduct(response.data);
      })
      .catch(function(error){
        console.log(error);
      });
  };

  function getItem(id){
    const item = product.customizationItems.find((item) => item.id === id);
    if(items.some((item) => item.id === id)){
      setItems(items.filter((item) => item.id !== id))
    }else{
      setItems([...items,item])
    }
  };

  function numberQty(e){
    setQty(parseInt(e.target.value));
  }

  function getSelectItems(items){
    setSelectItems(items);
  }

  function AddToCart(){
    //const selectedItems = items.filter((item) => selectItems.includes(item.id));
    //const itemId = items.map((item) => item.id.toString());
    console.log(memberId, product.storeId, productId, selectItems, qty);
    CartService.postAddToCart(memberId, product.storeId, productId, selectItems, qty)
      .then(function(response){
        console.log(response.data);
      })
      .catch(function(error){
        console.log(error);
      })
  }

  return(
    <div>
      <div>
        <label>MemberId:</label>
        <input type='text' value={memberId} onChange={textMemberId} />
      </div>
      <div>
        <label>ProductId:</label>
        <input type='text' value={productId} onChange={textProductId} />
      </div>
      <div>
        <label>State:</label>
        <input type='text' value={state} onChange={textState} />
      </div>
      <button onClick={getProduct}>GetProduct</button>
      {product && (
        <div>
          <ProductInfo product={product} />
          <List items={product.customizationItems} getItem={getItem} ItemsForSelected={getSelectItems}/>
          <div>
            <label>Quantity:</label>
            <input type="number" value={qty} min={1} onChange={numberQty} />
          </div>
          <button onClick={AddToCart}>AddToCart</button>
        </div>
      )}
    </div>
  );
};

export default ProductSelection;