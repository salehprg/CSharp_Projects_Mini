import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';
import {SmsService} from '../../shared/services/sms/sms.service';
import {GroupService} from '../../shared/services/Groups/group.service';
import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {PanelVersionModel} from '../../shared/model/sms/panel-version.model';
import {FileUploader} from 'ng2-file-upload';

@Component({
  selector: 'app-add-panel-version-modal',
  templateUrl: './add-panel-version-modal.component.html',
  styleUrls: ['./add-panel-version-modal.component.scss']
})
export class AddPanelVersionModalComponent implements OnInit {
  versionForm: FormGroup;
  versionModel: PanelVersionModel;
  isLoading = false;
  isEdit = false;
  height = window.screen.availHeight - 320;
  header = 'افزودن ورژن';
  files: File[] = [];
  uploader: FileUploader = new FileUploader({
    isHTML5: true
  });
  hasAnotherDropZoneOver = false;

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private smsService: SmsService,
              private groupService: GroupService,
              private modalService: NgbModal,
              private store: Store<AppState>,
              private router: Router,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    if (!this.versionModel) {
      this.versionModel = this.initPanelVersion();
    } else {
      this.isEdit = true;
      this.header = 'اصلاح ورژن';
    }
    this.initForm();
  }

  initPanelVersion(): PanelVersionModel {
    const version: PanelVersionModel = {
      path: '',
      maxVersion: 0,
      minVersion: 0,
      nickname: '',
      createDate: -1,
      id: -1
    };
    return version;
  }

  initForm() {
    this.versionForm = this.fb.group({
      nickname: [this.versionModel.nickname, Validators.compose([
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(100),
      ])
      ],
      minVersion: [this.versionModel.minVersion, Validators.compose([
        Validators.required,
        Validators.min(0),
        Validators.pattern(/^-?(0|[1-9]\d*)?$/)
      ])
      ],
      maxVersion: [this.versionModel.maxVersion, Validators.compose([
        Validators.required,
        Validators.min(0),
        Validators.pattern(/^-?(0|[1-9]\d*)?$/)
      ])
      ]
    });
  }

  isPanelHasError(controlName: string, validationType: string): boolean {
    const control = this.versionForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }


  submit() {
    if (this.versionForm.valid && this.files.length > 0) {
      this.versionModel.nickname = this.versionForm.controls['nickname'].value;
      this.versionModel.minVersion = +this.versionForm.controls['minVersion'].value;
      this.versionModel.maxVersion = +this.versionForm.controls['maxVersion'].value;
      console.log(this.versionModel);
      this.activeModal.close({versionModel: this.versionModel, files: this.files});
    }
  }

  fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }

  onSelect(event) {
    console.log(event);
    this.files = [];
    for (const itm of event.addedFiles) {
      this.files.push(itm);
    }
  }

  onRemove(event) {
    console.log(event);
    this.files.splice(this.files.indexOf(event), 1);
  }

}
