export class ReviewModel {
  constructor (
    public id: string,
    public reviewText: string,
    public creatorUsername: string,
    public lastModified: Date ) { }
}
