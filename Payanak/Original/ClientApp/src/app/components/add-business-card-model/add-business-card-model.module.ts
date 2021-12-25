import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddBusinessCardModelComponent } from './add-business-card-model.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule, MatFormFieldModule, MatSelectModule, MatTooltipModule} from '@angular/material';
import {UiSwitchModule} from 'ngx-ui-switch';
import {NgSelectModule} from '@ng-select/ng-select';
import {NgxLoadingModule} from 'ngx-loading';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';



@NgModule({
  declarations: [AddBusinessCardModelComponent],
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
  ],
  exports: [AddBusinessCardModelComponent]
})
export class AddBusinessCardModelModule { }
