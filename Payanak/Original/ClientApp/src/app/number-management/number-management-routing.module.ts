import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {AssignNumberComponent} from './assign-number/assign-number.component';


const routes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'assign',
        component: AssignNumberComponent,
        data: {
          title: 'Assign Number'
        }
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NumberManagementRoutingModule { }
