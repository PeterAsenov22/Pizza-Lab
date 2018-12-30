import { IngredientsState } from './ingredients.state'
import { IngredientModel } from '../../../components/admin/models/IngredientModel'
import { GET_ALL } from './ingredients.actions'

const initialState: IngredientsState = {
  all: []
}

function getAllIngredients(state: IngredientsState, ingredients: IngredientModel[]) {
  return Object.assign({}, state, {
    all: ingredients
  })
}

export function ingredientsReducer (state: IngredientsState = initialState, action) {
  switch (action.type) {
    case GET_ALL:
      return getAllIngredients(state, action.payload)
    default:
      return state
  }
}
