import { Action } from '@ngrx/store'

import { CategoryModel } from '../../../components/admin/models/CategoryModel'

export const GET_ALL = '[CATEGORIES] GET_ALL'
export const ADD_CATEGORY = '[CATEGORIES] ADD_CATEGORY'

export class GetAllCategories implements Action {
  readonly type: string = GET_ALL

  constructor (public payload: CategoryModel[]) { }
}

export class AddCategory implements Action {
  readonly type: string = ADD_CATEGORY

  constructor (public payload: CategoryModel) { }
}

