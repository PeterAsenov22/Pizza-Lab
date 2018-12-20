import { DEAUTHENTICATE } from '../authentication/authentication.actions'
import { GET_REQUEST_BEGIN, GET_REQUEST_END, UNDO_ORDERS_REQUEST } from './http.actions'
import { GET_USER_ORDERS, GET_PENDING_ORDERS, GET_APPROVED_ORDERS } from '../orders/orders.actions'
import { HttpState } from './http.state'

const initialState = {
  currentGetCalls: 0,
  ordersRequestMade: false
}

export function httpReducer(state: HttpState = initialState, action) {
  switch (action.type) {
    case GET_REQUEST_BEGIN:
      return Object.assign({}, state, {
        currentGetCalls: state.currentGetCalls + 1
      })
    case GET_REQUEST_END:
      return Object.assign({}, state, {
        currentGetCalls: state.currentGetCalls - 1
      })
    case GET_USER_ORDERS:
    case GET_PENDING_ORDERS:
    case GET_APPROVED_ORDERS:
      return Object.assign({}, state, {
        ordersRequestMade: true
      })
    case DEAUTHENTICATE:
    case UNDO_ORDERS_REQUEST:
      return Object.assign({}, state, {
        ordersRequestMade: false
      })
    default:
      return state
  }
}
