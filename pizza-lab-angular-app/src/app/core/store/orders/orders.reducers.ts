import { DEAUTHENTICATE } from '../authentication/authentication.actions'
import { GET_USER_ORDERS, SUBMIT_ORDER, GET_PENDING_ORDERS, APPROVE_ORDER, GET_APPROVED_ORDERS } from './orders.actions'
import { OrderModel } from '../../../components/orders/models/OrderModel'
import { OrdersState } from './orders.state'

const initialState: OrdersState = {
  userOrders: [],
  pendingOrders: [],
  approvedOrders: []
}

function getUserOrders(state: OrdersState, orders: OrderModel[]) {
  return Object.assign({}, state, {
    userOrders: orders
  })
}

function getPendingOrders(state: OrdersState, orders: OrderModel[]) {
  return Object.assign({}, state, {
    pendingOrders: orders
  })
}

function getApprovedOrders(state: OrdersState, orders: OrderModel[]) {
  return Object.assign({}, state, {
    approvedOrders: orders
  })
}

function submitOrder(state: OrdersState, order: OrderModel) {
  return Object.assign({}, state, {
    userOrders: [...state.userOrders, order]
  })
}

function removeOrders(state: OrdersState) {
  return Object.assign({}, state, {
    userOrders: [],
    pendingOrders: [],
    approvedOrders: []
  })
}

function approveOrder(state: OrdersState, id) {
  return Object.assign({}, state, {
    pendingOrders: state.pendingOrders.filter(o => o._id !== id)
  })
}

export function ordersReducer(state: OrdersState = initialState, action) {
  switch (action.type) {
    case GET_USER_ORDERS:
      return getUserOrders(state, action.payload)
    case GET_PENDING_ORDERS:
      return getPendingOrders(state, action.payload)
    case GET_APPROVED_ORDERS:
      return getApprovedOrders(state, action.payload)
    case SUBMIT_ORDER:
      return submitOrder(state, action.payload)
    case APPROVE_ORDER:
      return approveOrder(state, action.id)
    case DEAUTHENTICATE:
      return removeOrders(state)
    default:
      return state
  }
}
