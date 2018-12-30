import { AuthenticationState } from './authentication/authentication.state'
import { CartState } from './cart/cart.state'
import { CategoriesState } from './categories/categories.state'
import { HttpState } from './http/http.state'
import { IngredientsState } from './ingredients/ingredients.state'
import { OrdersState } from './orders/orders.state'
import { ProductsState } from './products/products.state'

export interface AppState {
  authentication: AuthenticationState
  cart: CartState
  categories: CategoriesState
  http: HttpState
  ingredients: IngredientsState
  orders: OrdersState
  products: ProductsState
}
