import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatButtonModule, MatFormFieldModule, MatSelectModule, MatTooltipModule} from '@angular/material';
import {UiSwitchModule} from 'ngx-ui-switch';
import {NgSelectModule} from '@ng-select/ng-select';
import {AddPanelModalComponent} from './add-panel-modal.component';
import {NgxLoadingModule} from 'ngx-loading';
import {GroupService} from '../../shared/services/Groups/group.service';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';



@NgModule({
  declarations: [ AddPanelModalComponent],
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
  exports: [AddPanelModalComponent],
  providers: [GroupService]
})
export class AddPanelModalModule { }
