import {NgModule} from '@angular/core';
import {StoreModule} from '@ngrx/store';
import {SharedModule} from './shared/shared.module';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {FullLayoutComponent} from './layouts/full/full-layout.component';
import {ContentLayoutComponent} from './layouts/content/content-layout.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {ToastrModule} from 'ngx-toastr';
import {NgbModalConfig, NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {AgmCoreModule} from '@agm/core';
import {AuthService} from './shared/services/auth/auth.service';
import {metaReducers, reducers} from './shared/reducers';
import {EffectsModule} from '@ngrx/effects';
import {environment} from '../environments/environment';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';
import {InterceptService} from './shared/intercepter/intercepter.service';
import {authReducer} from './shared/reducers/auth/auth.reducers';
import {AuthEffects} from './shared/effects/auth/auth.effect';
import {
  DateAdapter,
  MAT_DATE_FORMATS,
  MAT_DATE_LOCALE,
  MAT_SNACK_BAR_DEFAULT_OPTIONS,
  MatDialogModule,
  MatSnackBarModule
} from '@angular/material';
import {MaterialPersianDateAdapter, PERSIAN_DATE_FORMATS} from './shared/adapter/persian-date.adapter';
import {MenuService} from './shared/services/menu.service';
import {PersianDatePipe} from './shared/pipes/persian-date.pipe';
import {PersianNumberPipe} from './shared/pipes/persian-number.pipe';
import {ToServerPathPipe} from './shared/pipes/to-server-path.pipe';

import {SmsService} from './shared/services/sms/sms.service';
import {TemplatesService} from './shared/services/Templates/Templates.service';
import {GroupService} from './shared/services/Groups/group.service';
import {UserService} from './shared/services/user/user.service';
import {PanelsService} from './shared/services/Panel/Panels.service';
import {NumberService} from './shared/services/Numbers/number.service';
import {BussinesCardService} from './shared/services/BussinesCard/BussinesCard.service';
import {TicketService} from './shared/services/ticket/ticket.service';

import {TaskService} from './shared/services/task.service';
import {TaskComponentModule} from './components/task-component/task-component.module';
import {NgxLoadingModule} from 'ngx-loading';
import {rolesReducer} from './shared/reducers/auth/role.reducers';
import {RoleEffects} from './shared/effects/auth/role.effect';
import {permissionsReducer} from './shared/reducers/auth/permission.reducers';
import {PermissionEffects} from './shared/effects/auth/permission.effect';
import {PersianUnitPipe} from './shared/pipes/persian-unit.pipe';
import {SearchService} from './shared/services/search.service';
import {AuthGuard, AuthGuardNeg} from './shared/services/auth-gurd.service';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true,
  wheelPropagation: true,
  useBothWheelAxes: true
};

@NgModule({
  declarations: [
    AppComponent,
    FullLayoutComponent,
    ContentLayoutComponent,
  ],
  imports: [
    BrowserAnimationsModule,
    StoreModule.forRoot({}),
    AppRoutingModule,
    SharedModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    NgbModule,
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCERobClkCv1U4mDijGm1FShKva_nxsGJY'
    }),
    PerfectScrollbarModule,
    MatDialogModule,
    MatSnackBarModule,
    NgxLoadingModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreModule.forRoot(reducers, {metaReducers}),
    StoreModule.forFeature('auth', authReducer),
    StoreModule.forFeature('roles', rolesReducer),
    StoreModule.forFeature('permissions', permissionsReducer),
    EffectsModule.forFeature([AuthEffects, RoleEffects, PermissionEffects]),
    TaskComponentModule,
    !environment.production ? [StoreDevtoolsModule.instrument()] : []
  ],
  providers: [
    {provide: DateAdapter, useClass: MaterialPersianDateAdapter, deps: [MAT_DATE_LOCALE]},
    {provide: MAT_DATE_FORMATS, useValue: PERSIAN_DATE_FORMATS},
    {provide: PERFECT_SCROLLBAR_CONFIG, useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG},
    {provide: MAT_SNACK_BAR_DEFAULT_OPTIONS, useValue: {duration: 2500}},
    AuthService,
    MenuService,
    AuthGuardNeg,
    AuthGuard,
    SearchService,
    InterceptService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptService,
      multi: true
    },
    PersianDatePipe,
    PersianNumberPipe,
    ToServerPathPipe,
    SmsService,
    GroupService,
    TemplatesService,
    UserService,
    PanelsService,
    NumberService,
    BussinesCardService,
    TicketService,
    TaskService,
    NgbModalConfig,
    UserService
  ],
  exports: [
    PersianUnitPipe
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
