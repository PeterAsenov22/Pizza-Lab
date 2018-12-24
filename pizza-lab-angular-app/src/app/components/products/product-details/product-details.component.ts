import { Component, Input } from '@angular/core'
import { Store } from '@ngrx/store'

import { AddToCart } from '../../../core/store/cart/cart.actions'
import { AppState } from '../../../core/store/app.state'
import { AuthenticationService } from '../../../core/services/authentication/authentication.service'
import { CartProductModel } from '../../../core/models/CartProductModel'
import { ProductModel } from '../models/ProductModel'
import { ProductsService } from '../../../core/services/products/products.service'
import { Router } from '@angular/router'

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent {
  @Input() protected product: ProductModel

  constructor (
    protected authService: AuthenticationService,
    private productsService: ProductsService,
    private store: Store<AppState>,
    private router: Router ) { }

  addToCart() {
    const productToAdd = new CartProductModel(
      this.product.id,
      this.product.name,
      1,
      this.product.price)

    this.store.dispatch(new AddToCart(productToAdd))
    this.router.navigate(['/cart'])
  }

  onLikeButtonClick() {
    this.productsService.likeProduct(this.product.id, this.authService.getUsername())
  }

  onUnlikeButtonClick() {
    this.productsService.unlikeProduct(this.product.id, this.authService.getUsername())
  }
}
