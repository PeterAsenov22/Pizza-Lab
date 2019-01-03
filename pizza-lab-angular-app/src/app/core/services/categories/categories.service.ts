import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { NgxSpinnerService } from 'ngx-spinner'
import { Router } from '@angular/router'
import { Store } from '@ngrx/store'
import { ToastrService } from 'ngx-toastr'

import { AddCategory, GetAllCategories } from '../../../core/store/categories/categories.actions'
import { AppState } from '../../store/app.state'
import { CategoryModel } from '../../../components/admin/models/CategoryModel'
import { ResponseDataModel } from '../../models/ResponseDataModel'

const categoriesUrl = 'https://localhost:44393/api/categories'
const categoriesAdminUrl = 'https://localhost:44393/api/admin/categories'
const tenMinutes = 1000 * 60 * 10

@Injectable()
export class CategoriesService {
  private categoriesCached: boolean = false
  private cacheTime: number

  constructor (
    private http: HttpClient,
    private router: Router,
    private store: Store<AppState>,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }

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

  createCategory(model: CategoryModel) {
    this.spinner.show()
    this.http
      .post(categoriesAdminUrl, model)
      .subscribe((res: ResponseDataModel) => {
        const category: CategoryModel = res.data

        this.store.dispatch(new AddCategory(category))
        this.spinner.hide()
        this.router.navigate(['/menu'])
        this.toastr.success(res.message)
      })
  }
}
