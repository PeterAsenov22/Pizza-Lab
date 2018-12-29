import { ReviewModel } from './ReviewModel'

export class ProductModel {
  public id: string
  public name: string
  public category: string
  public description: string
  public image: string
  public price: number
  public weight: number
  public ingredients: Array<string>
  public likes: Array<string>
  public reviews: ReviewModel[]
}
