import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';
import {select, Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {selectAllPermissions} from '../../shared/selector/auth/permission.selector';
import {PermissionModel} from '../../shared/model/permission.model';
import {filter, findIndex} from 'lodash';
import {RoleModel} from '../../shared/model/role.model';

@Component({
  selector: 'app-add-role-modal',
  templateUrl: './add-role-modal.component.html',
  styleUrls: ['./add-role-modal.component.scss']
})
export class AddRoleModalComponent implements OnInit {
  roleForm: FormGroup;
  role: RoleModel;
  permissions: PermissionModel[] = [];
  checked: boolean[] = [];
  parentPermissions: PermissionModel[] = [];
  isEdit = false;
  header = 'افزودن نقش';
  height = window.screen.availHeight - 320;



  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private store: Store<AppState>,
              private router: Router) {
    this.store.pipe(select(selectAllPermissions)).subscribe(
      res => {
        this.permissions = [...res];
        this.checked = new Array(res.length);
        if (this.role && this.role.permissions) {
          for (const itm of this.role.permissions) {
            const index = findIndex(this.permissions, (perm) => {
              return itm === perm.id;
            });
            this.checked[index] = true;
          }
        }
        this.parentPermissions = filter(res, (itm) => {
          return !itm.parent;
        });
      }
    );
  }

  ngOnInit() {
    if (!this.role) {
      this.role = this.initRole();
    } else {
      this.isEdit = true;
      this.header = 'اصلاح نقش';
    }
    if (this.role && this.role.permissions) {
      for (const itm of this.role.permissions) {
        const index = findIndex(this.permissions, (perm) => {
          return itm === perm.id;
        });
        this.checked[index] = true;
      }
    }
    this.initForm();
  }

  initRole(): RoleModel {
    const role: RoleModel = {
      name: '',
      canDelete: true,
      canEdit: true,
      title: '',
      id: -1,
      permissions: []
    };
    return role;
  }

  initForm() {
    this.roleForm = this.fb.group({
      title: [this.role.title, Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      name: [this.role.name, Validators.compose([
        Validators.required,
        Validators.minLength(1),
        Validators.maxLength(2000)
      ])
      ],
    });
  }

  isTemplateHasError(controlName: string, validationType: string): boolean {
    const control = this.roleForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  submit() {
    if (this.roleForm.valid) {
      this.role.title = this.roleForm.controls['title'].value;
      this.role.name = this.roleForm.controls['name'].value;
      let i = 0;
      this.role.permissions = [];
      for (const itm of this.checked) {
        if (itm) {
          this.role.permissions.push(this.permissions[i].id);
        }
        i++;
      }
      this.activeModal.close(this.role);
    }
  }

  getChildPermissions(id: number) {
    return filter(this.permissions, (itm) => {
      return itm.parent === id;
    });
  }

  parentCliked(itm, event) {
    if (!this.role.canEdit) {
      return;
    }
    const index = findIndex(this.permissions, (perm) => {
      return itm.id === perm.id;
    });
    let i = 0;
    for (const perm of this.permissions) {
      if (perm.parent === itm.id) {
        this.checked[i] = this.checked[index];
      }
      i++;
    }
  }

  childCliked(itm, event) {
    if (!this.role.canEdit) {
      return;
    }
    const parentIndex = findIndex(this.permissions, (perm) => {
      return itm.parent === perm.id;
    });
    let i = 0;
    let parentCheck = false;
    for (const perm of this.permissions) {
      if (perm.parent === this.permissions[parentIndex].id) {
        parentCheck = parentCheck || this.checked[i];
      }
      i++;
    }
    this.checked[parentIndex] = parentCheck;
  }

  getIndex(itm) {
    return findIndex(this.permissions, (perm) => {
      return itm.id === perm.id;
    });
  }
}
