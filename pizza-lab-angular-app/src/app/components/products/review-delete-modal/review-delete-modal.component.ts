import { Component, Input } from '@angular/core'
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap'
import { faWindowClose } from '@fortawesome/free-solid-svg-icons'

import { ReviewsService } from '../../../core/services/reviews/reviews.service'

@Component({
  selector: 'app-review-delete-modal',
  templateUrl: './review-delete-modal.component.html',
  styleUrls: ['./review-delete-modal.component.scss']
})
export class ReviewDeleteModalComponent {
  protected faWindowClose = faWindowClose
  @Input() private reviewId: string
  @Input() private productId: string

  constructor(
    protected activeModal: NgbActiveModal,
    private reviewsService: ReviewsService
  ) { }

  delete() {
    this.reviewsService.deleteReview(this.reviewId, this.productId, this.activeModal)
  }
}
