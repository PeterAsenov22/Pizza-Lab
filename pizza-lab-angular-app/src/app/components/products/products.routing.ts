import { NgModule } from '@angular/core'
import { RouterModule, Routes } from '@angular/router'

import { DetailsPageComponent } from './details-page/details-page.component'

const productsRoutes: Routes = [
  { path: 'details/:id', component: DetailsPageComponent},
]

@NgModule({
  imports: [RouterModule.forChild(productsRoutes)],
  exports: [RouterModule]
})
export class ProductsRoutingModule { }
