import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NumberManagementRoutingModule } from './number-management-routing.module';
import { AssignNumberComponent } from './assign-number/assign-number.component';
import {NumberService} from '../shared/services/Numbers/number.service';
import {MatPaginatorModule, MatTooltipModule} from '@angular/material';
import {CartableModule} from '../cartable/cartable.module';
import {AddNumberModalComponent} from '../components/add-number-modal/add-number-modal.component';
import {AddNumberModalModule} from '../components/add-number-modal/add-number-modal.module';
import {SharedModule} from '../shared/shared.module';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {NgxLoadingModule} from 'ngx-loading';


@NgModule({
  declarations: [ AssignNumberComponent],
  imports: [
    CommonModule,
    NumberManagementRoutingModule,
    MatPaginatorModule,
    CartableModule,
    AddNumberModalModule,
    MatTooltipModule,
    SharedModule,
    PerfectScrollbarModule,
    NgxLoadingModule
  ],
  providers: [NumberService],
  entryComponents: [AddNumberModalComponent]
})
export class NumberManagementModule { }
