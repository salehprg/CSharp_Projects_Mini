import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {DashboardRoutingModule} from './dashboard-routing.module';
import {ChartistModule} from 'ng-chartist';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {MatchHeightModule} from '../shared/directives/match-height.directive';

import {Dashboard1Component} from './dashboard1/dashboard1.component';
import {Dashboard2Component} from './dashboard2/dashboard2.component';
import {StoreModule} from '@ngrx/store';
import {authReducer} from '../shared/reducers/auth/auth.reducers';
import {EffectsModule} from '@ngrx/effects';
import {AuthEffects} from '../shared/effects/auth/auth.effect';
import {SharedModule} from '../shared/shared.module';


@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    ChartistModule,
    NgbModule,
    MatchHeightModule,
    SharedModule
  ],
  exports: [],
  declarations: [
    Dashboard1Component,
    Dashboard2Component
  ],
  providers: [],
})
export class DashboardModule {
}
