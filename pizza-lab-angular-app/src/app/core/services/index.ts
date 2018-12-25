import { AuthenticationService } from './authentication/authentication.service'
import { ProductsService } from './products/products.service'
import { OrdersService } from './orders/orders.service'
import { ReviewsService } from './reviews/reviews.service'

export const allServices = [
  AuthenticationService,
  OrdersService,
  ProductsService,
  ReviewsService
]
