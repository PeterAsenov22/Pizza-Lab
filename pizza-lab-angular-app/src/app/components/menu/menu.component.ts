import { Component, OnInit } from '@angular/core'
import { Subscription } from 'rxjs'
import { Store, select } from '@ngrx/store'

import { AppState } from '../../core/store/app.state'
import { BaseComponent } from '../base.component'
import { ProductModel } from '../products/models/ProductModel'
import { ProductsService } from '../../core/services/products/products.service'

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent extends BaseComponent implements OnInit {
  protected productsToView: ProductModel[]
  private products: ProductModel[]
  private subscription$: Subscription
  private seacrhTerm: string = ''

  constructor(
    private productsService: ProductsService,
    private store: Store<AppState>) {
      super()
  }

  ngOnInit() {
    this.productsService.getAllProducts()
    this.subscription$ = this.store
      .pipe(select(state => state.products.all))
      .subscribe(products => {
        this.products = products
        this.productsToView = this.products.filter(p => p.name.toLowerCase().includes(this.seacrhTerm.toLowerCase()))
      })

    this.subscriptions.push(this.subscription$)
  }

  searchChange(event) {
    this.seacrhTerm = event.target.value
    this.productsToView = this.products.filter(p => p.name.toLowerCase().includes(this.seacrhTerm.toLowerCase()))
  }
}
