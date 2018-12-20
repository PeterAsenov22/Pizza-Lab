import { FormGroup, FormControl } from '@angular/forms'

export default class CustomValidators {
  static passwordsDoMatch(registrationFormGroup: FormGroup) {
    const password = registrationFormGroup.controls.password.value
    const repeatPassword = registrationFormGroup.controls.confirmPassword.value

    if (repeatPassword.length <= 0) {
      return null
    }

    if (repeatPassword !== password) {
      return {
        mismatch: true
      }
    }

    return null
  }

  static noSpaceAfterComma(formControl: FormControl) {
    const value = formControl.value

    if (value.indexOf(', ') > -1) {
      return {
        spaceAfterComma: true
      }
    }

    return null
  }

  static noCommaAtTheEnd(formControl: FormControl) {
    const value = formControl.value

    if (value.endsWith(',')) {
      return {
        commaAtTheEnd: true
      }
    }

    return null
  }
}
