// Angular
import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
// RxJS
import { filter, mergeMap, tap, withLatestFrom } from 'rxjs/operators';
import { defer, Observable, of } from 'rxjs';
// NGRX
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Action, select, Store } from '@ngrx/store';
// Auth actions
import { AppState } from '../../reducers';
import {AuthActionTypes, Login, Logout, Register, UserLoaded, UserRequested} from '../../actions/auth/auth.actions';
import {AuthService} from '../../services/auth/auth.service';
import {isUserLoaded} from '../../selector/auth/auth.selector';
import {ResponseModel} from '../../model/Response/responseModel';
import {error} from 'util';

@Injectable()
export class AuthEffects {
  @Effect({dispatch: false})
  login$ = this.actions$.pipe(
    ofType<Login>(AuthActionTypes.Login),
    tap(action => {
      localStorage.setItem('token', action.payload.authToken);
      this.store.dispatch(new UserRequested());
    }),
  );

  @Effect({dispatch: false})
  logout$ = this.actions$.pipe(
    ofType<Logout>(AuthActionTypes.Logout),
    tap(() => {
      localStorage.removeItem('token');
      window.location.href = window.location.origin + '/pages/login';
    })
  );

  @Effect({dispatch: false})
  loadUser$ = this.actions$
    .pipe(
      ofType<UserRequested>(AuthActionTypes.UserRequested),
      withLatestFrom(
        this.store.pipe(select(isUserLoaded))
      ),
      filter(
        ([action, _isUserLoaded]) => !_isUserLoaded
      ),
      mergeMap(
        ([action, _isUserLoaded]) => this.auth.getUserByToken()
      ),
      tap((_user: ResponseModel) => {
        if (_user) {
          this.store.dispatch(new UserLoaded({ user: _user.Result }));
          // this.router.navigate(['/dashboard/dashboard1']);
        } else {
          this.store.dispatch(new Logout());
        }
      })
    );

  @Effect()
  init$: Observable<Action> = defer(() => {
    const userToken = localStorage.getItem('token');
    let observableResult = of({type: 'NO_ACTION'});
    if (userToken) {
      observableResult = of(new Login({  authToken: userToken }));
    }
    return observableResult;
  });

  private returnUrl: string;

  constructor(private actions$: Actions,
              private router: Router,
              private auth: AuthService,
              private store: Store<AppState>) {

    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
      }
    });
  }
}
