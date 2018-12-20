import { Component, OnInit, OnDestroy } from '@angular/core'
import { Subscription } from 'rxjs'
import { Store, select } from '@ngrx/store'

import { AppState } from '../../../core/store/app.state'
import { animations } from './pending-orders.animation'
import { BaseComponent } from '../../base.component'
import { getTotalSum, toLocaleString } from '../../../core/utils/helperFunctions'
import { OrderModel } from '../../orders/models/OrderModel'
import { OrdersService } from '../../../core/services/orders/orders.service'
import { UndoOrdersRequestMade } from '../../../core/store/http/http.actions'

@Component({
  selector: 'app-pending-orders',
  templateUrl: './pending-orders.component.html',
  animations: animations
})
export class PendingOrdersComponent extends BaseComponent implements OnInit, OnDestroy {
  protected pageSize: number = 5
  protected currentPage: number = 1
  protected notFoundMessage = 'There are no pending orders at the moment.'
  protected getTotalSum = getTotalSum
  protected toLocaleString = toLocaleString
  protected pendingOrders: OrderModel[]
  private subscription$: Subscription

  constructor(
    private store: Store<AppState>,
    private ordersService: OrdersService ) {
    super()
  }

  ngOnInit() {
    this.store.dispatch(new UndoOrdersRequestMade())
    this.ordersService.getPendingOrders()
    this.subscription$ = this.store
      .pipe(select(state => state))
      .subscribe(state => {
        if (state.http.ordersRequestMade) {
          this.pendingOrders = state.orders.pendingOrders.sort((a: OrderModel, b: OrderModel) => +new Date(b.date) - +new Date(a.date))
        }
      })

    this.subscriptions.push(this.subscription$)
  }

  approve(id: string) {
    const order = this.pendingOrders.find(o => o._id === id)
    this.ordersService.approveOrder(id)
  }

  trackByIds(index: number, order: OrderModel): string {
    return order._id
  }
}
