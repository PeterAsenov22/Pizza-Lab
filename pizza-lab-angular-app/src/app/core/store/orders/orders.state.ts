import { OrderModel } from '../../../components/orders/models/OrderModel'

export interface OrdersState {
  readonly userOrders: OrderModel[]
  readonly pendingOrders: OrderModel[]
  readonly approvedOrders: OrderModel[]
}
