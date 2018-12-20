import { CommonModule } from '@angular/common'
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { NgModule } from '@angular/core'
import { RouterModule } from '@angular/router'

import { sharedComponents } from '.'

@NgModule({
  declarations: [
    ...sharedComponents
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    RouterModule
  ],
  exports: [
    ...sharedComponents
  ]
})
export class SharedModule { }
