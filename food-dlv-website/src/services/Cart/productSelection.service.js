import axios from 'axios';

const API_URL = 'https://localhost:7093/api/ProductSelection/';

class ProductSelectionService{
    getProductSelect(productId,state){
        return axios.get(          
            `${API_URL}?productId=${productId}&status=${state}`
        );
    }}

export default new ProductSelectionService();