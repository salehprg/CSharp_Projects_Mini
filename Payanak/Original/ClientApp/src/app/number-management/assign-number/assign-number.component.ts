import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatDialog, MatSnackBar} from '@angular/material';
import {QueryParamModel} from '../../shared/model/Response/query-param.model';
import {Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ToastrService} from 'ngx-toastr';
import {take} from 'rxjs/operators';
import {ConfirmComponent} from '../../components/confirm/confirm.component';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {NumberService} from '../../shared/services/Numbers/number.service';
import {AddNumberModalComponent} from '../../components/add-number-modal/add-number-modal.component';
import {NumberModel} from '../../shared/model/number/number.model';
import {Subscription} from 'rxjs';
import {SearchService} from '../../shared/services/search.service';

@Component({
  selector: 'app-assign-number',
  templateUrl: './assign-number.component.html',
  styleUrls: ['./assign-number.component.scss']
})
export class AssignNumberComponent implements OnInit, OnDestroy {

  queryParam: QueryParamModel;
  numbers: NumberModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageNumber = 1;
  search: string;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private numberService: NumberService,
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
    this.numberService.getAllNumbers(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.numbers = res.Result;
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

  lunchAddNumber() {
    const ref = this.modalService.open(AddNumberModalComponent);
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.numberService.addNumber(res).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.successAndLoad(res);
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, 'خطا');
              }
            }
            this.loading = false;
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

  delete(number: NumberModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از حذف شماره این کاربر اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loading = true;
        this.numberService.deleteNumber(number.id).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.successAndLoad(res);
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, 'خطا');
              }
            }
            this.loading = false;
          },
          err => {
            this.loading = false;
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  toggleBlock(number: NumberModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.numberService.toggleBlockNumber(number).subscribe(
          res => {
            if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.successAndLoad(res);
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, 'خطا');
              }
            }
            this.loading = false;
          },
          err => {
            this.loading = false;
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
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
