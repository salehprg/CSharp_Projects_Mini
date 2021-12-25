import {Component, OnInit, ViewChild} from '@angular/core';
import {MatDialog, MatPaginator, MatSnackBar} from '@angular/material';
import {QueryParamModel} from '../../shared/model/Response/query-param.model';
import {PanelModel} from '../../shared/model/sms/panel.model';
import {Router} from '@angular/router';

import {PanelsService} from '../../shared/services/Panel/Panels.service';

import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ToastrService} from 'ngx-toastr';
import {take} from 'rxjs/operators';
import {ConfirmComponent} from '../../components/confirm/confirm.component';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {AddGroupModalComponent} from '../../components/add-group-modal/add-group-modal.component';
import {AddPanelModalComponent} from '../../components/add-panel-modal/add-panel-modal.component';

@Component({
  selector: 'app-add-panel',
  templateUrl: './add-panel.component.html',
  styleUrls: ['./add-panel.component.scss']
})
export class AddPanelComponent implements OnInit {

  queryParam: QueryParamModel;
  panels: PanelModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  search: string;
  pageNumber = 1;
  constructor(private router: Router,
              private panelService: PanelsService,
              private modalService: NgbModal,
              private snakeBar: MatSnackBar,
              private dialog: MatDialog,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    this.onSearch();
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
    this.panelService.getAllPanels(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.panels = res.Result;
            this.length = res.TotalCount;
          } else {
            this.snakeBar.open('جستجوی ناموفق');
          }
        }
      );
  }

  deactivate(panel: PanelModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از تغییر وضعیت پنل اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.panelService.deactivatePanel(panel.id).subscribe(
          res => {
            this.successAndLoad(res);
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  block(panel: PanelModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از تغییر وضعیت پنل اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.panelService.blockPanel(panel).subscribe(
          res => {
            this.successAndLoad(res);
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  delete(panel: PanelModel) {
    const dialogTitle = 'تایید عملیات';
    const dialogBody = 'آیا از حذف پنل اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.panelService.deletePanel(panel.id).subscribe(
          res => {
            this.successAndLoad(res);
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  lunchAddPanel() {
    const ref = this.modalService.open(AddPanelModalComponent, {size: 'lg'});
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.panelService.addPanel(res).subscribe(
          res => {
            this.successAndLoad(res);
          }
          ,
          err => {
            this.toaster.error('خطا در ذخیره سازی.', 'خطا');
          });
      }
      console.log(res);
      console.log((typeof (res)));
    });
    ref.result.catch(err => {
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

  edit(panel: PanelModel) {
    const ref = this.modalService.open(AddPanelModalComponent, {size: 'lg'});
    ref.componentInstance.panelModel = panel;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.panelService.editPanel(res).subscribe(
          res => {
            this.successAndLoad(res);
          }
          ,
          err => {
            this.toaster.error('خطا در ذخیره سازی.', 'خطا');
          });
      }
      console.log(res);
      console.log((typeof (res)));
    }).catch(err => {
    });
  }

  ceil(itm) {
    return Math.ceil(itm);
  }

  getStart() {
    const no = this.pageNumber - 1;
    return (no * this.pageSize + 1) + '-' + Math.min(no * this.pageSize + this.pageSize, this.length);
  }
}
