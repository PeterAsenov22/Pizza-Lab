import { Component } from '@angular/core'
import CustomValidators from '../../../core/utils/customValidators'
import { faWindowClose } from '@fortawesome/free-solid-svg-icons'
import { FormBuilder, Validators } from '@angular/forms'
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'
import { NgxSpinnerService } from 'ngx-spinner'
import { Subscription } from 'rxjs'

import { AuthenticationService } from '../../../core/services/authentication/authentication.service'
import { BaseComponent } from '../../base.component'
import { RegisterModel } from '../models/RegisterModel'

@Component({
  selector: 'app-register-modal',
  templateUrl: './register-modal.component.html'
})
export class RegisterModalComponent extends BaseComponent {
  protected registerForm
  protected faWindowClose = faWindowClose
  private subscription$: Subscription

  constructor(
    protected activeModal: NgbActiveModal,
    protected formBuilder: FormBuilder,
    private authService: AuthenticationService,
    private spinner: NgxSpinnerService
  ) {
    super()
    this.createForm()
  }

  get email() { return this.registerForm.get('email') }
  get username() { return this.registerForm.get('username') }
  get password() { return this.registerForm.get('password') }
  get confirmPassword() { return this.registerForm.get('confirmPassword') }

  public submitForm() {
    if (this.registerForm.invalid) {
      return
    }

    this.spinner.show()
    const formValue = this.registerForm.value
    const registerModel = new RegisterModel(formValue.email, formValue.username, formValue.password)
    this.subscription$ = this.authService.register(registerModel)
      .subscribe(() => {
        this.spinner.hide()
        this.activeModal.close()
      })
    this.subscriptions.push(this.subscription$)
  }

  private createForm() {
    this.registerForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      username: ['', [Validators.required, Validators.minLength(4), Validators.pattern('^[^@]*$')]],
      password: ['', [Validators.required, Validators.minLength(8)]],
      confirmPassword: ['', [Validators.required]]
    }, { validator: CustomValidators.passwordsDoMatch.bind(this)})
  }
}
