import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';

@Component({
  selector: 'app-get-name',
  templateUrl: './get-name.component.html',
  styleUrls: ['./get-name.component.scss']
})
export class GetNameComponent implements OnInit {

  title: string;
  body: string;
  constructor(public dialogRef: MatDialogRef<GetNameComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) {
    this.title = data.title;
    this.body = data.body;
  }

  ngOnInit() {
  }


}
