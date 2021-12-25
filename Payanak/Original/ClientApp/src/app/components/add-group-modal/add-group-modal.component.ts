import {Component, OnInit} from '@angular/core';
import {MatSnackBar} from '@angular/material';
import {AppState} from '../../shared/reducers';
import {Store} from '@ngrx/store';
import {Router} from '@angular/router';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {GroupModel} from '../../shared/model/group.model';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';

@Component({
  selector: 'app-add-group-modal',
  templateUrl: './add-group-modal.component.html',
  styleUrls: ['./add-group-modal.component.scss']
})
export class AddGroupModalComponent implements OnInit {
  groupForm: FormGroup;
  group: GroupModel;
  imageFile: File;
  isEdit = false;
  header = 'افزودن گروه';

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private store: Store<AppState>,
              private router: Router) {
  }

  ngOnInit() {
    if (!this.group) {
      this.group = this.initGroup();
    } else {
      this.isEdit = true;
      this.header = 'اصلاح گروه';
    }
    this.initForm();
  }

  initGroup(): GroupModel {
    const group = new GroupModel();
    group.picture = 'assets/img/portrait/avatars/avatar-08.png';
    return group;
  }

  initForm() {
    this.groupForm = this.fb.group({
      name: [this.group.name, Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      title: [this.group.title, Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      descriptions: [this.group.descriptions, Validators.compose([
        Validators.maxLength(1000)
      ])
      ],
    });
  }

  isGroupHasError(controlName: string, validationType: string): boolean {
    const control = this.groupForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  imageSelected(event) {
    if (!event || !event.target || !event.target.files || event.target.files.length === 0) {
      return;
    }
    this.imageFile = event.target.files[0];
    const fr = new FileReader();
    fr.onload = (ev => {
      this.group.picture = fr.result as string;
    });
    fr.readAsDataURL(this.imageFile);
  }

  submit() {
    if (this.groupForm.valid) {
      this.group.title = this.groupForm.controls['title'].value;
      this.group.name = this.groupForm.controls['name'].value;
      this.group.descriptions = this.groupForm.controls['descriptions'].value;
      this.activeModal.close(this.group);
    }
  }
}
