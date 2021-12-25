import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PanelManagementRoutingModule } from './panel-management-routing.module';
import { PanelListComponent } from './panel-list/panel-list.component';
import {SharedModule} from '../shared/shared.module';
import {AddGroupModalComponent} from '../components/add-group-modal/add-group-modal.component';
import {AddRoleModalComponent} from '../components/add-role-modal/add-role-modal.component';
import {ConfirmComponent} from '../components/confirm/confirm.component';
import {AddTemplateModalComponent} from '../components/add-template-modal/add-template-modal.component';
import {AddPanelModalComponent} from '../components/add-panel-modal/add-panel-modal.component';
import {AddBusinessCardModelComponent} from '../components/add-business-card-model/add-business-card-model.component';
import {EditUserRolesComponent} from '../components/edit-user-roles/edit-user-roles.component';
import {AddCreditModalComponent} from '../components/add-credit-modal/add-credit-modal.component';
import {AddGroupModalModule} from '../components/add-group-modal/add-group-modal.module';
import {AddRoleModalModule} from '../components/add-role-modal/add-role-modal.module';
import {ConfirmModule} from '../components/confirm/confirm.module';
import {AddTemplateModalModule} from '../components/add-template-modal/add-template-modal.module';
import {AddPanelModalModule} from '../components/add-panel-modal/add-panel-modal.module';
import { PanelVersionComponent } from './panel-version/panel-version.component';
import {FormsModule} from '@angular/forms';
import {NgxLoadingModule} from 'ngx-loading';
import {FileUploadModule} from 'ng2-file-upload';
import {NgxDropzoneModule} from 'ngx-dropzone';
import {AddPanelVersionModalComponent} from '../components/add-panel-version-modal/add-panel-version-modal.component';
import {AddPanelVersionModalModule} from '../components/add-panel-version-modal/add-panel-version-modal.module';
import {MatTooltipModule} from '@angular/material';


@NgModule({
  declarations: [PanelListComponent, PanelVersionComponent],
  imports: [
    CommonModule,
    PanelManagementRoutingModule,
    SharedModule,
    AddGroupModalModule,
    AddRoleModalModule,
    ConfirmModule,
    AddTemplateModalModule,
    AddPanelModalModule,
    FormsModule,
    NgxLoadingModule,
    FileUploadModule,
    NgxDropzoneModule,
    AddPanelVersionModalModule,
    MatTooltipModule
  ],
  entryComponents: [
    AddGroupModalComponent,
    AddRoleModalComponent,
    ConfirmComponent,
    AddTemplateModalComponent,
    AddPanelModalComponent,
    AddPanelVersionModalComponent
  ]
})
export class PanelManagementModule { }
