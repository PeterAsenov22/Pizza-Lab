import { Component, Input } from '@angular/core'
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'
import { faWindowClose } from '@fortawesome/free-solid-svg-icons'

import { ProductsService } from '../../../core/services/products/products.service'

@Component({
  selector: 'app-product-delete-modal',
  templateUrl: './product-delete-modal.component.html',
  styleUrls: ['./product-delete-modal.component.scss']
})
export class ProductDeleteModalComponent {
  protected faWindowClose = faWindowClose
  @Input() private productId: string

  constructor(
    protected activeModal: NgbActiveModal,
    private productsService: ProductsService
  ) { }

  delete() {
    this.productsService.deleteProduct(this.productId, this.activeModal)
  }
}
