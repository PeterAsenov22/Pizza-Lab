import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { NgxSpinnerService } from 'ngx-spinner'
import { Router } from '@angular/router'
import { Store } from '@ngrx/store'
import { ToastrService } from 'ngx-toastr'

import { AddIngredient, GetAllIngredients } from '../../../core/store/ingredients/ingredients.actions'
import { AppState } from '../../store/app.state'
import { environment } from 'src/environments/environment'
import { IngredientModel } from '../../../components/admin/models/IngredientModel'
import { ResponseDataModel } from '../../models/ResponseDataModel'

const ingredientsUrl = environment.apiBaseUrl + 'ingredients'
const ingredientsAdminUrl = environment.apiBaseUrl + 'admin/ingredients'
const tenMinutes = 1000 * 60 * 10

@Injectable()
export class IngredientsService {
  private ingredientsCached: boolean = false
  private cacheTime: number

  constructor (
    private http: HttpClient,
    private router: Router,
    private store: Store<AppState>,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }

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

  createIngredient(model: IngredientModel) {
    this.spinner.show()
    this.http
      .post(ingredientsAdminUrl, model)
      .subscribe((res: ResponseDataModel) => {
        const ingredient: IngredientModel = res.data

        this.store.dispatch(new AddIngredient(ingredient))
        this.spinner.hide()
        this.router.navigate(['/menu'])
        this.toastr.success(res.message)
      })
  }
}
