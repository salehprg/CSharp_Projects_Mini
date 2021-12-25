// NGRX
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { createFeatureSelector } from '@ngrx/store';
import {PermissionModel} from '../../model/permission.model';
import {PermissionActions, PermissionActionTypes} from '../../actions/auth/permisson.actions';
// Actions
// Models

export interface PermissionsState extends EntityState<PermissionModel> {
  _isAllPermissionsLoaded: boolean;
}

export const adapter: EntityAdapter<PermissionModel> = createEntityAdapter<PermissionModel>();

export const initialPermissionsState: PermissionsState = adapter.getInitialState({
  _isAllPermissionsLoaded: false
});

export function permissionsReducer(state = initialPermissionsState, action: PermissionActions): PermissionsState {
  switch  (action.type) {
    case PermissionActionTypes.AllPermissionsRequested:
      return {...state,
        _isAllPermissionsLoaded: false
      };
    case PermissionActionTypes.AllPermissionsLoaded:
      return adapter.addAll(action.payload.permissions, {
        ...state,
        _isAllPermissionsLoaded: true
      });
    default:
      return state;
  }
}

export const getRoleState = createFeatureSelector<PermissionsState>('permissions');

export const {
  selectAll,
  selectEntities,
  selectIds,
  selectTotal
} = adapter.getSelectors();
