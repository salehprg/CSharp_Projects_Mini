import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {AddGroupModalComponent} from './add-group-modal.component';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {ReactiveFormsModule} from '@angular/forms';
import {MatFormFieldModule} from '@angular/material';
import {ToServerPathPipe} from '../../shared/pipes/to-server-path.pipe';
import {SharedModule} from '../../shared/shared.module';


@NgModule({
  declarations: [
    AddGroupModalComponent,
  ],
  imports: [
    CommonModule,
    NgbModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    SharedModule,
  ],
  exports: [
    AddGroupModalComponent
  ]
})
export class AddGroupModalModule {
}
