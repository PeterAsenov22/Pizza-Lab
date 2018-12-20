import { NgModule } from '@angular/core'
import { CommonModule } from '@angular/common'

import { AdminGuard } from './authentication/admin.guard'
import { AuthGuard } from './authentication/authentication.guard'

@NgModule({
  providers: [ AuthGuard, AdminGuard ],
  imports: [
    CommonModule
  ]
})
export class GuardsModule { }
