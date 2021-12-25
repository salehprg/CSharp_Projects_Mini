import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {ComposeComponent} from '../sms-management/compose/compose.component';
import {AddPanelComponent} from './add-panel/add-panel.component';
import {RoleListComponent} from './role-list/role-list.component';
import {AddBusinessCardComponent} from './add-business-card/add-business-card.component';
import {UserListComponent} from './user-list/user-list.component';
import {AddUserComponent} from './add-user/add-user.component';
import {TicketsListComponent} from './tickets-list/tickets-list.component';


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'addUser',
        component: AddUserComponent,
        data: {
          title: 'Add User'
        }
      },
      {
        path: 'editUser/:id',
        component: AddUserComponent,
        data: {
          title: 'Edit User'
        }
      },
      {
        path: 'listRole',
        component: RoleListComponent,
        data: {
          title: 'List Role'
        }
      },
      {
        path: 'listUser',
        component: UserListComponent,
        data: {
          title: 'List User'
        }
      },
      {
        path: 'listTicket',
        component: TicketsListComponent,
        data: {
          title: 'List Tickets'
        }
      },
      {
        path: 'addBusinessCard',
        component: AddBusinessCardComponent,
        data: {
          title: 'Add Business Card'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserManagementRoutingModule { }
