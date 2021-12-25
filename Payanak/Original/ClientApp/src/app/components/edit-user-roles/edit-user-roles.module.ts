import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditUserRolesComponent } from './edit-user-roles.component';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule, MatSelectModule} from '@angular/material';
import {UiSwitchModule} from 'ngx-ui-switch';
import {NgSelectModule} from '@ng-select/ng-select';
import {TagInputModule} from 'ngx-chips';



@NgModule({
  declarations: [EditUserRolesComponent],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    UiSwitchModule,
    NgSelectModule,
    MatSelectModule,
    PerfectScrollbarModule,
    TagInputModule
  ],
  exports: [EditUserRolesComponent]
})
export class EditUserRolesModule { }
