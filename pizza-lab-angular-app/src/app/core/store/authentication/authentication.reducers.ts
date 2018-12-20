import { AuthenticationState } from './authentication.state'
import { AUTHENTICATE, DEAUTHENTICATE } from './authentication.actions'

const initialState: AuthenticationState = {
  token: '',
  username: '',
  isAdmin: false,
  isAuthenticated: false
}

export function authenticationReducer (state: AuthenticationState = initialState, action) {
  switch (action.type) {
    case AUTHENTICATE:
      return action.payload
    case DEAUTHENTICATE:
      return initialState
    default:
      return state
  }
}
