import {Component, OnDestroy, OnInit} from '@angular/core';
import {QueryParamModel} from '../../shared/model/Response/query-param.model';
import {SentSMSModel} from '../../shared/model/sms/sent-sms.model';
import {Router} from '@angular/router';
import {GroupService} from '../../shared/services/Groups/group.service';
import {SmsService} from '../../shared/services/sms/sms.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatSnackBar} from '@angular/material';
import {ToastrService} from 'ngx-toastr';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {ReceiveSMSModel} from '../../shared/model/sms/receive-sms.model';
import {Subscription} from 'rxjs';
import {SearchService} from '../../shared/services/search.service';

@Component({
  selector: 'app-receive',
  templateUrl: './receive.component.html',
  styleUrls: ['./receive.component.scss']
})
export class ReceiveComponent implements OnInit, OnDestroy {

  queryParam: QueryParamModel;
  sms: ReceiveSMSModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageNumber = 1;
  search: string;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private groupService: GroupService,
              private smsService: SmsService,
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
    this.smsService.getUserReceivedSms(this.queryParam).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.sms = res.Result;
          this.length = res.TotalCount;
        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
        this.loading = false;
      },
      err => {
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        this.loading = false;
      }
    );
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

  getCorrectString(str) {
    return this.isNullOrEmpty(str) ? '---' : str;
  }

  getTrimmedString(str: string) {
    return str.length > 25 ? str.substr(0, 22) + '...' : str;
  }
}
