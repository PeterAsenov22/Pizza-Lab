import { HttpClient } from '@angular/common/http'
import { Injectable } from '@angular/core'
import { Router } from '@angular/router'
import { Store, select } from '@ngrx/store'

import { AppState } from '../../store/app.state'
import { ClearCart } from '../../store/cart/cart.actions'
import { GetUserOrders, SubmitOrder, GetPendingOrders, ApproveOrder, GetApprovedOrders } from '../../store/orders/orders.actions'
import { OrderModel } from '../../../components/orders/models/OrderModel'
import { OrderProductModel } from '../../../components/orders/models/OrderProductModel'
import { NgxSpinnerService } from 'ngx-spinner'
import { ResponseDataModel } from '../../models/ResponseDataModel'

const baseUrl = 'http://localhost:5000/orders/'
const userOrdersUrl = 'user'
const pendingOrdersUrl = 'pending'
const approvedOrdersUrl = 'approved'
const submitOrderUrl = 'submit'
const approveOrderUrl = 'approve/'

@Injectable()
export class OrdersService {
  private ordersRequestMade: boolean

  constructor (
    private http: HttpClient,
    private store: Store<AppState>,
    private spinner: NgxSpinnerService,
    private router: Router ) {
      this.store
        .pipe(select(state => state.http.ordersRequestMade))
        .subscribe(data => {
          this.ordersRequestMade = data
        })
  }

  getUserOrders() {
    if (this.ordersRequestMade) {
      return
    }

    this.spinner.show()

    this.http.get<OrderModel[]>(`${baseUrl}${userOrdersUrl}`)
      .subscribe(orders => {
        this.store.dispatch(new GetUserOrders(orders))
        this.spinner.hide()
    })
  }

  getPendingOrders() {
    this.spinner.show()

    this.http.get<OrderModel[]>(`${baseUrl}${pendingOrdersUrl}`)
      .subscribe(orders => {
        this.store.dispatch(new GetPendingOrders(orders))
        this.spinner.hide()
    })
  }

  getApprovedOrders() {
    this.spinner.show()

    this.http.get<OrderModel[]>(`${baseUrl}${approvedOrdersUrl}`)
      .subscribe(orders => {
        this.store.dispatch(new GetApprovedOrders(orders))
        this.spinner.hide()
    })
  }

  submitNewOrder(products: OrderProductModel[]) {
    this.spinner.show()
    this.http
      .post(`${baseUrl}${submitOrderUrl}`, products)
      .subscribe((res: ResponseDataModel) => {
        this.store.dispatch(new SubmitOrder(res.data))
        this.store.dispatch(new ClearCart())
        this.spinner.hide()
        this.router.navigate(['/orders/my'])
      })
  }

  approveOrder(id: string) {
    this.store.dispatch(new ApproveOrder(id))
    this.http
      .post(`${baseUrl}${approveOrderUrl}${id}`, {})
      .subscribe()
  }
}
