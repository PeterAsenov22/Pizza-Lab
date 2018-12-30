import { authenticationReducer } from './authentication/authentication.reducers'
import { cartReducer } from './cart/cart.reducers'
import { categoriesReducer } from './categories/categories.reducers'
import { httpReducer } from './http/http.reducers'
import { ingredientsReducer } from './ingredients/ingredients.reducers'
import { ordersReducer } from './orders/orders.reducers'
import { productsReducer } from './products/products.reducers'

export const appReducers = {
  authentication: authenticationReducer,
  cart: cartReducer,
  categories: categoriesReducer,
  http: httpReducer,
  ingredients: ingredientsReducer,
  orders: ordersReducer,
  products: productsReducer
}
