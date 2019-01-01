import { OrderProductModel } from './OrderProductModel'

export class OrderModel {
  id: string
  creatorId: string
  creatorEmail: string
  orderProducts: OrderProductModel[]
  dateCreated: Date
  status: string
}
