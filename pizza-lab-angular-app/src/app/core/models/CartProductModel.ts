export class CartProductModel {
  constructor (
    public productId: string,
    public productName: string,
    public quantity: number,
    public price: number) { }
}
