import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SmsManagementRoutingModule } from './sms-management-routing.module';
import { ComposeComponent } from './compose/compose.component';
import {NumberService} from '../shared/services/Numbers/number.service';
import {TagInputModule} from 'ngx-chips';
import {FormsModule} from '@angular/forms';
import {NgxLoadingModule} from 'ngx-loading';
import {GroupService} from '../shared/services/Groups/group.service';
import {SharedModule} from '../shared/shared.module';
import { SentComponent } from './sent/sent.component';
import { ReceiveComponent } from './receive/receive.component';
import {EventlySMSComponent } from './EventlySMS/EventlySMS.component'
import { MatTooltipModule } from '@angular/material';
import {AddSmsEventlyModalModule } from '../components/add-sms-evently-modal/add-sms-Evently-modal.module'
import {AddSmsEventlyModalComponent } from '../components/add-sms-evently-modal/add-sms-evently-modal.component'
import { from } from 'rxjs';
TagInputModule.withDefaults({
  tagInput: {
    placeholder: 'افزودن',
    secondaryPlaceholder: 'افزودن'
    // add here other default values for tag-input
  },
  dropdown: {
    displayBy: 'my-display-value',
    // add here other default values for tag-input-dropdown
  }
});

@NgModule({
  declarations: [ComposeComponent, SentComponent, ReceiveComponent , EventlySMSComponent ],
  imports: [
    CommonModule,
    SmsManagementRoutingModule,
    TagInputModule,
    FormsModule,
    NgxLoadingModule,
    SharedModule,
    MatTooltipModule,
    AddSmsEventlyModalModule
  ],
  entryComponents:
    [
      AddSmsEventlyModalComponent
  ],
  providers: [NumberService, GroupService]
})
export class SmsManagementModule { }
