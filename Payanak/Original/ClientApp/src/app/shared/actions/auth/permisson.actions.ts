// NGRX
import { Action } from '@ngrx/store';
import {PermissionModel} from '../../model/permission.model';
// Models

export enum PermissionActionTypes {
  AllPermissionsRequested = '[Init] All Permissions Requested',
  AllPermissionsLoaded = '[Init] All Permissions Loaded'
}

export class AllPermissionsRequested implements Action {
  readonly type = PermissionActionTypes.AllPermissionsRequested;
}

export class AllPermissionsLoaded implements Action {
  readonly type = PermissionActionTypes.AllPermissionsLoaded;
  constructor(public payload: { permissions: PermissionModel[] }) { }
}

export type PermissionActions = AllPermissionsRequested | AllPermissionsLoaded;
