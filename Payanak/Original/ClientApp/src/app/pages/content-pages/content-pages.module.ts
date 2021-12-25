import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';

import {ContentPagesRoutingModule} from './content-pages-routing.module';

import {ComingSoonPageComponent} from './coming-soon/coming-soon-page.component';
import {ErrorPageComponent} from './error/error-page.component';
import {ForgotPasswordPageComponent} from './forgot-password/forgot-password-page.component';
import {LockScreenPageComponent} from './lock-screen/lock-screen-page.component';
import {LoginPageComponent} from './login/login-page.component';
import {MaintenancePageComponent} from './maintenance/maintenance-page.component';
import {RegisterPageComponent} from './register/register-page.component';
import {MatDatepickerModule, MatInputModule, MatSelectModule} from '@angular/material';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {StoreModule} from '@ngrx/store';
import {authReducer} from '../../shared/reducers/auth/auth.reducers';
import {EffectsModule} from '@ngrx/effects';
import {AuthEffects} from '../../shared/effects/auth/auth.effect';
import {ArchwizardModule} from 'angular-archwizard';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {SharedModule} from '../../shared/shared.module';
import {MapModule} from '../../map/map.module';
import { CompleteRegisterComponent } from './complete-register/complete-register.component';
import {NgxLoadingModule} from 'ngx-loading';


@NgModule({
  imports: [
    CommonModule,
    ContentPagesRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    NgbModule,
    ArchwizardModule,
    PerfectScrollbarModule,
    MatSelectModule,
    MatDatepickerModule,
    SharedModule,
    MapModule,
    NgxLoadingModule
  ],
  declarations: [
    ComingSoonPageComponent,
    ErrorPageComponent,
    ForgotPasswordPageComponent,
    LockScreenPageComponent,
    LoginPageComponent,
    MaintenancePageComponent,
    RegisterPageComponent,
    CompleteRegisterComponent
  ]
})
export class ContentPagesModule {
}
