import { authenticationReducer } from './authentication/authentication.reducers'
import { cartReducer } from './cart/cart.reducers'
import { httpReducer } from './http/http.reducers'
import { ordersReducer } from './orders/orders.reducers'
import { productsReducer } from './products/products.reducers'

export const appReducers = {
  authentication: authenticationReducer,
  cart: cartReducer,
  http: httpReducer,
  orders: ordersReducer,
  products: productsReducer
}
