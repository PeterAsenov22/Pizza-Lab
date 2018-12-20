import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'
import { NgxPaginationModule } from 'ngx-pagination'
import { OrdersRoutingModule } from './orders.routing'
import { SharedModule } from '../shared/shared.module'

import { ordersComponents } from '.'

@NgModule({
  declarations: [
    ...ordersComponents
  ],
  imports: [
    CommonModule,
    NgxPaginationModule,
    OrdersRoutingModule,
    SharedModule
  ],
  exports: [
    ...ordersComponents
  ]
})
export class OrdersModule { }
