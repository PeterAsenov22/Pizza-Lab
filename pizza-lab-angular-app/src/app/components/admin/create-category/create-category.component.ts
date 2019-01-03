import { Component, OnInit } from '@angular/core'
import { FormBuilder, Validators } from '@angular/forms'

import { CategoriesService } from '../../../core/services/categories/categories.service'
import { CategoryModel } from '../models/CategoryModel'

@Component({
  selector: 'app-create-category',
  templateUrl: './create-category.component.html',
  styleUrls: ['./create-category.component.scss']
})
export class CreateCategoryComponent implements OnInit {
  protected createCategoryForm

  constructor(
    private categoriesService: CategoriesService,
    private fb: FormBuilder) {
    }

  ngOnInit() {
    this.createForm()
  }

  create() {
    if (this.createCategoryForm.invalid) {
      return
    }

    const category: CategoryModel = Object.assign({}, this.createCategoryForm.value)

    this.categoriesService.createCategory(category)
  }

  get name () {
    return this.createCategoryForm.get('name')
  }

  private createForm() {
    this.createCategoryForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]]
    })
  }
}
