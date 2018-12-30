import { AuthenticationService } from './authentication/authentication.service'
import { CategoriesService } from './categories/categories.service'
import { IngredientsService } from './ingredients/ingredients.service'
import { ProductsService } from './products/products.service'
import { OrdersService } from './orders/orders.service'
import { ReviewsService } from './reviews/reviews.service'

export const allServices = [
  AuthenticationService,
  CategoriesService,
  IngredientsService,
  OrdersService,
  ProductsService,
  ReviewsService
]
