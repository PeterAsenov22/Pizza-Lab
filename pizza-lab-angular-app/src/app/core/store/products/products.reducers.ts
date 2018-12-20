import { GET_ALL, ADD_REVIEW, LIKE_PRODUCT, UNLIKE_PRODUCT, CREATE_PRODUCT, DELETE_PRODUCT, EDIT_PRODUCT } from './products.actions'
import { ProductModel } from '../../../components/products/models/ProductModel'
import { ProductsState } from './products.state'
import { ReviewModel } from '../../../components/products/models/ReviewModel'

const initialState: ProductsState = {
  all: []
}

function getAllProducts(state: ProductsState, products: ProductModel[]) {
  return Object.assign({}, state, {
    all: products
  })
}

function addProduct(state: ProductsState, product: ProductModel) {
  return Object.assign({}, state, {
    all: [...state.all, product]
  })
}

function editProduct(state: ProductsState, product: ProductModel) {
  return Object.assign({}, state, {
    all: [...state.all.filter(p => p._id !== product._id), product]
  })
}

function removeProduct(state: ProductsState, id: string) {
  return Object.assign({}, state, {
    all: state.all.filter(p => p._id !== id)
  })
}

function addProductReview(state: ProductsState, review: ReviewModel, productId: string) {
  const allProductsCopy = state.all.slice()
  const product = allProductsCopy.find(p => p._id === productId)
  if (product) {
    product.reviews.push(review)
  }

  return Object.assign({}, state, {
    all: allProductsCopy
  })
}

function likeProduct(state: ProductsState, id: string, username: string) {
  const allProductsCopy = state.all.slice()
  const product = allProductsCopy.find(p => p._id === id)
  if (product) {
    product.likes.push(username)
  }

  return Object.assign({}, state, {
    all: allProductsCopy
  })
}

function unlikeProduct(state: ProductsState, id: string, username: string) {
  const allProductsCopy = state.all.slice()
  const product = allProductsCopy.find(p => p._id === id)
  if (product) {
    product.likes = product.likes.filter(u => u !== username)
  }

  return Object.assign({}, state, {
    all: allProductsCopy
  })
}

export function productsReducer (state: ProductsState = initialState, action) {
  switch (action.type) {
    case GET_ALL:
      return getAllProducts(state, action.payload)
    case CREATE_PRODUCT:
      return addProduct(state, action.payload)
    case EDIT_PRODUCT:
      return editProduct(state, action.payload)
    case DELETE_PRODUCT:
      return removeProduct(state, action.id)
    case ADD_REVIEW:
      return addProductReview(state, action.review, action.productId)
    case LIKE_PRODUCT:
      return likeProduct(state, action.id, action.username)
    case UNLIKE_PRODUCT:
      return unlikeProduct(state, action.id, action.username)
    default:
      return state
  }
}
