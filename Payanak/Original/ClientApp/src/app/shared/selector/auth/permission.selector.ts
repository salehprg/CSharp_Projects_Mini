// NGRX
import { createFeatureSelector, createSelector } from '@ngrx/store';
// State
import * as fromPermissions from '../../reducers/auth/permission.reducers';
import {PermissionsState} from '../../reducers/auth/permission.reducers';

export const selectPermissionsState = createFeatureSelector<PermissionsState>('permissions');

export const selectPermissionById = (permissionId: number) => createSelector(
  selectPermissionsState,
  ps => ps.entities[permissionId]
);

export const selectAllPermissions = createSelector(
  selectPermissionsState,
  fromPermissions.selectAll
);

export const selectAllPermissionsIds = createSelector(
  selectPermissionsState,
  fromPermissions.selectIds
);

export const allPermissionsLoaded = createSelector(
  selectPermissionsState,
  ps  => ps._isAllPermissionsLoaded
);
