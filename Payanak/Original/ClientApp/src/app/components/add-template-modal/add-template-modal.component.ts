import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {GroupModel} from '../../shared/model/group.model';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';
import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {TemplateModel} from '../../shared/model/sms/template.model';

@Component({
  selector: 'app-add-template-modal',
  templateUrl: './add-template-modal.component.html',
  styleUrls: ['./add-template-modal.component.scss']
})
export class AddTemplateModalComponent implements OnInit {

  templateForm: FormGroup;
  template: TemplateModel;
  isEdit = false;
  header = 'افزودن پیش نویس';
  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private store: Store<AppState>,
              private router: Router) {
  }

  ngOnInit() {
    if (!this.template) {
      this.template = this.initTemplate();
    } else {
      this.isEdit = true;
      this.header = 'اصلاح پیش نویس';
    }
    this.initForm();
  }

  initTemplate(): TemplateModel {
    const template = new TemplateModel();
    return template;
  }

  initForm() {
    this.templateForm = this.fb.group({
      title: [this.template.title, Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      body: [this.template.body, Validators.compose([
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(2000)
      ])
      ],
    });
  }

  isTemplateHasError(controlName: string, validationType: string): boolean {
    const control = this.templateForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  submit() {
    if (this.templateForm.valid) {
      this.template.title = this.templateForm.controls['title'].value;
      this.template.body = this.templateForm.controls['body'].value;
      this.activeModal.close(this.template);
    }
  }
}
