import { ActivatedRoute } from '@angular/router'
import { Component, OnInit } from '@angular/core'
import { Subscription } from 'rxjs'
import { Store, select } from '@ngrx/store'

import { AppState } from '../../../core/store/app.state'
import { BaseComponent } from '../../base.component'
import { ProductModel } from '../models/ProductModel'

@Component({
  selector: 'app-details-page',
  templateUrl: './details-page.component.html'
})
export class DetailsPageComponent extends BaseComponent implements OnInit {
  protected id: string
  protected notFoundMessage = 'PRODUCT NOT FOUND'
  protected product: ProductModel
  private subscription$: Subscription

  constructor (
    private route: ActivatedRoute,
    private store: Store<AppState>) {
      super()
  }

  ngOnInit () {
    this.id = this.route.snapshot.paramMap.get('id')
    this.subscription$ = this.store
      .pipe(select(state => state.products.all))
      .subscribe(data => {
        if (data.length > 0) {
          this.product = data.find(p => p._id === this.id)
        }
      })

    this.subscriptions.push(this.subscription$)
  }
}
