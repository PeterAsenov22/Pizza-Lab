import { Action } from '@ngrx/store'
import { OrderModel } from '../../../components/orders/models/OrderModel'

export const GET_USER_ORDERS = '[ORDERS] GET_USER_ORDERS'
export const GET_PENDING_ORDERS = '[ORDERS] GET_PENDING_ORDERS'
export const GET_APPROVED_ORDERS = '[ORDERS] GET_APPROVED_ORDERS'
export const SUBMIT_ORDER = '[ORDERS] SUBMIT_ORDER'
export const APPROVE_ORDER = '[ORDERS] APPROVE_ORDER'

export class GetUserOrders implements Action {
  readonly type: string = GET_USER_ORDERS

  constructor (public payload: OrderModel[]) { }
}

export class GetPendingOrders implements Action {
  readonly type: string = GET_PENDING_ORDERS

  constructor (public payload: OrderModel[]) { }
}

export class GetApprovedOrders implements Action {
  readonly type: string = GET_APPROVED_ORDERS

  constructor (public payload: OrderModel[]) { }
}

export class SubmitOrder implements Action {
  readonly type: string = SUBMIT_ORDER

  constructor (public payload: OrderModel) { }
}

export class ApproveOrder implements Action {
  readonly type: string = APPROVE_ORDER

  constructor (public id: string) { }
}
