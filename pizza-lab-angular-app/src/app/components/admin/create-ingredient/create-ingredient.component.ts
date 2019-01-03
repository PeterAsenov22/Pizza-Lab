import { Component, OnInit } from '@angular/core'
import { FormBuilder, Validators } from '@angular/forms'

import { IngredientsService } from '../../../core/services/ingredients/ingredients.service'
import { IngredientModel } from '../models/IngredientModel'

@Component({
  selector: 'app-create-ingredient',
  templateUrl: './create-ingredient.component.html',
  styleUrls: ['./create-ingredient.component.scss']
})
export class CreateIngredientComponent implements OnInit {
  protected createIngredientForm

  constructor(
    private ingredientsService: IngredientsService,
    private fb: FormBuilder) {
    }

  ngOnInit() {
    this.createForm()
  }

  create() {
    if (this.createIngredientForm.invalid) {
      return
    }

    const ingredient: IngredientModel = Object.assign({}, this.createIngredientForm.value)

    this.ingredientsService.createIngredient(ingredient)
  }

  get name () {
    return this.createIngredientForm.get('name')
  }

  private createForm() {
    this.createIngredientForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]]
    })
  }
}
