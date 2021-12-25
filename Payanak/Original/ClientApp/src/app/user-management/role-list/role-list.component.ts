import {Component, OnDestroy, OnInit} from '@angular/core';
import {QueryParamModel} from '../../shared/model/Response/query-param.model';
import {GroupModel} from '../../shared/model/group.model';
import {Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatSnackBar} from '@angular/material';
import {ToastrService} from 'ngx-toastr';
import {take} from 'rxjs/operators';
import {ConfirmComponent} from '../../components/confirm/confirm.component';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {RoleModel} from '../../shared/model/role.model';
import {AuthService} from '../../shared/services/auth/auth.service';
import {AddRoleModalComponent} from '../../components/add-role-modal/add-role-modal.component';
import {SearchService} from '../../shared/services/search.service';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-role-list',
  templateUrl: './role-list.component.html',
  styleUrls: ['./role-list.component.scss']
})
export class RoleListComponent implements OnInit, OnDestroy {
  queryParam: QueryParamModel;
  groups: GroupModel[] = [];
  roles: RoleModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  search: string;
  pageNumber = 1;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private authService: AuthService,
              private modalService: NgbModal,
              private searchService: SearchService,
              private snakeBar: MatSnackBar,
              private dialog: MatDialog,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    this.onSearch();
    const searchSubscription = this.searchService.filterChanged.subscribe(
      res => {
        this.onSearch(res);
      }
    );
    this.subscriptions.push(searchSubscription);
  }

  ngOnDestroy() {
    this.subscriptions.forEach(el => el.unsubscribe());
  }

  onSearch(query: string = '') {
    this.search = query;
    this.loadData();
  }

  loadData() {
    this.queryParam = {
      filter: this.search,
      pageNumber: this.pageNumber,
      pageSize: this.pageSize,
      sortField: '',
      sortOrder: ''
    };
    this.loading = true;
    this.authService.getAllRolesWithQuery(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.roles = res.Result;
            this.length = res.TotalCount;
          } else {
            for (const itm of res.Status) {
              this.toaster.error(res.Status[0].message, 'خطا');
            }
          }
          this.loading = false;
        },
        err => {
          this.toaster.error('خطا در عملیات.', 'خطا');
          this.loading = false;
        }
      );
  }

  lunchAddRole() {
    const ref = this.modalService.open(AddRoleModalComponent);
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {

        this.loading = true;
        this.authService.addRole(res).subscribe(
          res => {
            this.loading = false;
            this.successAndLoad(res);
          }
          ,
          err => {
            this.loading = false;
            this.toaster.error('خطا در ذخیره سازی.', 'خطا');
          });

      }
    }).catch(err => {
    });
  }

  delete(role: RoleModel) {
    if (!role.canDelete) {
      return;
    }
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از حذف گروه و تمامی کاربران آن اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loading = true;
        this.authService.deleteRole(role.id).subscribe(
          res => {
            this.loading = false;
            this.successAndLoad(res);
          },
          err => {
            this.loading = false;
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  edit(role: RoleModel) {
    const ref = this.modalService.open(AddRoleModalComponent, {size: 'lg'});
    ref.componentInstance.role = role;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.authService.editRole(res).subscribe(
          res => {
            this.loading = false;
            this.successAndLoad(res);
          }
          ,
          err => {
            this.loading = false;
            this.toaster.error('خطا در ذخیره سازی.', 'خطا');
          });
      }
      console.log(res);
      console.log((typeof (res)));
    }).catch(err => {
    });
  }

  successAndLoad(res: ResponseModel) {
    if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
      this.toaster.success('ثبت اطلاعات با موفقیت انجام پذیرفت.', res.Status[0].message);
      this.loadData();
    } else {
      for (const itm of res.Status) {
        this.toaster.error(res.Status[0].message, 'خطا');
      }
    }
  }

  ceil(itm) {
    return Math.ceil(itm);
  }

  getStart() {
    const no = this.pageNumber - 1;
    const min = Math.min(no * this.pageSize + this.pageSize, this.length);
    if (min === 0) {
      return 'صفر';
    }
    return (no * this.pageSize + 1) + ' تا ' + Math.min(no * this.pageSize + this.pageSize, this.length);
  }
}
