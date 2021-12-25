import {Component, OnDestroy, OnInit} from '@angular/core';
import {QueryParamModel} from '../../shared/model/Response/query-param.model';
import {GroupModel} from '../../shared/model/group.model';
import {RoleModel} from '../../shared/model/role.model';
import {Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatSnackBar} from '@angular/material';
import {ToastrService} from 'ngx-toastr';
import {take} from 'rxjs/operators';
import {AddRoleModalComponent} from '../../components/add-role-modal/add-role-modal.component';
import {ConfirmComponent} from '../../components/confirm/confirm.component';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {ContactModel} from '../../shared/model/contact/contact.model';
import {UserService} from '../../shared/services/user/user.service';
import {EditUserRolesComponent} from '../../components/edit-user-roles/edit-user-roles.component';
import {AddCreditModalComponent} from '../../components/add-credit-modal/add-credit-modal.component';
import {Subscription} from 'rxjs';
import {SearchService} from '../../shared/services/search.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit, OnDestroy {
  queryParam: QueryParamModel;
  groups: GroupModel[] = [];
  users: ContactModel[] = [];
  roles: RoleModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  search: string;
  pageNumber = 1;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private userService: UserService,
              private modalService: NgbModal,
              private snakeBar: MatSnackBar,
              private dialog: MatDialog,
              private searchService: SearchService,
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
    this.userService.getAllUsers(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.users = res.Result;
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

  editRole(user: ContactModel) {
    const ref = this.modalService.open(EditUserRolesComponent, {size: 'lg'});
    ref.componentInstance.userId = user.id;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.userService.editUserRole(res, user.id).subscribe(
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

  editCredit(user: ContactModel) {
    const ref = this.modalService.open(AddCreditModalComponent, {size: 'lg'});
    ref.componentInstance.userId = user.id;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.userService.editUserCredit(res, user.id).subscribe(
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

  isNullOrEmpty(str: string): boolean {
    return (str === undefined || str === null || str === '');
  }
}
