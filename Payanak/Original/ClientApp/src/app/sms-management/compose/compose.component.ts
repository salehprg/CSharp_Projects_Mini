import {Component, OnInit} from '@angular/core';
import {NumberService} from '../../shared/services/Numbers/number.service';
import {take} from 'rxjs/operators';
import {NumberModel} from '../../shared/model/number/number.model';
import {Router} from '@angular/router';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatSnackBar} from '@angular/material';
import {ToastrService} from 'ngx-toastr';
import {Validators} from '@angular/forms';
import {ComposeSMSModel} from '../../shared/model/sms/compose-sms.model';

import {SmsService} from '../../shared/services/sms/sms.service';
import {TemplatesService} from '../../shared/services/Templates/Templates.service';

import {TaskService} from '../../shared/services/task.service';
import {filter, map} from 'lodash';
import {GroupService} from '../../shared/services/Groups/group.service';
import {GroupModel} from '../../shared/model/group.model';
import {TemplateModel} from '../../shared/model/sms/template.model';

@Component({
  selector: 'app-compose',
  templateUrl: './compose.component.html',
  styleUrls: ['./compose.component.scss']
})
export class ComposeComponent implements OnInit {
  numbers: NumberModel[] = [];
  sendTo: string[] = [];
  groups: any[] = [];
  templates: any[] = [];
  groupIds: GroupModel[] = [];
  templateIds: TemplateModel[] = [];
  templateId: number;
  validators = [Validators.pattern(/^-?(0|[0-9]\d*)?$/)];
  errorMessages = {
    pattern: 'لطفا فرمت شماره همراه را رعایت کنید.',
  };
  header: string;
  body = '';
  sendNumber: number;
  loading = false;
  gOrN = '0';
  tOrB = '1';
  partLoding: boolean[] = [false, false, false];

  constructor(private numberService: NumberService,
              private smsService: SmsService,
              private templateService: TemplatesService,
              private taskService: TaskService,
              private groupService: GroupService,
              private router: Router,
              private modalService: NgbModal,
              private snakeBar: MatSnackBar,
              private dialog: MatDialog,
              public toaster: ToastrService) {

  }

  ngOnInit() {
    this.loadAllNumbers();
    this.loadAllGroups();
    this.loadAllTemplates();
  }

  loadAllGroups() {
    const queryParam = {
      filter: '',
      pageNumber: 0,
      pageSize: 1000,
      sortField: '',
      sortOrder: ''
    };
    this.partLoding[0] = true;
    this.groupService.getUserOwnedGroups(queryParam)
      .pipe(take(1))
      .subscribe(res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.groups = res.Result;
          } else {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
          this.partLoding[0] = false;
        },
        err => {
          this.partLoding[0] = false;
          console.log(err)
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        });
  }

  loadAllTemplates() {
    const queryParam = {
      filter: '',
      pageNumber: 0,
      pageSize: 1000,
      sortField: '',
      sortOrder: ''
    };
    this.partLoding[1] = true;
    this.templateService.getUserTemplates(queryParam)
      .pipe(take(1))
      .subscribe(res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.templates = res.Result;
          } else {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
          this.partLoding[1] = false;
        },
        err => {
          this.partLoding[1] = false;
          console.log(err)
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        });
  }

  loadAllNumbers() {
    const queryParam = {
      filter: '',
      pageNumber: 0,
      pageSize: 1000,
      sortField: '',
      sortOrder: ''
    };
    this.partLoding[2] = true;
    this.numberService.getAllNumbers(queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            this.numbers = filter(res.Result, function(number) {
              return !number.isBlocked;
            });
          } else {

            this.toaster.error('خطا در عملیات.', 'خطا');
          }
          this.partLoding[2] = false;
        },
        err => {
          this.partLoding[2] = false;
          console.log(err)
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
      );
  }

  ComposeSMS() {
    this.loading = true;
    const tmpIds = map(this.templateIds, (itm) => {
      return itm.id;
    });
    if (!(this.tOrB === '1' && tmpIds && tmpIds.length > 0) &&
      !(this.tOrB === '0' && this.body && this.body.length > 0)) {
      this.toaster.error('متن یا پیش نویس را وارد کنید.', 'خطا');
      this.loading = false;
      return;
    }
    const data: ComposeSMSModel = {
      header: this.header,
      numbers: this.gOrN === '0' ? this.sendTo : null,
      body: this.tOrB === '0' ? this.body : null,
      sendNumber: +this.sendNumber,
      groupIds: this.gOrN === '1' ? map(this.groupIds, (itm) => {
        return itm.id;
      }) : null,
      templateId: this.tOrB === '1' && tmpIds && tmpIds.length > 0 ? tmpIds[0] : null,
    };
    this.smsService.ComposeSms(data).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.taskService.addNewTaskId(res.Result);
          this.toaster.show('لطفا از قسمت فعالیت منتظر نتیجه باشید.', 'اطلاعات');
        } else {
          for (const itm of res.Status) {
            this.toaster.error(itm.message, 'خطا');
          }
        }
        this.loading = false;
      },
      err => {
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        this.loading = false;
      }
    );
  }

  getBodyType() {
    const body = this.tOrB === '0' ? this.body :
      (this.templateIds && this.templateIds.length > 0 ? this.templateIds[0].body : '');
    if (this.isPersian(body)) {
      return 'فارسی';
    } else {
      return 'انگلیسی';
    }
  }

  calculateSMSLength() {
    const body = this.tOrB === '0' ? this.body :
      (this.templateIds && this.templateIds.length > 0 ? this.templateIds[0].body : '');
    const isPersian = this.isPersian(body);
    if (isPersian) {
      if (body.length <= 70) {
        return 1;
      } else {
        return 1 + Math.ceil((body.length - 70) / 66);
      }
    } else {
      if (body.length <= 160) {
        return 1;
      } else {
        return 1 + Math.ceil((body.length - 160) / 152);
      }
    }
  }


  isPersian(obj) {
    const patt = new RegExp('[^\u0000-\u009F]');
    return patt.test(obj);
  }

  getBodyLength() {
    const bodyLen = this.tOrB === '0' ? this.body.length :
      (this.templateIds && this.templateIds.length > 0 ? this.templateIds[0].body.length : 0);
    return bodyLen;
  }

  partLoading() {
    return this.partLoding[0] || this.partLoding[1] || this.partLoding[2];
  }

}
