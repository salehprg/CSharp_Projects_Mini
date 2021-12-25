import {RoleModel} from '../../model/role.model';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import {RoleActions, RoleActionTypes} from '../../actions/auth/role.actions';

export interface RolesState extends EntityState<RoleModel> {
  isAllRolesLoaded: boolean;
  queryRowsCount: number;
  queryResult: RoleModel[];
  lastCreatedRoleId: number;
  listLoading: boolean;
  actionsLoading: boolean;
  showInitWaitingMessage: boolean;
}

export const adapter: EntityAdapter<RoleModel> = createEntityAdapter<RoleModel>();

export const initialRolesState: RolesState = adapter.getInitialState({
  isAllRolesLoaded: false,
  queryRowsCount: 0,
  queryResult: [],
  lastCreatedRoleId: undefined,
  listLoading: false,
  actionsLoading: false,
  showInitWaitingMessage: true
});

export function rolesReducer(state = initialRolesState, action: RoleActions): RolesState {
  switch  (action.type) {
    case RoleActionTypes.RolesPageToggleLoading: return {
      ...state, listLoading: action.payload.isLoading, lastCreatedRoleId: undefined
    };
    case RoleActionTypes.RolesActionToggleLoading: return {
      ...state, actionsLoading: action.payload.isLoading
    };
    case RoleActionTypes.RoleOnServerCreated: return {
      ...state
    };
    case RoleActionTypes.RoleCreated: return adapter.addOne(action.payload.role, {
      ...state, lastCreatedRoleId: action.payload.role.id
    });
    case RoleActionTypes.RoleUpdated: return adapter.updateOne(action.payload.partialRole, state);
    case RoleActionTypes.RoleDeleted: return adapter.removeOne(action.payload.id, state);
    case RoleActionTypes.AllRolesLoaded: return adapter.addAll(action.payload.roles, {
      ...state, isAllRolesLoaded: true
    });
    case RoleActionTypes.RolesPageCancelled: return {
      ...state, listLoading: false, queryRowsCount: 0, queryResult: []
    };
    case RoleActionTypes.RolesPageLoaded: return adapter.addMany(action.payload.roles, {
      ...initialRolesState,
      listLoading: false,
      queryRowsCount: action.payload.totalCount,
      queryResult: action.payload.roles,
      lastQuery: action.payload.page,
      showInitWaitingMessage: false
    });
    default: return state;
  }
}

export const {
  selectAll,
  selectEntities,
  selectIds,
  selectTotal
} = adapter.getSelectors();
