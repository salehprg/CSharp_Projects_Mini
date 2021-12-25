import {ActionReducerMap, MetaReducer} from '@ngrx/store';
import {environment} from '../../../environments/environment';
import {routerReducer} from '@ngrx/router-store';
import {storeFreeze} from 'ngrx-store-freeze';

export interface AppState { }

export const reducers: ActionReducerMap<AppState> = { router: routerReducer };

export const metaReducers: MetaReducer<AppState>[] = !environment.production ? [storeFreeze] : [];
