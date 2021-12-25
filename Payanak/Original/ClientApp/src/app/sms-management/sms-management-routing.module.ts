import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ComposeComponent} from './compose/compose.component';
import {SentComponent} from './sent/sent.component';
import { ReceiveComponent } from './receive/receive.component';
import {EventlySMSComponent} from './EventlySMS/EventlySMS.component'


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'compose',
        component: ComposeComponent,
        data: {
          title: 'Compose SMS'
        }
      },
      {
        path: 'sent',
        component: SentComponent,
        data: {
          title: 'Sent Sms'
        }
      },
      {
        path: 'eventlySMS',
        component: EventlySMSComponent,
        data: {
          title: 'Evently SMS'
        }
      },
      {
        path: 'received',
        component: ReceiveComponent,
        data: {
          title: 'Sent Sms'
        }
      },
      {
        path: 'inbox/:id',
        component: ComposeComponent,
        data: {
          title: 'User Owned Groups'
        }
      },
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SmsManagementRoutingModule {
}
