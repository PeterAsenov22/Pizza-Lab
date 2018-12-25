import { AuthenticationService } from '../../../core/services/authentication/authentication.service'
import { Component, Input } from '@angular/core'
import { FormBuilder, Validators } from '@angular/forms'
import { ReviewsService } from '../../../core/services/reviews/reviews.service'
import { ReviewModel } from '../models/ReviewModel'
import { toLocaleString } from '../../../core/utils/helperFunctions'

@Component({
  selector: 'app-product-reviews',
  templateUrl: './product-reviews.component.html',
  styleUrls: ['./product-reviews.component.scss']
})
export class ProductReviewsComponent {
  protected reviewForm
  protected toLocaleString = toLocaleString
  @Input() protected reviews: ReviewModel[]
  @Input() private productId: string

  constructor (
    protected formBuilder: FormBuilder,
    protected authService: AuthenticationService,
    private reviewsService: ReviewsService ) {
    this.createForm()
  }

  get review() { return this.reviewForm.get('review') }

  submitForm() {
    if (this.reviewForm.invalid) {
      return
    }

    const formValue = this.reviewForm.value
    this.reviewsService.addProductReview(formValue, this.productId)
    this.reviewForm.reset()
  }

  private createForm() {
    this.reviewForm = this.formBuilder.group({
      review: ['', [Validators.required, Validators.minLength(4)]]
    })
  }
}
