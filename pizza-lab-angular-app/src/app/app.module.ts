import { AppRoutingModule } from './app.routing'
import { AuthenticationModule } from './components/authentication/authentication.module'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { BrowserModule } from '@angular/platform-browser'
import { GuardsModule } from './core/guards/guards.module'
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { MDBBootstrapModule } from 'angular-bootstrap-md'
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'
import { NgModule } from '@angular/core'
import { NgxSpinnerModule } from 'ngx-spinner'
import { OrdersModule } from './components/orders/orders.module'
import { ProductsModule } from './components/products/products.module'
import { RouterModule } from '@angular/router'
import { ServicesModule } from './core/services/services.module'
import { SharedModule } from './components/shared/shared.module'
import { StoreModule, ActionReducer } from '@ngrx/store'
import { ToastrModule } from 'ngx-toastr'

import { AppComponent } from './app.component'
import { CartComponent } from './components/cart/cart.component'
import { HomeComponent } from './components/home/home.component'
import { MenuComponent } from './components/menu/menu.component'

import { appReducers } from './core/store/app.reducers'
import { JWTInterceptor, ErrorInterceptor } from './core/interceptors'

import { AppState } from './core/store/app.state'
import { storeLogger } from 'ngrx-store-logger'

import { environment } from '../environments/environment'

export function logger(reducer: ActionReducer<AppState>): any {
  return storeLogger()(reducer)
}

export const metaReducers = environment.production ? [] : [logger]

@NgModule({
  declarations: [
    AppComponent,
    CartComponent,
    HomeComponent,
    MenuComponent
  ],
  imports: [
    AppRoutingModule,
    AuthenticationModule,
    BrowserAnimationsModule,
    BrowserModule,
    GuardsModule,
    HttpClientModule,
    MDBBootstrapModule.forRoot(),
    NgbModule.forRoot(),
    NgxSpinnerModule,
    OrdersModule,
    ProductsModule,
    RouterModule,
    ServicesModule,
    SharedModule,
    StoreModule.forRoot(appReducers, {metaReducers}),
    ToastrModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JWTInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
