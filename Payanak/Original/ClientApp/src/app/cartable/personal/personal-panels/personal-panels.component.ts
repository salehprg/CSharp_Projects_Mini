import {Component, OnDestroy, OnInit} from '@angular/core';
import {MatDialog, MatSnackBar} from '@angular/material';
import {QueryParamModel} from '../../../shared/model/Response/query-param.model';
import {Router} from '@angular/router';
import {PanelsService} from '../../../shared/services/Panel/Panels.service';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {ToastrService} from 'ngx-toastr';
import {take} from 'rxjs/operators';
import {ConfirmComponent} from '../../../components/confirm/confirm.component';
import {ResponseModel} from '../../../shared/model/Response/responseModel';
import {PanelModel} from '../../../shared/model/sms/panel.model';
import {AddPanelModalComponent} from '../../../components/add-panel-modal/add-panel-modal.component';
import {Subscription} from 'rxjs';
import {SearchService} from '../../../shared/services/search.service';

@Component({
  selector: 'app-personal-panels',
  templateUrl: './personal-panels.component.html',
  styleUrls: ['./personal-panels.component.scss']
})
export class PersonalPanelsComponent implements OnInit, OnDestroy {

  queryParam: QueryParamModel;
  panels: PanelModel[] = [];
  length = 0;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 25, 100];
  pageNumber = 1;
  search: string;
  loading = false;
  private subscriptions: Subscription[] = [];

  constructor(private router: Router,
              private panelService: PanelsService,
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
    this.panelService.getUserPanels(this.queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.panels = res.Result;
            this.length = res.TotalCount;
          } else {
            for (const itm of res.Status) {
              this.toaster.error(res.Status[0].message, '??????');
            }
          }
          this.loading = false;
        },
        err => {
          this.loading = false;
          this.toaster.error('?????? ???? ?????????? ????????.', '??????');
        }
      );
  }

  deactivate(panel: PanelModel) {
    const dialogTitle = '?????????? ????????????';
    const dialogBody = '?????? ???? ?????????? ?????????? ?????? ?????????????? ????????????';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loading = true;
        this.panelService.deactivatePanel(panel.id).subscribe(
          res => {
            this.loading = false;
            this.successAndLoad(res);
          },
          err => {
            this.loading = false;
            this.toaster.error('?????? ???? ????????????.', '??????');
          }
        );
      }
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

  edit(panel: PanelModel) {
    const ref = this.modalService.open(AddPanelModalComponent, {size: 'lg'});
    ref.componentInstance.panelModel = panel;
    ref.result.then(res => {
      if ((typeof (res)) !== 'string') {
        this.loading = true;
        this.panelService.editPanel(res).subscribe(
          res => {
            this.loading = false;
            this.successAndLoad(res);
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
}
