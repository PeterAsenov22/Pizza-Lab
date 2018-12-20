import { CommonModule } from '@angular/common'
import { NgModule } from '@angular/core'

import { allServices } from '.'

@NgModule({
  providers: [
    ...allServices
  ],
  imports: [
    CommonModule
  ]
})
export class ServicesModule { }
