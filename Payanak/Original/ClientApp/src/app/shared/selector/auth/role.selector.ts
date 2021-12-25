
// NGRX
import { createFeatureSelector, createSelector } from '@ngrx/store';

// State
import * as fromRole from '../../reducers/auth/role.reducers';
import { each } from 'lodash';
import {RolesState} from '../../reducers/auth/role.reducers';
import {RoleModel} from '../../model/role.model';

export const selectRolesState = createFeatureSelector<RolesState>('roles');

export const selectRoleById = (roleId: number) => createSelector(
  selectRolesState,
  rolesState => rolesState.entities[roleId]
);

export const selectAllRoles = createSelector(
  selectRolesState,
  fromRole.selectAll
);

export const selectAllRolesIds = createSelector(
  selectRolesState,
  fromRole.selectIds
);

export const allRolesLoaded = createSelector(
  selectRolesState,
  rolesState => rolesState.isAllRolesLoaded
);


export const selectRolesPageLoading = createSelector(
  selectRolesState,
  rolesState => rolesState.listLoading
);

export const selectRolesActionLoading = createSelector(
  selectRolesState,
  rolesState => rolesState.actionsLoading
);

export const selectLastCreatedRoleId = createSelector(
  selectRolesState,
  rolesState => rolesState.lastCreatedRoleId
);

export const selectRolesShowInitWaitingMessage = createSelector(
  selectRolesState,
  rolesState => rolesState.showInitWaitingMessage
);
