import { CategoriesState} from './categories.state'
import { CategoryModel } from '../../../components/admin/models/CategoryModel'
import { GET_ALL } from './categories.actions'

const initialState: CategoriesState = {
  all: []
}

function getAllCategories(state: CategoriesState, categories: CategoryModel[]) {
  return Object.assign({}, state, {
    all: categories
  })
}

export function categoriesReducer (state: CategoriesState = initialState, action) {
  switch (action.type) {
    case GET_ALL:
      return getAllCategories(state, action.payload)
    default:
      return state
  }
}
