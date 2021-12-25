import {Component, OnInit} from '@angular/core';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {FormBuilder} from '@angular/forms';
import {MatSnackBar} from '@angular/material';
import {select, Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {RoleModel} from '../../shared/model/role.model';
import {PermissionModel} from '../../shared/model/permission.model';
import {filter, findIndex, map} from 'lodash';
import {selectAllRoles} from '../../shared/selector/auth/role.selector';
import {selectAllPermissions} from '../../shared/selector/auth/permission.selector';
import {UserService} from '../../shared/services/user/user.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-edit-user-roles',
  templateUrl: './edit-user-roles.component.html',
  styleUrls: ['./edit-user-roles.component.scss']
})
export class EditUserRolesComponent implements OnInit {
  height = window.screen.availHeight - 320;
  header = 'اصلاح نقش ها';

  roles: RoleModel[] = [];
  roleIds: any[] = [];
  permissions: PermissionModel[] = [];
  checked: boolean[] = [];
  parentPermissions: PermissionModel[] = [];
  userId: number;

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private userService: UserService,
              private store: Store<AppState>,
              private router: Router,
              public toaster: ToastrService) {
    this.store.pipe(select(selectAllPermissions)).subscribe(
      res => {
        this.permissions = [...res];
        this.checked = new Array(res.length);
        this.parentPermissions = filter(res, (itm) => {
          return !itm.parent;
        });
      }
    );
    this.store.pipe(select(selectAllRoles)).subscribe(
      res => {
        this.roles = res;
      }
    );
  }

  ngOnInit() {
    this.userService.getUserRoles(this.userId).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          const roles = res.Result;
          this.roleIds = [];
          for (const itm of roles) {
            for (const perm of itm.permissions) {
              this.checked[this.getIndex({id: perm})] = true;
            }
            this.roleIds.push(itm.id);
          }
        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
      },
      err => {
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
      }
    );
  }


  getChildPermissions(id: number) {
    return filter(this.permissions, (itm) => {
      return itm.parent === id;
    });
  }

  getIndex(itm) {
    return findIndex(this.permissions, (perm) => {
      return itm.id === perm.id;
    });
  }

  ItemsChanged() {
    // const ids = map(this.roleIds, (itm) => {
    //   return itm.id;
    // });
    const ids = this.roleIds;
    const roles = filter(this.roles, (itm) => {
      const index = findIndex(ids, (rol) => {
        return rol === itm.id;
      });
      return index !== -1;
    });
    this.checked = new Array(this.permissions.length);
    for (const itm of roles) {
      for (const perm of itm.permissions) {
        this.checked[this.getIndex({id: perm})] = true;
      }
    }
  }
  submit() {
    this.activeModal.close(this.roleIds);
  }

}
