import { OrderProductModel } from './OrderProductModel'

export class OrderModel {
  _id: string
  creator: string
  creatorEmail: string
  products: OrderProductModel[]
  date: Date
  status: string
}
