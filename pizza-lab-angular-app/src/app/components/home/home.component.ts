import { Component, OnInit } from '@angular/core'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { Subscription } from 'rxjs'
import { Store, select } from '@ngrx/store'

import { AppState } from '../../core/store/app.state'
import { AuthenticationService } from '../../core/services/authentication/authentication.service'
import { BaseComponent } from '../base.component'
import { ProductModel } from '../products/models/ProductModel'
import { ProductsService } from '../../core/services/products/products.service'
import { RegisterModalComponent } from '../authentication/register-modal/register-modal.component'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent implements OnInit {
  protected products: ProductModel[]
  private subscription$: Subscription

  constructor(
    protected authService: AuthenticationService,
    private productsService: ProductsService,
    private store: Store<AppState>,
    private modalService: NgbModal) {
      super()
  }

  ngOnInit() {
    this.productsService.getAllProducts()
    this.subscription$ = this.store
      .pipe(select(state => state.products.all))
      .subscribe(products => {
        this.products = products
          .sort((a, b) => b.likes.length - a.likes.length)
          .slice(0, 3)
      })

    this.subscriptions.push(this.subscription$)
  }

  openRegisterModal() {
    const registerRef = this.modalService.open(RegisterModalComponent)
    registerRef.result.then((result) => {
    }).catch((error) => {
    })
  }
}
