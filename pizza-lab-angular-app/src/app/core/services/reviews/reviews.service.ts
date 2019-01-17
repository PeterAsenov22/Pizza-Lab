import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { NgxSpinnerService } from 'ngx-spinner'
import { Store } from '@ngrx/store'
import { ToastrService } from 'ngx-toastr'

import { AddProductReview, GetProductReviews, DeleteProductReview } from '../../store/products/products.actions'
import { AppState } from '../../store/app.state'
import { environment } from 'src/environments/environment'
import { ResponseDataModel } from '../../models/ResponseDataModel'
import { ReviewModel } from '../../../components/products/models/ReviewModel'

const baseUrl = environment.apiBaseUrl + 'reviews'

@Injectable()
export class ReviewsService {
  constructor (
    private http: HttpClient,
    private store: Store<AppState>,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) { }

  addProductReview(model, productId: string) {
    this.spinner.show()
    this.http
      .post(`${baseUrl}/${productId}`, model)
      .subscribe((res: ResponseDataModel) => {
        this.store.dispatch(new AddProductReview(res.data, productId))
        this.spinner.hide()
        this.toastr.success(res.message)
      })
  }

  getProductReviews(productId: string) {
    this.spinner.show()
    this.http.get<ReviewModel[]>(`${baseUrl}/${productId}`)
      .subscribe(reviews => {
        this.store.dispatch(new GetProductReviews(reviews, productId))
        this.spinner.hide()
      })
  }

  deleteReview(reviewId: string, productId: string, activeModal) {
    this.spinner.show()
    this.http
      .delete(`${baseUrl}/${reviewId}`)
      .subscribe((res: ResponseDataModel) => {
        this.store.dispatch(new DeleteProductReview(reviewId, productId))
        this.spinner.hide()
        activeModal.close()
        this.toastr.success(res.message)
      })
  }
}
