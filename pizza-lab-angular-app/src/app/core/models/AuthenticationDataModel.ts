export default class AuthenticationDataModel {
  constructor (
    public token: string,
    public username: string,
    public isAdmin: boolean,
    public isAuthenticated: boolean
  ) { }
}
