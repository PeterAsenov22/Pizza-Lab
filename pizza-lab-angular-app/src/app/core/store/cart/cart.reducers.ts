import { ADD_TO_CART, SYNC_CART, REMOVE_FROM_CART, CLEAR_CART } from './cart.actions'
import { DEAUTHENTICATE } from '../authentication/authentication.actions'
import { CartProductModel } from '../../models/CartProductModel'
import { CartState } from './cart.state'

const initialState: CartState = {
  products: []
}

function addToCart (state: CartState, product: CartProductModel) {
  if (state.products.find(p => p.productId === product.productId)) {
    const newProducts = state.products.slice()
    const cartProduct = newProducts.find(p => p.productId === product.productId)
    cartProduct.quantity += 1

    return Object.assign({}, state, {
      products: newProducts
    })
  }

  return Object.assign({}, state, {
    products: [...state.products, product]
  })
}

function syncCart (state: CartState, id: string, quantity: number) {
  const newProducts = state.products.slice()
  const cartProduct = newProducts.find(p => p.productId === id)
  cartProduct.quantity = quantity

  return Object.assign({}, state, {
    products: newProducts
  })
}

function removeFromCart (state: CartState, id: string) {
  return Object.assign({}, state, {
    products: state.products.filter(p => p.productId !== id)
  })
}

function clearCart (state) {
  return Object.assign({}, state, {
    products: []
  })
}

export function cartReducer (state: CartState = initialState, action) {
  switch (action.type) {
    case ADD_TO_CART:
      return addToCart(state, action.payload)
    case SYNC_CART:
      return syncCart(state, action.id, action.quantity)
    case REMOVE_FROM_CART:
      return removeFromCart(state, action.id)
    case CLEAR_CART:
    case DEAUTHENTICATE:
      return clearCart(state)
    default:
      return state
  }
}
