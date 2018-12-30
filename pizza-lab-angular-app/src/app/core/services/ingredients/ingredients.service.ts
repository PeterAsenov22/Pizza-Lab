import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { NgxSpinnerService } from 'ngx-spinner'
import { Store } from '@ngrx/store'

import { AppState } from '../../store/app.state'
import { IngredientModel } from '../../../components/admin/models/IngredientModel'
import { GetAllIngredients } from '../../../core/store/ingredients/ingredients.actions'

const ingredientsUrl = 'https://localhost:44393/api/ingredients'
const tenMinutes = 1000 * 60 * 10

@Injectable()
export class IngredientsService {
  private ingredientsCached: boolean = false
  private cacheTime: number

  constructor (
    private http: HttpClient,
    private store: Store<AppState>,
    private spinner: NgxSpinnerService) { }

  getAllIngredients() {
    if (this.ingredientsCached && (new Date().getTime() - this.cacheTime) < tenMinutes) {
      return
    }

    this.ingredientsCached = true
    this.cacheTime = new Date().getTime()

    this.spinner.show()
    this.http.get<IngredientModel[]>(ingredientsUrl)
      .subscribe(ingredients => {
        this.store.dispatch(new GetAllIngredients(ingredients))
        this.spinner.hide()
      })
  }
}
