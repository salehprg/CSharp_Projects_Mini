import {Component, OnDestroy, OnInit} from '@angular/core';
import {QueryParamModel} from '../../../shared/model/Response/query-param.model';
import {BusinessCardModel} from '../../../shared/model/sms/business-card.model';
import {Router} from '@angular/router';
import {BussinesCardService} from '../../../shared/services/BussinesCard/BussinesCard.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatSnackBar} from '@angular/material';
import {ToastrService} from 'ngx-toastr';
import {take} from 'rxjs/operators';
import {ConfirmComponent} from '../../../components/confirm/confirm.component';
import {AddBusinessCardModelComponent} from '../../../components/add-business-card-model/add-business-card-model.component';
import {ResponseModel} from '../../../shared/model/Response/responseModel';
import {Subscription} from 'rxjs';
import {SearchService} from '../../../shared/services/search.service';

@Component({
  selector: 'app-personal-business-card',
  templateUrl: './personal-business-card.component.html',
  styleUrls: ['./personal-business-card.component.scss']
})
export class PersonalBusinessCardComponent implements OnInit, OnDestroy {

  queryParam: QueryParamModel;
  businessCards: BusinessCardModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageNumber = 1;
  search: string;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private bussinesCardService: BussinesCardService,
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
    this.bussinesCardService.getUserBusinessCards(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          console.log(res);
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.businessCards = res.Result;
            this.length = res.TotalCount;
          } else {
            for (const itm of res.Status) {
              this.toaster.error(res.Status[0].message, 'خطا');
            }
          }
          this.loading = false;
        },
        err => {
          this.loading = false;
          this.toaster.error('خطا در ذخیره سازی.', 'خطا');
        }
      );
  }

  deactivate(businessCard: BusinessCardModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از تغییر وضعیت پنل اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loading = true;
        this.bussinesCardService.deactivateBusinessCard(businessCard.id).subscribe(
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

  edit(businessCard: BusinessCardModel) {
    const ref = this.modalService.open(AddBusinessCardModelComponent, {size: 'lg'});
    ref.componentInstance.bcModel = businessCard;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.bussinesCardService.editBusinessCard(res).subscribe(
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
