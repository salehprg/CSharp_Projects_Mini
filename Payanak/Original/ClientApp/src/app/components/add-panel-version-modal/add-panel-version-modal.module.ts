import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddPanelVersionModalComponent } from './add-panel-version-modal.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule, MatFormFieldModule, MatSelectModule, MatTooltipModule} from '@angular/material';
import {UiSwitchModule} from 'ngx-ui-switch';
import {NgSelectModule} from '@ng-select/ng-select';
import {NgxLoadingModule} from 'ngx-loading';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {NgxDropzoneModule} from 'ngx-dropzone';



@NgModule({
  declarations: [AddPanelVersionModalComponent],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    UiSwitchModule,
    NgSelectModule,
    MatSelectModule,
    NgxLoadingModule,
    MatButtonModule,
    MatTooltipModule,
    PerfectScrollbarModule,
    NgxDropzoneModule,
  ]
})
export class AddPanelVersionModalModule { }
