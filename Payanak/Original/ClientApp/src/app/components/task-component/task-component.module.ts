import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskComponentComponent } from './task-component.component';



@NgModule({
  declarations: [TaskComponentComponent],
  exports: [
    TaskComponentComponent
  ],
  imports: [
    CommonModule
  ]
})
export class TaskComponentModule { }
