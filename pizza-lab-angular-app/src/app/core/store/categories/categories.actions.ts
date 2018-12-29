import { Action } from '@ngrx/store'

import { CategoryModel } from '../../../components/admin/models/CategoryModel'

export const GET_ALL = '[CATEGORIES] GET_ALL'

export class GetAllCategories implements Action {
  readonly type: string = GET_ALL

  constructor (public payload: CategoryModel[]) { }
}

