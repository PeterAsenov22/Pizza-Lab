import { AdminRoutingModule } from './admin.routing'
import { CommonModule } from '@angular/common'
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { NgModule } from '@angular/core'
import { NgSelectModule } from '@ng-select/ng-select'
import { NgxSpinnerModule } from 'ngx-spinner'
import { SharedModule } from '../shared/shared.module'

import { adminComponents } from '.'
import { NgxPaginationModule } from '../../../../node_modules/ngx-pagination'

@NgModule({
  declarations: [
    ...adminComponents
  ],
  imports: [
    AdminRoutingModule,
    CommonModule,
    FontAwesomeModule,
    FormsModule,
    NgSelectModule,
    NgxPaginationModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    SharedModule
  ],
  exports: [
  ]
})
export class AdminModule { }
