import { OrderProductModel } from './OrderProductModel'

export class OrderModel {
  id: string
  creator: string
  creatorEmail: string
  products: OrderProductModel[]
  date: Date
  status: string
}
