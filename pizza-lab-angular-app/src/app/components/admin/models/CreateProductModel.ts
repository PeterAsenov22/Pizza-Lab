export class CreateProductModel {
  constructor (
    public name: string,
    public category: string,
    public ingredients: string[],
    public description: string,
    public image: string,
    public weight: number,
    public price: number ) {
  }
}
