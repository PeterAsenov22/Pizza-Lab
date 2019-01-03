import { IngredientsState } from './ingredients.state'
import { IngredientModel } from '../../../components/admin/models/IngredientModel'
import { ADD_INGREDIENT, GET_ALL } from './ingredients.actions'

const initialState: IngredientsState = {
  all: []
}

function addIngredient(state: IngredientsState, ingredient: IngredientModel) {
  return Object.assign({}, state, {
    all: [...state.all, ingredient]
  })
}

function getAllIngredients(state: IngredientsState, ingredients: IngredientModel[]) {
  return Object.assign({}, state, {
    all: ingredients
  })
}

export function ingredientsReducer (state: IngredientsState = initialState, action) {
  switch (action.type) {
    case ADD_INGREDIENT:
      return addIngredient(state, action.payload)
    case GET_ALL:
      return getAllIngredients(state, action.payload)
    default:
      return state
  }
}
