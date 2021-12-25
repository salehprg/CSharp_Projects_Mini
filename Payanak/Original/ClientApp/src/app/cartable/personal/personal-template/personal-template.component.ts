import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatDialog, MatPaginator, MatSnackBar} from '@angular/material';
import {QueryParamModel} from '../../../shared/model/Response/query-param.model';
import {GroupModel} from '../../../shared/model/group.model';
import {Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ToastrService} from 'ngx-toastr';
import {take} from 'rxjs/operators';
import {AddGroupModalComponent} from '../../../components/add-group-modal/add-group-modal.component';
import {ConfirmComponent} from '../../../components/confirm/confirm.component';
import {ResponseModel} from '../../../shared/model/Response/responseModel';
import {TemplateModel} from '../../../shared/model/sms/template.model';
import {TemplatesService} from '../../../shared/services/Templates/Templates.service';
import {AddTemplateModalComponent} from '../../../components/add-template-modal/add-template-modal.component';
import {Subscription} from 'rxjs';
import {SearchService} from '../../../shared/services/search.service';

@Component({
  selector: 'app-personal-template',
  templateUrl: './personal-template.component.html',
  styleUrls: ['./personal-template.component.scss']
})
export class PersonalTemplateComponent implements OnInit, OnDestroy {

  queryParam: QueryParamModel;
  templates: TemplateModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageNumber = 1;
  search: string;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private templateService: TemplatesService,
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
    this.templateService.getUserTemplates(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.templates = res.Result;
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

  lunchAddTemplate() {
    const ref = this.modalService.open(AddTemplateModalComponent);
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.templateService.addTemplate(res).subscribe(
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

  delete(template: TemplateModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از حذف پیش نویس اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loading = true;
        this.templateService.deleteTemplate(template.id).subscribe(
          res => {
            this.loading = false;
            this.successAndLoad(res);
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
            this.loading = false;
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

  edit(template: TemplateModel) {
    const ref = this.modalService.open(AddTemplateModalComponent, {size: 'lg'});
    ref.componentInstance.template = template;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.templateService.editTemplate(res).subscribe(
          res => {
            this.loading = false;
            this.successAndLoad(res);
          },
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

  getTrimmedString(str: string) {
    return str.length > 45 ? str.substr(0, 42) + '...' : str;
  }
}
