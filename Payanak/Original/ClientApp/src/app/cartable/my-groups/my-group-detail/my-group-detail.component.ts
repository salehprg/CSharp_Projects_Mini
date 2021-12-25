import {Component, OnDestroy, OnInit} from '@angular/core';
import {GroupModel} from '../../../shared/model/group.model';
import {ActivatedRoute, Router} from '@angular/router';
import {GroupService} from '../../../shared/services/Groups/group.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatSnackBar} from '@angular/material';
import {ToastrService} from 'ngx-toastr';
import {Subscription} from 'rxjs';
import * as tableData from '../../../shared/data/smart-data-table';
import {LocalDataSource} from 'ng2-smart-table';
import {QueryParamModel} from '../../../shared/model/Response/query-param.model';
import {ContactModel} from '../../../shared/model/contact/contact.model';
import {take} from 'rxjs/operators';
import {ConfirmComponent} from '../../../components/confirm/confirm.component';
import {ResponseModel} from '../../../shared/model/Response/responseModel';
import {AddScheduleDetailModelComponent} from '../../../components/add-schedule-detail-model/add-schedule-detail-model.component';
import {SmsService} from '../../../shared/services/sms/sms.service';
import {SearchService} from '../../../shared/services/search.service';


@Component({
  selector: 'app-my-group-detail',
  templateUrl: './my-group-detail.component.html',
  styleUrls: ['./my-group-detail.component.scss']
})
export class MyGroupDetailComponent implements OnInit, OnDestroy {
  group: GroupModel;
  settings = tableData.settings;
  source: LocalDataSource;
  queryParam: QueryParamModel;
  users: ContactModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageNumber = 1;
  search: string;
  private subscriptions: Subscription[] = [];
  loading = false;

  constructor(private router: Router,
              private activatedRoute: ActivatedRoute,
              private groupService: GroupService,
              private smsService: SmsService,
              private modalService: NgbModal,
              private snakeBar: MatSnackBar,
              private dialog: MatDialog,
              private searchService: SearchService,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    this.source = new LocalDataSource();
    const routeSubscription = this.activatedRoute.params.subscribe(params => {
      const id = params['id'];
      if (id && id > 0) {
        this.loading = true;
        this.groupService.getGroupById(id).subscribe(
          res => {
            if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.group = res.Result;
              this.loading = false;
              this.loadData();
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, 'خطا');
              }
              this.loading = false;
            }
          },
          err => {
            this.loading = false;
            this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
          });
      } else {
        this.router.navigate(['..']);
      }
    });
    this.subscriptions.push(routeSubscription);
    const searchSubscription = this.searchService.filterChanged.subscribe(
      res => {
        this.onSearch(res);
      }
    );
    this.subscriptions.push(searchSubscription);

  }

  ngOnDestroy() {
    this.subscriptions.forEach(sb => sb.unsubscribe());
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
    if (!this.group) {
      this.toaster.error('گروهی انتخاب نشده است.', 'خطا');
      return;
    }
    this.loading = true;
    this.groupService.getGroupContacts(this.group.id, this.queryParam).pipe(
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

  delete(itm: ContactModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از حذف کاربر ' + itm.fName + ' ' + itm.lName + ' از گروه آن اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loading = true;
        this.groupService.deleteContactFromGroup(itm.id, this.group.id).subscribe(
          res => {
            this.successAndLoad(res);
            this.loading = false;
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
            this.loading = false;
          }
        );
      }
    });
  }

  AddSSI(itm: ContactModel) {
    const ref = this.modalService.open(AddScheduleDetailModelComponent);
    ref.componentInstance.user = itm;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.smsService.addSSD(res).subscribe(
          res => {
            this.successAndLoad(res);
            this.loading = false;
          }
          ,
          err => {
            this.toaster.error('خطا در ذخیره سازی.', 'خطا');
            this.loading = false;
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
