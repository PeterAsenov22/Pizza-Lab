import { Action } from '@ngrx/store'

import { IngredientModel } from '../../../components/admin/models/IngredientModel'

export const ADD_INGREDIENT = '[INGREDIENTS] ADD_INGREDIENT'
export const GET_ALL = '[INGREDIENTS] GET_ALL'

export class AddIngredient implements Action {
  readonly type: string = ADD_INGREDIENT

  constructor (public payload: IngredientModel) { }
}

export class GetAllIngredients implements Action {
  readonly type: string = GET_ALL

  constructor (public payload: IngredientModel[]) { }
}
