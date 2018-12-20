import { CartProductModel } from '../../models/CartProductModel'

export interface CartState {
  readonly products: CartProductModel[]
}
