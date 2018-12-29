import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { NgxSpinnerService } from 'ngx-spinner'
import { Router } from '@angular/router'
import { Store } from '@ngrx/store'
import { ToastrService } from 'ngx-toastr'

import { AppState } from '../../store/app.state'
import { CreateProductModel } from '../../../components/admin/models/CreateProductModel'
import { ProductModel } from '../../../components/products/models/ProductModel'

import { GetAllProducts,
  LikeProduct, UnlikeProduct, CreateProduct, DeleteProduct, EditProduct } from '../../store/products/products.actions'
import { GetRequestBegin, GetRequestEnd } from '../../store/http/http.actions'
import { ResponseDataModel } from '../../models/ResponseDataModel'

const baseUrl = 'https://localhost:44393/api'
const allProductsUrl = baseUrl + '/products/all'
const likeProductUrl = baseUrl + '/products/like/'
const unlikeProductUrl = baseUrl + '/products/unlike/'
const adminProductsUrl = baseUrl + '/admin/products'
const fiveMinutes = 1000 * 60 * 5

@Injectable()
export class ProductsService {
  private productsCached: boolean = false
  private cacheTime: number

  constructor (
    private http: HttpClient,
    private store: Store<AppState>,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router ) { }

  getAllProducts () {
    if (this.productsCached && (new Date().getTime() - this.cacheTime) < fiveMinutes) {
      return
    }

    this.productsCached = true
    this.cacheTime = new Date().getTime()

    this.store.dispatch(new GetRequestBegin())

    this.http.get<ProductModel[]>(allProductsUrl)
      .subscribe(products => {
        products.forEach(p => p.reviews = [])

        this.store.dispatch(new GetAllProducts(products))
        this.store.dispatch(new GetRequestEnd())
      })
  }

  createProduct(model: CreateProductModel) {
    this.spinner.show()
    this.http
      .post(adminProductsUrl, model)
      .subscribe((res: ResponseDataModel) => {
        const product: ProductModel = res.data
        product.reviews = []

        this.store.dispatch(new CreateProduct(product))
        this.spinner.hide()
        this.router.navigate(['/menu'])
        this.toastr.success(res.message)
      })
  }

  editProduct(model: ProductModel) {
    this.spinner.show()
    this.http
      .put(`${adminProductsUrl}/${model.id}`, model)
      .subscribe((res: ResponseDataModel) => {
        const product: ProductModel = res.data
        product.reviews = []

        this.store.dispatch(new EditProduct(product))
        this.spinner.hide()
        this.router.navigate(['/menu'])
        this.toastr.success(res.message)
      })
  }

  deleteProduct(id: string, activeModal) {
    this.spinner.show()
    this.http
      .delete(`${adminProductsUrl}/${id}`)
      .subscribe((res: ResponseDataModel) => {
        this.store.dispatch(new DeleteProduct(id))
        this.spinner.hide()
        activeModal.close()
        this.toastr.success(res.message)
      })
  }

  likeProduct(id: string, username: string) {
    this.store.dispatch(new LikeProduct(id, username))
    this.http
      .post(`${likeProductUrl}${id}`, {})
      .subscribe()
  }

  unlikeProduct(id: string, username: string) {
    this.store.dispatch(new UnlikeProduct(id, username))
    this.http
      .post(`${unlikeProductUrl}${id}`, {})
      .subscribe()
  }
}
