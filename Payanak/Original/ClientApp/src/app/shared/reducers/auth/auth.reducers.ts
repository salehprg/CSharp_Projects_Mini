// Actions
import {AuthActions, AuthActionTypes} from '../../actions/auth/auth.actions';
// Models


export interface AuthState {
  loggedIn: boolean;
  authToken: string;
  user: any;
  isUserLoaded: boolean;
}

export const initialAuthState: AuthState = {
  loggedIn: false,
  authToken: undefined,
  user: undefined,
  isUserLoaded: false
};

export function authReducer(state = initialAuthState, action: AuthActions): AuthState {
  switch (action.type) {
    case AuthActionTypes.Login: {
      const _token: string = action.payload.authToken;
      return {
        loggedIn: true,
        authToken: _token,
        user: undefined,
        isUserLoaded: false
      };
    }
    case AuthActionTypes.UserRequested: {
      return {
        ...state,
        isUserLoaded: false
      };
    }
    case AuthActionTypes.Register: {
      const _token: string = action.payload.authToken;
      return {
        loggedIn: true,
        authToken: _token,
        user: undefined,
        isUserLoaded: false
      };
    }

    case AuthActionTypes.Logout:
      return initialAuthState;

    case AuthActionTypes.UserLoaded: {
      const _user: any = action.payload.user;
      return {
        ...state,
        user: _user,
        isUserLoaded: true
      };
    }

    default:
      return state;
  }
}
