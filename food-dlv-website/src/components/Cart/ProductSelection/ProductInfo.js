const ProductInfo = ({product}) => {

    return(
        <div>
            <h1>{product.productName}</h1>
            <p>{product.productContent}</p>
            <img src={product.photo} alt={product.productName} />
            <p>Price:{product.unitPrice}NTD</p>                      
        </div>
    )
}

export default ProductInfo;