import { Component, Input } from '@angular/core'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { ProductModel } from '../models/ProductModel'
import { Router } from '@angular/router'
import { Store } from '@ngrx/store'

import { AddToCart } from '../../../core/store/cart/cart.actions'
import { AppState } from '../../../core/store/app.state'
import { AuthenticationService } from '../../../core/services/authentication/authentication.service'
import { CartProductModel } from '../../../core/models/CartProductModel'
import { ProductDeleteModalComponent } from '../product-delete-modal/product-delete-modal.component'

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent {
  @Input() protected product: ProductModel

  constructor (
    protected authService: AuthenticationService,
    private store: Store<AppState>,
    private router: Router,
    private modalService: NgbModal) { }

  addToCart () {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/cart'])
      return
    }

    const productToAdd = new CartProductModel(
      this.product.id,
      this.product.name,
      1,
      this.product.price)

    this.store.dispatch(new AddToCart(productToAdd))
    this.router.navigate(['/cart'])
  }

  openDeleteProductModal() {
    const deleteRef = this.modalService.open(ProductDeleteModalComponent)
    deleteRef.componentInstance.productId = this.product.id
    deleteRef.result.then((result) => {
    }).catch((error) => {
    })
  }
}
