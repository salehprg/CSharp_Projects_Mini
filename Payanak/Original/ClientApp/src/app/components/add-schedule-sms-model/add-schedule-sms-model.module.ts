import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddScheduleSmsModelComponent } from './add-schedule-sms-model.component';
import {NgxLoadingModule} from 'ngx-loading';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material';
import {NgSelectModule} from '@ng-select/ng-select';



@NgModule({
  declarations: [AddScheduleSmsModelComponent],
  imports: [
    CommonModule,
    NgxLoadingModule,
    PerfectScrollbarModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    NgSelectModule
  ]
})
export class AddScheduleSmsModelModule { }
