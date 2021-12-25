import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddScheduleDetailModelComponent } from './add-schedule-detail-model.component';
import {NgxLoadingModule} from 'ngx-loading';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {ReactiveFormsModule} from '@angular/forms';
import {MatDatepickerModule, MatFormFieldModule} from '@angular/material';
import {NgSelectModule} from '@ng-select/ng-select';
import {SharedModule} from '../../shared/shared.module';



@NgModule({
  declarations: [AddScheduleDetailModelComponent],
  imports: [
    CommonModule,
    NgxLoadingModule,
    PerfectScrollbarModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    NgSelectModule,
    MatDatepickerModule,
    SharedModule
  ]
})
export class AddScheduleDetailModelModule { }
