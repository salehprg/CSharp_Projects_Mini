// Angular
import { Injectable } from '@angular/core';
// RxJS
import { mergeMap, map, tap } from 'rxjs/operators';
import { defer, Observable, of } from 'rxjs';
// NGRX
import { Effect, Actions, ofType } from '@ngrx/effects';
import { Action } from '@ngrx/store';
import {AllPermissionsLoaded, AllPermissionsRequested, PermissionActionTypes} from '../../actions/auth/permisson.actions';
import {PermissionModel} from '../../model/permission.model';
import {AuthService} from '../../services/auth/auth.service';
import {ResponseModel} from '../../model/Response/responseModel';
// Services
// Actions


@Injectable()
export class PermissionEffects {
  @Effect()
  loadAllPermissions$ = this.actions$
    .pipe(
      ofType<AllPermissionsRequested>(PermissionActionTypes.AllPermissionsRequested),
      mergeMap(() => this.auth.getAllPermissions()),
      map((result: ResponseModel) => {
        return  new AllPermissionsLoaded({
          permissions: result.Result
        });
      })
    );

  @Effect()
  init$: Observable<Action> = defer(() => {
    return of(new AllPermissionsRequested());
  });

  constructor(private actions$: Actions, private auth: AuthService) { }
}
