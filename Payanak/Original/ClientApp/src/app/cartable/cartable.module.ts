import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';

import {CartableRoutingModule} from './cartable-routing.module';
import {Ng2SmartTableModule} from 'ng2-smart-table';
import {GroupService} from '../shared/services/Groups/group.service';
import {MatButtonModule, MatInputModule, MatPaginatorModule, MatTooltipModule} from '@angular/material';
import {MyGroupsComponent} from './my-groups/my-groups.component';
import {AddGroupModalComponent} from '../components/add-group-modal/add-group-modal.component';
import {AddGroupModalModule} from '../components/add-group-modal/add-group-modal.module';
import {ConfirmComponent} from '../components/confirm/confirm.component';
import {ConfirmModule} from '../components/confirm/confirm.module';
import {MyGroupDetailComponent} from './my-groups/my-group-detail/my-group-detail.component';
import {FullPagesModule} from '../pages/full-pages/full-pages.module';
import {PersonalTemplateComponent} from './personal/personal-template/personal-template.component';
import {AddTemplateModalComponent} from '../components/add-template-modal/add-template-modal.component';
import {AddTemplateModalModule} from '../components/add-template-modal/add-template-modal.module';
import {PersonalPanelsComponent} from './personal/personal-panels/personal-panels.component';
import {AddPanelModalComponent} from '../components/add-panel-modal/add-panel-modal.component';
import {AddPanelModalModule} from '../components/add-panel-modal/add-panel-modal.module';
import {SharedModule} from '../shared/shared.module';
import {PersonalNumberComponent} from './personal/personal-number/personal-number.component';
import {NumberService} from '../shared/services/Numbers/number.service';
import {PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {PersonalBusinessCardComponent} from './personal/personal-business-card/personal-business-card.component';
import {AddBusinessCardModelModule} from '../components/add-business-card-model/add-business-card-model.module';
import {AddBusinessCardModelComponent} from '../components/add-business-card-model/add-business-card-model.component';
import {PersonalScheduledSmsComponent} from './personal/personal-scheduled-sms/personal-scheduled-sms.component';
import {AddScheduleSmsModelComponent} from '../components/add-schedule-sms-model/add-schedule-sms-model.component';
import {AddScheduleSmsModelModule} from '../components/add-schedule-sms-model/add-schedule-sms-model.module';
import {AddScheduleDetailModelComponent} from '../components/add-schedule-detail-model/add-schedule-detail-model.component';
import {AddScheduleDetailModelModule} from '../components/add-schedule-detail-model/add-schedule-detail-model.module';
import { TicketComponent } from './payanak/ticket/ticket.component';
import {GetNameModule} from '../components/get-name/get-name.module';
import {GetNameComponent} from '../components/get-name/get-name.component';
import { AddCreditComponent } from './payanak/add-credit/add-credit.component';
import {NgxLoadingModule} from 'ngx-loading';


@NgModule({
  declarations: [
    MyGroupsComponent,
    MyGroupDetailComponent,
    PersonalTemplateComponent,
    PersonalPanelsComponent,
    PersonalNumberComponent,
    PersonalBusinessCardComponent,
    PersonalScheduledSmsComponent,
    TicketComponent,
    AddCreditComponent
  ],
  imports: [
    CommonModule,
    CartableRoutingModule,
    Ng2SmartTableModule,
    MatPaginatorModule,
    MatButtonModule,
    AddGroupModalModule,
    MatInputModule,
    ConfirmModule,
    FullPagesModule,
    AddTemplateModalModule,
    MatTooltipModule,
    AddPanelModalModule,
    SharedModule,
    PerfectScrollbarModule,
    AddBusinessCardModelModule,
    AddScheduleSmsModelModule,
    AddScheduleDetailModelModule,
    GetNameModule,
    NgxLoadingModule
  ],
  providers: [GroupService, NumberService],
  entryComponents: [
    AddGroupModalComponent,
    ConfirmComponent,
    AddTemplateModalComponent,
    AddPanelModalComponent,
    AddBusinessCardModelComponent,
    AddScheduleSmsModelComponent,
    AddScheduleDetailModelComponent,
    GetNameComponent
  ]
})
export class CartableModule {
}
