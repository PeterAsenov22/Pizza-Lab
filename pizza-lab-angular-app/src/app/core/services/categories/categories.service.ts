import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { NgxSpinnerService } from 'ngx-spinner'
import { Store } from '@ngrx/store'

import { AppState } from '../../store/app.state'
import { CategoryModel } from '../../../components/admin/models/CategoryModel'
import { GetAllCategories } from '../../../core/store/categories/categories.actions'

const categoriesUrl = 'https://localhost:44393/api/categories'
const tenMinutes = 1000 * 60 * 10

@Injectable()
export class CategoriesService {
  private categoriesCached: boolean = false
  private cacheTime: number

  constructor (
    private http: HttpClient,
    private store: Store<AppState>,
    private spinner: NgxSpinnerService) { }

  getAllCategories() {
    if (this.categoriesCached && (new Date().getTime() - this.cacheTime) < tenMinutes) {
      return
    }

    this.categoriesCached = true
    this.cacheTime = new Date().getTime()

    this.spinner.show()
    this.http.get<CategoryModel[]>(categoriesUrl)
      .subscribe(categories => {
        this.store.dispatch(new GetAllCategories(categories))
        this.spinner.hide()
      })
  }
}
