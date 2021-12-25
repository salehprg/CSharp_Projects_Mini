import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddTemplateModalComponent } from './add-template-modal.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule, MatSelectModule} from '@angular/material';
import {UiSwitchModule} from 'ngx-ui-switch';
import {NgSelectModule} from '@ng-select/ng-select';



@NgModule({
  declarations: [AddTemplateModalComponent],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    UiSwitchModule,
    NgSelectModule,
    MatSelectModule
  ],
  exports: [AddTemplateModalComponent]
})
export class AddTemplateModalModule { }
