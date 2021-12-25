import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddSmsEventlyModalComponent } from './add-sms-evently-modal.component';
import {NgxLoadingModule} from 'ngx-loading';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material';
import { NgSelectModule } from '@ng-select/ng-select';

@NgModule({
  declarations: [AddSmsEventlyModalComponent],
  imports: [
    CommonModule,
    NgxLoadingModule,
    PerfectScrollbarModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    NgSelectModule
  ]
})
export class AddSmsEventlyModalModule { }
