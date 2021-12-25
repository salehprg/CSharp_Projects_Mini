// Angular
import {Injectable} from '@angular/core';
// RxJS
import {of, Observable, defer, forkJoin} from 'rxjs';
import {mergeMap, map, withLatestFrom, filter, tap} from 'rxjs/operators';
// NGRX
import {Effect, Actions, ofType} from '@ngrx/effects';
import {Store, select, Action} from '@ngrx/store';
// CRUD

// State
import {AppState} from '../../reducers';
import {
  AllRolesLoaded,
  AllRolesRequested,
  RoleActionTypes, RoleCreated, RoleDeleted, RoleOnServerCreated,
  RolesActionToggleLoading, RolesPageLoaded, RolesPageRequested,
  RolesPageToggleLoading, RoleUpdated
} from '../../actions/auth/role.actions';
import {allRolesLoaded} from '../../selector/auth/role.selector';
import {ResponseModel} from '../../model/Response/responseModel';
import {QueryParamModel} from '../../model/Response/query-param.model';
import {AuthService} from '../../services/auth/auth.service';
// Selectors
// Actions


@Injectable()
export class RoleEffects {
  showPageLoadingDistpatcher = new RolesPageToggleLoading({isLoading: true});
  hidePageLoadingDistpatcher = new RolesPageToggleLoading({isLoading: false});

  showActionLoadingDistpatcher = new RolesActionToggleLoading({isLoading: true});
  hideActionLoadingDistpatcher = new RolesActionToggleLoading({isLoading: false});

  @Effect()
  loadAllRoles$ = this.actions$
    .pipe(
      ofType<AllRolesRequested>(RoleActionTypes.AllRolesRequested),
      withLatestFrom(this.store.pipe(select(allRolesLoaded))),
      filter(([action, isAllRolesLoaded]) => !isAllRolesLoaded),
      mergeMap(() => this.auth.getAllRoles()),
      map((roles: ResponseModel) => {
        return new AllRolesLoaded({ roles: roles.Result});
      })
    );

  @Effect()
  loadRolesPage$ = this.actions$
    .pipe(
      ofType<RolesPageRequested>(RoleActionTypes.RolesPageRequested),
      mergeMap(({payload}) => {
        this.store.dispatch(this.showPageLoadingDistpatcher);
        const requestToServer = this.auth.getAllRolesWithQuery(payload.page);
        const lastQuery = of(payload.page);
        return forkJoin(requestToServer, lastQuery);
      }),
      map(response => {
        const result: ResponseModel = response[0];
        const lastQuery: QueryParamModel = response[1];
        this.store.dispatch(this.hidePageLoadingDistpatcher);

        return new RolesPageLoaded({
          roles: result.Result,
          totalCount: result.TotalCount,
          page: lastQuery
        });
      }),
    );

  @Effect()
  deleteRole$ = this.actions$
    .pipe(
      ofType<RoleDeleted>(RoleActionTypes.RoleDeleted),
      mergeMap(({payload}) => {
          this.store.dispatch(this.showActionLoadingDistpatcher);
          return this.auth.deleteRole(payload.id);
        }
      ),
      map(() => {
        return this.hideActionLoadingDistpatcher;
      }),
    );

  @Effect()
  updateRole$ = this.actions$
    .pipe(
      ofType<RoleUpdated>(RoleActionTypes.RoleUpdated),
      mergeMap(({payload}) => {
        this.store.dispatch(this.showActionLoadingDistpatcher);
        return this.auth.editRole(payload.role);
      }),
      map(() => {
        return this.hideActionLoadingDistpatcher;
      }),
    );


  @Effect()
  createRole$ = this.actions$
    .pipe(
      ofType<RoleOnServerCreated>(RoleActionTypes.RoleOnServerCreated),
      mergeMap(({payload}) => {
        this.store.dispatch(this.showActionLoadingDistpatcher);
        return this.auth.addRole(payload.role).pipe(
          tap(res => {
            this.store.dispatch(new RoleCreated({role: res.Result}));
          })
        );
      }),
      map(() => {
        return this.hideActionLoadingDistpatcher;
      }),
    );

  @Effect()
  init$: Observable<Action> = defer(() => {
    return of(new AllRolesRequested());
  });

  constructor(private actions$: Actions, private auth: AuthService, private store: Store<AppState>) {
  }
}
