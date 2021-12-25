import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserManagementRoutingModule } from './user-management-routing.module';
import { AddPanelComponent } from './add-panel/add-panel.component';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {MatButtonModule, MatDatepickerModule, MatInputModule, MatPaginatorModule, MatTooltipModule} from '@angular/material';
import {AddGroupModalModule} from '../components/add-group-modal/add-group-modal.module';
import {ConfirmModule} from '../components/confirm/confirm.module';
import {FullPagesModule} from '../pages/full-pages/full-pages.module';
import {AddTemplateModalModule} from '../components/add-template-modal/add-template-modal.module';
import {AddGroupModalComponent} from '../components/add-group-modal/add-group-modal.component';
import {ConfirmComponent} from '../components/confirm/confirm.component';
import {AddTemplateModalComponent} from '../components/add-template-modal/add-template-modal.component';
import {AddPanelModalComponent} from '../components/add-panel-modal/add-panel-modal.component';
import {AddPanelModalModule} from '../components/add-panel-modal/add-panel-modal.module';
import {SharedModule} from '../shared/shared.module';
import {ArchwizardModule} from 'angular-archwizard';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MapModule} from '../map/map.module';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import { RoleListComponent } from './role-list/role-list.component';
import {AddRoleModalComponent} from '../components/add-role-modal/add-role-modal.component';
import {AddRoleModalModule} from '../components/add-role-modal/add-role-modal.module';
import {TagInputModule} from 'ngx-chips';
import { AddBusinessCardComponent } from './add-business-card/add-business-card.component';
import {AddBusinessCardModelModule} from '../components/add-business-card-model/add-business-card-model.module';
import {AddBusinessCardModelComponent} from '../components/add-business-card-model/add-business-card-model.component';
import { UserListComponent } from './user-list/user-list.component';
import {AddUserComponent} from './add-user/add-user.component';
import {EditUserRolesModule} from '../components/edit-user-roles/edit-user-roles.module';
import {EditUserRolesComponent} from '../components/edit-user-roles/edit-user-roles.component';
import {AddCreditModalComponent} from '../components/add-credit-modal/add-credit-modal.component';
import {AddCreditModalModule} from '../components/add-credit-modal/add-credit-modal.module';
import { TicketsListComponent } from './tickets-list/tickets-list.component';
import {NgxLoadingModule} from 'ngx-loading';


@NgModule({
  declarations: [AddPanelComponent,
    RoleListComponent,
    AddUserComponent,
    AddBusinessCardComponent,
    UserListComponent,
    TicketsListComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    UserManagementRoutingModule,
    Ng2SmartTableModule,
    MatPaginatorModule,
    MatButtonModule,
    AddGroupModalModule,
    MatInputModule,
    ConfirmModule,
    FullPagesModule,
    AddTemplateModalModule,
    MatTooltipModule,
    AddPanelModalModule,
    AddRoleModalModule,
    SharedModule,
    ArchwizardModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MapModule,
    PerfectScrollbarModule,
    TagInputModule,
    AddBusinessCardModelModule,
    EditUserRolesModule,
    AddCreditModalModule,
    NgxLoadingModule
  ],
  entryComponents: [
    AddGroupModalComponent,
    AddRoleModalComponent,
    ConfirmComponent,
    AddTemplateModalComponent,
    AddPanelModalComponent,
    AddBusinessCardModelComponent,
    EditUserRolesComponent,
    AddCreditModalComponent
  ]
})
export class UserManagementModule { }
