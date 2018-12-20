import {
   CanActivate,
   ActivatedRouteSnapshot,
   RouterStateSnapshot,
   Router
} from '@angular/router'
import { Injectable } from '@angular/core'
import { NgbModal } from '@ng-bootstrap/ng-bootstrap'
import { Observable } from 'rxjs'

import { AuthenticationService } from '../../services/authentication/authentication.service'
import { LoginModalComponent } from '../../../components/authentication/login-modal/login-modal.component'

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private modalService: NgbModal ) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot ): Observable<boolean> | Promise<boolean> | boolean {

    if (this.authService.isAuthenticated()) {
      return true
    }

    this.router.navigate(['/'])
    this.openLoginModal()
    return false
  }

  openLoginModal() {
    const loginRef = this.modalService.open(LoginModalComponent)
    loginRef.result.then((result) => {
    }).catch((error) => {
    })
  }
}
