import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatDialog, MatSnackBar} from '@angular/material';
import {QueryParamModel} from '../../shared/model/Response/query-param.model';
import {GroupModel} from '../../shared/model/group.model';
import {GroupService} from '../../shared/services/Groups/group.service';
import {take} from 'rxjs/operators';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {AddGroupModalComponent} from '../../components/add-group-modal/add-group-modal.component';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';
import {ConfirmComponent} from '../../components/confirm/confirm.component';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {Subscription} from 'rxjs';
import {SearchService} from '../../shared/services/search.service';

@Component({
  selector: 'app-my-groups',
  templateUrl: './my-groups.component.html',
  styleUrls: ['./my-groups.component.scss']
})
export class MyGroupsComponent implements OnInit, OnDestroy {
  queryParam: QueryParamModel;
  groups: GroupModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageNumber = 1;
  search: string;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private groupService: GroupService,
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
    this.groupService.getUserOwnedGroups(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.groups = res.Result;
            this.length = res.TotalCount;
          } else {
            for (const itm of res.Status) {
              this.toaster.error(res.Status[0].message, '??????');
            }
          }
          this.loading = false;
        },
        err => {
          this.toaster.error('?????? ???? ????????????.', '??????');
          this.loading = false;
        }
      );
  }

  lunchAddGroup() {
    const ref = this.modalService.open(AddGroupModalComponent);
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.groupService.addGroup(res).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.successAndLoad(res);
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, '??????');
              }
            }
            this.loading = false;
          }
          ,
          err => {
            this.loading = false;
            this.toaster.error('?????? ???? ?????????? ????????.', '??????');
          });
      }
    }).catch(err => {
    });
  }

  delete(group: GroupModel) {
    const dialogTitle = '?????????? ????????????';
    const dialogBody = '?????? ???? ?????? ???????? ?? ?????????? ?????????????? ???? ?????????????? ????????????';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loading = true;
        this.groupService.deleteGroup(group.id).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.successAndLoad(res);
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, '??????');
              }
            }
            this.loading = false;
          },
          err => {
            this.loading = false;
            this.toaster.error('?????? ???? ????????????.', '??????');
          }
        );
      }
    });
  }

  edit(group: GroupModel) {
    const ref = this.modalService.open(AddGroupModalComponent, {size: 'lg'});
    ref.componentInstance.group = group;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.groupService.editGroup(res).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.successAndLoad(res);
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, '??????');
              }
            }
            this.loading = false;
          }
          ,
          err => {
            this.loading = false;
            this.toaster.error('?????? ???? ?????????? ????????.', '??????');
          });
      }
    }).catch(err => {
    });
  }

  successAndLoad(res: ResponseModel) {
    if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
      this.toaster.success('?????? ?????????????? ???? ???????????? ?????????? ????????????.', res.Status[0].message);
      this.loadData();
    } else {
      for (const itm of res.Status) {
        this.toaster.error(res.Status[0].message, '??????');
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
      return '??????';
    }
    return (no * this.pageSize + 1) + ' ???? ' + Math.min(no * this.pageSize + this.pageSize, this.length);
  }

  getTrimmedString(str: string) {
    return str.length > 45 ? str.substr(0, 42) + '...' : str;
  }
}
