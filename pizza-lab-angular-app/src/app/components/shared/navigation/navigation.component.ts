import { Component } from '@angular/core'
import { faSignInAlt, faSignOutAlt } from '@fortawesome/free-solid-svg-icons'
import { faUserPlus } from '@fortawesome/free-solid-svg-icons'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'

import { LoginModalComponent } from '../../authentication/login-modal/login-modal.component'
import { RegisterModalComponent } from '../../authentication/register-modal/register-modal.component'

import { AuthenticationService } from '../../../core/services/authentication/authentication.service'

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss']
})
export class NavigationComponent {
  protected faLogin = faSignInAlt
  protected faLogout = faSignOutAlt
  protected faRegister = faUserPlus

  constructor (
    protected authService: AuthenticationService,
    private modalService: NgbModal ) {
  }

  openRegisterModal() {
    const registerRef = this.modalService.open(RegisterModalComponent)
    registerRef.result.then((result) => {
    }).catch((error) => {
    })
  }

  openLoginModal() {
    const loginRef = this.modalService.open(LoginModalComponent)
    loginRef.result.then((result) => {
    }).catch((error) => {
    })
  }

  logout () {
    this.authService.logout()
  }
}
