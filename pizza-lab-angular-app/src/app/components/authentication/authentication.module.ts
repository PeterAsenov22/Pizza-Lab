import { CommonModule } from '@angular/common'
import { FacebookLoginProvider } from 'angularx-social-login'
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { NgModule } from '@angular/core'
import { NgxSpinnerModule } from 'ngx-spinner'
import { SocialLoginModule, AuthServiceConfig } from 'angularx-social-login'

import { authenticationComponents } from '.'

const config = new AuthServiceConfig([
  {
    id: FacebookLoginProvider.PROVIDER_ID,
    provider: new FacebookLoginProvider('212702559538083')
  }
])

export function provideConfig() {
  return config
}

@NgModule({
  declarations: [
    ...authenticationComponents
  ],
  imports: [
    CommonModule,
    FontAwesomeModule,
    FormsModule,
    NgxSpinnerModule,
    ReactiveFormsModule,
    SocialLoginModule
  ],
  exports: [
    ...authenticationComponents
  ],
  entryComponents: [
    ...authenticationComponents
  ],
  providers: [
    {
      provide: AuthServiceConfig,
      useFactory: provideConfig
    }
  ]
})
export class AuthenticationModule { }
