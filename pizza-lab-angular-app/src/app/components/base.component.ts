import { OnDestroy } from '@angular/core'
import { Subscription } from 'rxjs'

export abstract class BaseComponent implements OnDestroy {
  protected subscriptions: Subscription[] = []

  public ngOnDestroy() {
    this.subscriptions.forEach(el => el.unsubscribe())
  }
}
