import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

import { OrderDetailsComponent } from './order-details/order-details.component'
import { UserOrdersComponent } from './user-orders/user-orders.component'


const ordersRoutes: Routes = [
  { path: 'my', component: UserOrdersComponent },
  { path: 'details/:id', component: OrderDetailsComponent }
]

@NgModule({
  imports: [RouterModule.forChild(ordersRoutes)],
  exports: [RouterModule]
})
export class OrdersRoutingModule { }
