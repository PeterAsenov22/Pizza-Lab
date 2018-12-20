import { AdminModule } from './components/admin/admin.module'
import { NgModule } from '@angular/core'
import { OrdersModule } from './components/orders/orders.module'
import { ProductsModule } from './components/products/products.module'
import { RouterModule, Routes } from '@angular/router'

// Components
import { CartComponent } from './components/cart/cart.component'
import { HomeComponent } from './components/home/home.component'
import { MenuComponent } from './components/menu/menu.component'
import { NotFoundComponent } from './components/shared/not-found/not-found.component'

// Guards
import { AdminGuard } from './core/guards/authentication/admin.guard'
import { AuthGuard } from './core/guards/authentication/authentication.guard'

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'home' },
  { path: 'home', component: HomeComponent },
  { path: 'menu', component: MenuComponent },
  { path: 'cart', component: CartComponent, canActivate: [AuthGuard] },
  { path: 'product', canActivate: [AuthGuard], loadChildren: () => ProductsModule },
  { path: 'orders', canActivate: [AuthGuard], loadChildren: () => OrdersModule },
  { path: 'admin', canActivate: [AdminGuard], loadChildren: () => AdminModule },
  { path: '**', component: NotFoundComponent }
]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
