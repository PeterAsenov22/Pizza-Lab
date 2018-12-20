export interface AuthenticationState {
  readonly token: string
  readonly username: string
  readonly isAdmin: boolean
  readonly isAuthenticated: boolean
}
