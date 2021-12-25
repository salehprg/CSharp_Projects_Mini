import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GetNameComponent } from './get-name.component';
import {MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule} from '@angular/material';
import {FormsModule} from '@angular/forms';



@NgModule({
  declarations: [GetNameComponent],
  imports: [
    CommonModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule,
    MatButtonModule
  ],
  exports: [GetNameComponent]
})
export class GetNameModule { }
