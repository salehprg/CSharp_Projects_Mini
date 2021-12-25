import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';

import {SmsService} from '../../shared/services/sms/sms.service';
import {TemplatesService} from '../../shared/services/Templates/Templates.service';
import {GroupService} from '../../shared/services/Groups/group.service';

import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {AddTemplateModalComponent} from '../add-template-modal/add-template-modal.component';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {TemplateModel} from '../../shared/model/sms/template.model';
import {ScheduleSmsInfoModel} from '../../shared/model/sms/schedule-sms-info.model';
import {filter, map} from 'lodash';
import {take} from 'rxjs/operators';
import {NumberService} from '../../shared/services/Numbers/number.service';
import {NumberModel} from '../../shared/model/number/number.model';

@Component({
  selector: 'app-add-schedule-sms-model',
  templateUrl: './add-schedule-sms-model.component.html',
  styleUrls: ['./add-schedule-sms-model.component.scss']
})
export class AddScheduleSmsModelComponent implements OnInit {
  ssForm: FormGroup;
  ssModel: ScheduleSmsInfoModel;
  isLoading = false;
  isEdit = false;
  height = window.screen.availHeight - 320;
  header = 'ارسال زماندار';
  numberDisplay: any[] = [];
  templatesDisplay: any[] = [];

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private smsService: SmsService,
              private templateService: TemplatesService,
              private groupService: GroupService,
              private modalService: NgbModal,
              private store: Store<AppState>,
              private router: Router,
              private numberService: NumberService,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    if (!this.ssModel) {
      this.ssModel = this.initSSI();
    } else {
      this.isEdit = true;
      this.header = 'ارسال زماندار';
    }
    this.initForm();
  }

  initSSI(): ScheduleSmsInfoModel {
    const ssi: ScheduleSmsInfoModel = {
      addedDay: 0,
      addedMonth: 0,
      addedYear: 0,
      code: 0,
      id: -1,
      name: '',
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
      template: {
        body: null,
        id: -1,
        title: null,
        userId: -1
      },
      status: 1,
      number: {
        isBlocked: false,
        number: null,
        user: null,
        username: null,
        isShared: false,
        owner: -1,
        id: -1,
        type: 1,
        createDate: -1,
        password: null
      }
    };
    return ssi;
  }

  initForm() {
    this.ssForm = this.fb.group({
      key: [this.ssModel.code, Validators.compose([
        Validators.required,
        Validators.maxLength(100),
      ])
      ],
      name: [this.ssModel.name, Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100),
      ])
      ],
      year: [this.ssModel.addedYear, Validators.compose([])
      ],
      month: [this.ssModel.addedMonth, Validators.compose([])
      ],
      day: [this.ssModel.addedDay, Validators.compose([])
      ],
      template: ['', Validators.compose([
        this.checkForTemplate.bind(this)
      ])
      ],
      number: ['', Validators.compose([
        this.checkForNumber.bind(this)
      ])
      ],
    });
    this.loadTemplate(true);
  }

  checkForTemplate(group: FormGroup) {
    if (!group.parent || !group.parent.get('template')) {
      return null;
    }
    const link = group.parent.get('template').value;
    return ((link && link.id)) ? null : {noTemplate: true};
  }

  checkForNumber(group: FormGroup) {
    if (!group.parent || !group.parent.get('number')) {
      return null;
    }
    const link = group.parent.get('number').value;
    return ((link && link.id)) ? null : {noNumber: true};
  }

  isSSHasError(controlName: string, validationType: string): boolean {
    const control = this.ssForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  selectedTemplateChanged(event) {
    if (event && event.id === '-1') {
      const ref = this.modalService.open(AddTemplateModalComponent);
      ref.result.then(res => {
        if ((typeof (res)) !== 'string') {
          this.templateService.addTemplate(res).subscribe(
            res => {
              this.successAndLoadTemplates(res);
            }
            ,
            err => {
              this.toaster.error('خطا در ذخیره سازی.', 'خطا');
            });

        }
      });
      ref.result.catch(err => {
      });
    }
  }

  successAndLoadTemplates(res: ResponseModel) {
    if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
      this.toaster.success('ثبت اطلاعات با موفقیت انجام پذیرفت.', res.Status[0].message);
      this.loadTemplate(true);
    } else {
      for (const itm of res.Status) {
        this.toaster.error(res.Status[0].message, 'خطا');
      }
    }
  }

  loadTemplate(event) {
    if (!event) {
      return;
    }
    this.isLoading = true;
    const queryParam = {
      filter: '',
      pageNumber: 1,
      pageSize: 100,
      sortField: '',
      sortOrder: ''
    };
    this.templateService.getUserTemplates(queryParam).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.templatesDisplay = map(res.Result, (data: TemplateModel) => {
            return {
              name: data.title,
              id: data.id
            };
          });
          this.templatesDisplay.push({
            name: 'افزودن پیش نویس ...',
            id: '-1'
          });
          if (this.isEdit) {
            this.ssForm.controls['template'].setValue({
              name: this.ssModel.template.title,
              id: this.ssModel.template.id
            })
            ;
          }
        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات پیش نویس ها', 'خطا');
        }
        this.loadAllNumbers();
        this.isLoading = false;
      },
      err => {
        this.isLoading = false;
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
      }
    );
  }

  loadAllNumbers() {
    const queryParam = {
      filter: '',
      pageNumber: 0,
      pageSize: 1000,
      sortField: '',
      sortOrder: ''
    };
    this.isLoading = true;
    this.numberService.getUserNumbers(queryParam).pipe(
      take(1))
      .subscribe(
        res => {
          if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
            const numbers = filter(res.Result, function(number) {
              return !number.isBlocked;
            });
            this.numberDisplay = map(numbers, (data: NumberModel) => {
              return {
                name: data.number + ' - ' + (data.type === 0 ? 'خدماتی' : 'تبلیغاتی'),
                id: data.id
              };
            });
            if (this.isEdit) {
              this.ssForm.controls['number'].setValue({
                name: this.ssModel.number.number,
                id: this.ssModel.number.id
              })
              ;
            }
          } else {
            console.log(res);
            this.toaster.error('خطا در بارگذاری شماره ها <br/>' + res.Status[0].message , 'خطا');
          }
          this.isLoading = false;
        },
        err => {
          this.isLoading = false;
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
      );
  }

  submit() {
    if (this.ssForm.valid) {
      this.ssModel.code = +this.ssForm.controls['key'].value;
      this.ssModel.name = this.ssForm.controls['name'].value;
      this.ssModel.addedYear = (this.ssForm.controls['year'].value) ?
        +this.ssForm.controls['year'].value :
        0;
      this.ssModel.addedMonth = (this.ssForm.controls['month'].value) ?
        +this.ssForm.controls['month'].value :
        0;
      this.ssModel.addedDay = (this.ssForm.controls['day'].value) ?
        +this.ssForm.controls['day'].value :
        0;
      this.ssModel.template.id = (this.ssForm.controls['template'].value.id) ?
        +this.ssForm.controls['template'].value.id :
        -1;
      this.ssModel.number.id = (this.ssForm.controls['number'].value.id) ?
        +this.ssForm.controls['number'].value.id :
        -1;
      this.activeModal.close(this.ssModel);
    }
  }

}
