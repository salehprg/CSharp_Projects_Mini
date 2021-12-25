import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {PanelListComponent} from './panel-list/panel-list.component';
import {PanelVersionComponent} from './panel-version/panel-version.component';


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'listPanel',
        component: PanelListComponent,
        data: {
          title: 'List Panel'
        }
      },
      {
        path: 'panelVersion',
        component: PanelVersionComponent,
        data: {
          title: 'Version Panel'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PanelManagementRoutingModule { }
