import { Component, Input } from '@angular/core'
import { ProductModel } from '../models/ProductModel'
import { animations } from './product-list.animations'

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.scss'],
  animations: animations
})
export class ProductListComponent {
  protected pageSize: number = 6
  protected currentPage: number = 1

  @Input() protected products: ProductModel[]

  changePage (page) {
    this.currentPage = page
  }

  trackByIds(index: number, product: ProductModel): string {
    return product.id
  }
}

