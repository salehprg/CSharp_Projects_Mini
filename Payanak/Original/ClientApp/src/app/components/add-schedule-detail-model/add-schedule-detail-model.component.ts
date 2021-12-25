import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ScheduleSmsInfoModel} from '../../shared/model/sms/schedule-sms-info.model';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatDialog, MatSnackBar} from '@angular/material';
import {SmsService} from '../../shared/services/sms/sms.service';
import {GroupService} from '../../shared/services/Groups/group.service';
import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {map} from 'lodash';
import {ContactModel} from '../../shared/model/contact/contact.model';
import {ScheduleDetailModel} from '../../shared/model/sms/schedule-detail.model';
import * as _moment from 'jalali-moment';
import {ConfirmComponent} from '../confirm/confirm.component';

const moment = _moment;

@Component({
  selector: 'app-add-schedule-detail-model',
  templateUrl: './add-schedule-detail-model.component.html',
  styleUrls: ['./add-schedule-detail-model.component.scss']
})
export class AddScheduleDetailModelComponent implements OnInit {

  @ViewChild('rdoBirthday', {static: true}) rdoBirthday: ElementRef;
  @ViewChild('rdoSpecialDay', {static: true}) rdoSpecialDay: ElementRef;
  @ViewChild('rdoSelectDay', {static: true}) rdoSelectDay: ElementRef;
  ssForm: FormGroup;
  user: ContactModel;
  isLoading = false;
  isEdit = false;
  height = window.screen.availHeight - 320;
  header = 'ارسال زماندار';
  ssiDisplay: any[] = [];
  ssis: any[] = [];
  templatesDisplay: any[] = [];
  ssd: ScheduleDetailModel;

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private smsService: SmsService,
              private groupService: GroupService,
              private modalService: NgbModal,
              private store: Store<AppState>,
              private router: Router,
              private dialog: MatDialog,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    const queryParam = {
      filter: '',
      pageNumber: 1,
      pageSize: 1000,
      sortField: '',
      sortOrder: ''
    };
    this.isLoading = true;
    this.initSSD();
    this.smsService.getUserSSI(queryParam).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.ssis = res.Result;
          this.ssiDisplay = map(res.Result, (data: ScheduleSmsInfoModel) => {
            return {
              name: data.name + ' ( ' + data.code + ')',
              id: data.id
            };
          });

        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
        this.isLoading = false;
      },
      error => {
        this.isLoading = false;
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
      }
    );
    this.initForm();
  }

  initSSD() {
    this.ssd = {
      user: {...this.user},
      parent: {
        id: -1,
        code: -1,
        name: null,
        status: 1,
        template: {
          title: null,
          body: null,
          id: -1,
          userId: this.user.id
        },
        addedDay: 0,
        addedMonth: 0,
        addedYear: 0,
        owner: {
          formId: null,
          id: -1,
          fName: null,
          gender: 1,
          mobilePhone: null,
          nickName: null,
          username: null,
          lName: null,
          telegram: null,
          nationalCode: null,
          longitude: null,
          latitude: null,
          instagram: null,
          address: null,
          birthday: -1,
          specialDay: -1,
          picture: null,
          credit: 0
        },
        number: {
          isBlocked: false,
          number: null,
          user: null,
          username: null,
          isShared: false,
          owner: this.user.id,
          id: -1,
          type: 1,
          createDate: -1,
          password: null
        }
      },
      date: -1,
      counter: 0,
      updatedDate: -1,
      id: -1
    };
  }

  initForm() {
    this.ssForm = this.fb.group({
      day: ['', Validators.compose([
        Validators.required
      ])
      ],
      ssi: ['', Validators.compose([])
      ],
      rdoBirthday: ['', Validators.compose([])
      ],
      rdoSpecialDay: ['', Validators.compose([])
      ],
      rdoSelectDay: ['', Validators.compose([])
      ],
    });
  }

  isSSHasError(controlName: string, validationType: string): boolean {
    const control = this.ssForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  selectedSSIChanged(event) {
    if (!event) {
      return;
    }
    this.smsService.getSSDForUser(this.user.id, event.id).subscribe(
      res => {
        if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          if (res.TotalCount > 0) {
            this.ssd = res.Result;
            console.log(res.Result);
          }
        } else {
          for (const itm of res.Status) {
            this.toaster.error(res.Status[0].message, 'خطا');
          }
        }
      },
      error => {
        this.toaster.error('خطا در دریافت اطلاعات.', 'خطا');

      }
    );
  }

  submit() {
    if (this.ssForm.valid) {
      if (this.rdoBirthday.nativeElement.checked) {
        this.ssd.date = this.user.birthday;
      } else if (this.rdoSelectDay.nativeElement.checked) {
        this.ssd.date =
          ((this.ssForm.controls['day'].value).toDate().getTime() * 10000) + 621355968000000000;
      } else if (this.rdoSpecialDay.nativeElement.checked) {
        this.ssd.date = this.user.specialDay;
      }
      this.ssd.parent.id = this.ssForm.controls['ssi'].value ? this.ssForm.controls['ssi'].value.id : -1;
      this.ssd.user.id = this.user.id;
      this.activeModal.close(this.ssd);
    }
  }

  delete() {
    const dialogTitle = 'تایید عملیات';
    const dialogBody =  'آیا از حذف این ارسال زماندار اطمینان دارید؟';
    const dialogRef = this.dialog.open(ConfirmComponent, {
      height: 'auto',
      data: {title: dialogTitle, body: dialogBody, hasValue: false, value: ''}
    });
    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.smsService.deleteSSD(this.ssd.id).subscribe(
          res => {
            if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
              this.toaster.success('با موفقیت حذف گردید.', 'موفقیت آمیز');
              this.initSSD();
            } else {
              for (const itm of res.Status) {
                this.toaster.error(res.Status[0].message, 'خطا');
              }
            }
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }
        );
      }
    });
  }

  ticksToDate(value: number) {
    const ticks: number = +value;
    if (ticks === -1) {
      return '';
    }
    const epochTicks = 621355968000000000;
    const ticksPerMillisecond = 10000; // there are 10000 .net ticks per millisecond

    const jsTicks = (ticks - epochTicks) / ticksPerMillisecond;
    const tickDate = new Date(jsTicks);
    const dateStr = tickDate.toLocaleDateString('en-US');
    return moment(dateStr);
  }

  selectedChanged() {
    if (this.rdoBirthday.nativeElement.checked) {
      this.ssForm.controls['day'].setValue(this.ticksToDate(this.user.birthday));
    } else if (this.rdoSpecialDay.nativeElement.checked) {
      this.ssForm.controls['day'].setValue(this.ticksToDate(this.user.specialDay));
    }
  }
}
