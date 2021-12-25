import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddNumberModalComponent } from './add-number-modal.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule, MatSelectModule} from '@angular/material';
import {UiSwitchModule} from 'ngx-ui-switch';
import {NgSelectModule} from '@ng-select/ng-select';



@NgModule({
  declarations: [AddNumberModalComponent],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    UiSwitchModule,
    NgSelectModule,
    MatSelectModule
  ],
  exports: [AddNumberModalComponent]
})
export class AddNumberModalModule { }
