import { Component, OnInit } from '@angular/core'
import { FormArray, FormBuilder, Validators } from '@angular/forms'
import { Store, select } from '@ngrx/store'
import { Subscription } from 'rxjs'

import { AppState } from '../../../core/store/app.state'
import { BaseComponent } from '../../base.component'
import { CategoriesService } from '../../../core/services/categories/categories.service'
import { CategoryModel } from '../models/CategoryModel'
import { CreateProductModel } from '../models/CreateProductModel'
import { IngredientModel } from '../models/IngredientModel'
import { IngredientsService } from '../../../core/services/ingredients/ingredients.service'
import { ProductsService } from '../../../core/services/products/products.service'

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.scss']
})
export class CreateProductComponent extends BaseComponent implements OnInit {
  protected availableIngredients: IngredientModel[]
  protected categories: CategoryModel[]
  protected createProductForm
  private categoriesSubscription$: Subscription
  private ingredientsSubscription$: Subscription

  constructor(
    private categoriesService: CategoriesService,
    private fb: FormBuilder,
    private ingredientsService: IngredientsService,
    private productsService: ProductsService,
    private store: Store<AppState> ) {
      super()
    }

  ngOnInit() {
    this.categoriesService.getAllCategories()
    this.ingredientsService.getAllIngredients()

    this.categoriesSubscription$ = this.store
      .pipe(select(state => state.categories.all))
      .subscribe(categories => {
        this.categories = categories
      })

    this.ingredientsSubscription$ = this.store
      .pipe(select(state => state.ingredients.all))
      .subscribe(ingredients => {
        this.availableIngredients = ingredients
      })

    this.subscriptions.push(this.categoriesSubscription$)
    this.subscriptions.push(this.ingredientsSubscription$)

    this.createForm()
  }

  create() {
    if (this.createProductForm.invalid) {
      return
    }

    const product: CreateProductModel = Object.assign({}, this.createProductForm.value)

    this.productsService.createProduct(product)
  }

  createForm() {
    this.createProductForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(20)]],
      category: [null],
      ingredients: this.fb.array([]),
      description: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(220)]],
      image: ['', [Validators.required, Validators.minLength(14), Validators.pattern('^(http|https):\/\/[a-zA-Z0-9]+.*$')]],
      weight: ['', [Validators.required, Validators.min(250), Validators.max(800)]],
      price: ['', [Validators.required, Validators.min(0.1)]]
    })
  }

  addIngredient(ingredient: string) {
    this.ingredients.push(this.fb.control(ingredient))
  }

  removeIngredient(ingredient: string) {
    const ingredientIndex: number = this.ingredients.value.findIndex(i => i === ingredient)
    this.ingredients.removeAt(ingredientIndex)
  }

  get name () {
    return this.createProductForm.get('name')
  }

  get ingredients () {
    return this.createProductForm.get('ingredients') as FormArray
  }

  get description () {
    return this.createProductForm.get('description')
  }

  get image () {
    return this.createProductForm.get('image')
  }

  get weight () {
    return this.createProductForm.get('weight')
  }

  get price () {
    return this.createProductForm.get('price')
  }
}
