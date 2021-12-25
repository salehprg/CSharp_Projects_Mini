import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.scss']
})
export class ConfirmComponent implements OnInit {
  title: string;
  body: string;
  constructor(public dialogRef: MatDialogRef<ConfirmComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) {
    this.title = data.title;
    this.body = data.body;
  }

  ngOnInit() {
  }

}
