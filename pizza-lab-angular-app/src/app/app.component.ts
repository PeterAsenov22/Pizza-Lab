import * as signalR from '@aspnet/signalr'
import { Component, OnInit } from '@angular/core'
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr'
import { NgxSpinnerService } from 'ngx-spinner'
import { Store, select } from '@ngrx/store'
import { Subscription } from 'rxjs'
import { ToastrService } from 'ngx-toastr'

import { AppState } from './core/store/app.state'
import { AuthenticationService } from './core/services/authentication/authentication.service'
import { CreateProduct } from './core/store/products/products.actions'
import { environment } from 'src/environments/environment'
import { BaseComponent } from './components/base.component'
import { ProductsService } from './core/services/products/products.service'
import { OrdersService } from './core/services/orders/orders.service'
import { delay } from 'rxjs/operators'
import { ProductModel } from './components/products/models/ProductModel';

const signalREndpoint = environment.apiBaseUrl + 'notify'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent extends BaseComponent implements OnInit {
  protected getCalls: number
  private hubConnection: HubConnection
  private subscription$: Subscription

  constructor (
    private spinner: NgxSpinnerService,
    private productsService: ProductsService,
    private authService: AuthenticationService,
    private ordersService: OrdersService,
    private store: Store<AppState>,
    private toastr: ToastrService) {
      super()
  }

  ngOnInit () {
    this.productsService.getAllProducts()

   if (this.authService.isAuthenticated() && !this.authService.isAdmin()) {
     this.ordersService.getUserOrders()
   }

   if (this.authService.isAdmin()) {
     this.ordersService.getPendingOrders()
     this.ordersService.getApprovedOrders()
   }

   this.subscription$ = this.store
     .pipe(select(state => state.http.currentGetCalls), delay(0))
     .subscribe(calls => {
       if ((!this.getCalls || this.getCalls === 0) && calls > 0) {
         this.spinner.show()
       }

       if (this.getCalls > 0 && calls === 0) {
         this.spinner.hide()
       }

       this.getCalls = calls
     })

   this.subscriptions.push(this.subscription$)

   this.hubConnection = new HubConnectionBuilder()
    .withUrl(signalREndpoint)
    .configureLogging(signalR.LogLevel.Information)
    .build()

   this.hubConnection
    .start()
    .catch(err => console.log(err.toString()))

   this.hubConnection
    .on('BroadcastProduct', (product: ProductModel) => {
        product.reviews = []
        this.store.dispatch(new CreateProduct(product))
        this.toastr.info('A new product has been added!')
    })
  }
}
