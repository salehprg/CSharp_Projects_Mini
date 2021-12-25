import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ConfirmComponent } from './confirm.component';
import {MatButtonModule, MatDialogModule, MatFormFieldModule, MatInputModule} from '@angular/material';
import {FormsModule} from '@angular/forms';



@NgModule({
   declarations: [
      ConfirmComponent
   ],
   imports: [
      CommonModule,
      MatDialogModule,
      MatFormFieldModule,
      MatInputModule,
      FormsModule,
      MatButtonModule
   ],
   exports: [
      ConfirmComponent
   ]
})
export class ConfirmModule { }
