import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {MyGroupsComponent} from './my-groups/my-groups.component';
import {MyGroupDetailComponent} from './my-groups/my-group-detail/my-group-detail.component';
import {PersonalTemplateComponent} from './personal/personal-template/personal-template.component';
import {PersonalPanelsComponent} from './personal/personal-panels/personal-panels.component';
import {PersonalNumberComponent} from './personal/personal-number/personal-number.component';
import {PersonalBusinessCardComponent} from './personal/personal-business-card/personal-business-card.component';
import {PersonalScheduledSmsComponent} from './personal/personal-scheduled-sms/personal-scheduled-sms.component';
import {TicketComponent} from './payanak/ticket/ticket.component';
import {AddCreditComponent} from './payanak/add-credit/add-credit.component';


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'personal/myGroups',
        component: MyGroupsComponent,
        data: {
          title: 'User Owned Groups'
        }
      },
      {
        path: 'personal/myGroups/:id',
        component: MyGroupDetailComponent,
        data: {
          title: 'User Owned Groups'
        }
      },
      {
        path: 'personal/template',
        component: PersonalTemplateComponent,
        data: {
          title: 'User Owned Templates'
        }
      },
      {
        path: 'personal/panels',
        component: PersonalPanelsComponent,
        data: {
          title: 'User Owned Panels'
        }
      },
      {
        path: 'personal/number',
        component: PersonalNumberComponent,
        data: {
          title: 'User Owned Panels'
        }
      },
      {
        path: 'personal/businessCard',
        component: PersonalBusinessCardComponent,
        data: {
          title: 'User Business Cards'
        }
      },
      {
        path: 'personal/scheduledSms',
        component: PersonalScheduledSmsComponent,
        data: {
          title: 'User Scheduled SMS'
        }
      },
      {
        path: 'payanak/ticket',
        component: TicketComponent,
        data: {
          title: 'User Ticket'
        }
      },
      {
        path: 'payanak/addCredit',
        component: AddCreditComponent,
        data: {
          title: 'User Add Ticket'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CartableRoutingModule { }
