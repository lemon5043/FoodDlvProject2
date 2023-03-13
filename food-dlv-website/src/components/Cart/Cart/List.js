import CartDetail from "./CartDetail";

const List = ({details, getCartDetail}) => {
    return(
        <div>
            <h1>List</h1>
            {
                details.map(detail => {
                    return<CartDetail key={detail.identifyNum} detail={detail} getCartDetail={getCartDetail}/>
                })
            }
        </div>
    )
}

export default List;