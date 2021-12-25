import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddRoleModalComponent } from './add-role-modal.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule, MatSelectModule} from '@angular/material';
import {UiSwitchModule} from 'ngx-ui-switch';
import {NgSelectModule} from '@ng-select/ng-select';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';



@NgModule({
  declarations: [AddRoleModalComponent],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    UiSwitchModule,
    NgSelectModule,
    MatSelectModule,
    PerfectScrollbarModule
  ],
  exports: [AddRoleModalComponent]
})
export class AddRoleModalModule { }
