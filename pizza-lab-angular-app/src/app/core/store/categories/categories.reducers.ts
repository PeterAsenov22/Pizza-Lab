import { ADD_CATEGORY, GET_ALL } from './categories.actions'
import { CategoriesState} from './categories.state'
import { CategoryModel } from '../../../components/admin/models/CategoryModel'

const initialState: CategoriesState = {
  all: []
}

function addCategory(state: CategoriesState, category: CategoryModel) {
  return Object.assign({}, state, {
    all: [...state.all, category]
  })
}

function getAllCategories(state: CategoriesState, categories: CategoryModel[]) {
  return Object.assign({}, state, {
    all: categories
  })
}

export function categoriesReducer (state: CategoriesState = initialState, action) {
  switch (action.type) {
    case ADD_CATEGORY:
      return addCategory(state, action.payload)
    case GET_ALL:
      return getAllCategories(state, action.payload)
    default:
      return state
  }
}
