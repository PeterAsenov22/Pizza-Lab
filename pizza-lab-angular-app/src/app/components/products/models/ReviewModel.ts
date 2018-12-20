export class ReviewModel {
  constructor (
    public _id: string,
    public reviewText: string,
    public creatorUsername: string,
    public lastModified: Date ) { }
}
