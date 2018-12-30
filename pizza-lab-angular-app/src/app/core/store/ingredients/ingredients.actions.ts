import { Action } from '@ngrx/store'

import { IngredientModel } from '../../../components/admin/models/IngredientModel'

export const GET_ALL = '[INGREDIENTS] GET_ALL'

export class GetAllIngredients implements Action {
  readonly type: string = GET_ALL

  constructor (public payload: IngredientModel[]) { }
}
