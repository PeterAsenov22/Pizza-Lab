import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

import { ApprovedOrdersComponent } from './approved-orders/approved-orders.component'
import { CreateCategoryComponent } from './create-category/create-category.component'
import { CreateIngredientComponent } from './create-ingredient/create-ingredient.component'
import { CreateProductComponent } from './create-product/create-product.component'
import { EditProductComponent } from './edit-product/edit-product.component'
import { PendingOrdersComponent } from './pending-orders/pending-orders.component'

const adminRoutes: Routes = [
  { path: 'product/create', component: CreateProductComponent },
  { path: 'product/edit/:id', component: EditProductComponent },
  { path: 'orders/pending', component: PendingOrdersComponent },
  { path: 'orders/approved', component: ApprovedOrdersComponent },
  { path: 'categories/add', component: CreateCategoryComponent },
  { path: 'ingredients/add', component: CreateIngredientComponent }
]

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
