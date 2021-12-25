import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ContactModel} from '../../shared/model/contact/contact.model';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';

import {TemplatesService} from '../../shared/services/Templates/Templates.service';
import {NumberService} from '../../shared/services/Numbers/number.service';
import {GroupService} from '../../shared/services/Groups/group.service';
import {PanelsService} from '../../shared/services/Panel/Panels.service';

import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {GroupModel} from '../../shared/model/group.model';
import {AddTemplateModalComponent} from '../add-template-modal/add-template-modal.component';
import {AddGroupModalComponent} from '../add-group-modal/add-group-modal.component';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {NumberModel} from '../../shared/model/number/number.model';
import {TemplateModel} from '../../shared/model/sms/template.model';
import {map} from 'lodash';
import {BusinessCardModel} from '../../shared/model/sms/business-card.model';

@Component({
  selector: 'app-add-business-card-model',
  templateUrl: './add-business-card-model.component.html',
  styleUrls: ['./add-business-card-model.component.scss']
})
export class AddBusinessCardModelComponent implements OnInit {
  bcFrom: FormGroup;
  bcModel: BusinessCardModel;
  contacts: ContactModel[] = [];
  contactsDisplay: any[] = [];
  groupsDisplay: any[] = [];
  numbersDisplay: any[] = [];
  templatesDisplay: any[] = [];
  isContactsLoaded = false;
  isLoadingContacts = false;
  isEdit = false;
  height = window.screen.availHeight - 320;
  header = 'افزودن کارت ویزیت';

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private groupService: GroupService,
              private templateService: TemplatesService,
              private numberService: NumberService,
              private panelService: PanelsService,
              private modalService: NgbModal,
              private store: Store<AppState>,
              private router: Router,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    if (!this.bcModel) {
      this.bcModel = this.initPanel();
    } else {
      this.isEdit = true;
      this.header = 'اصلاح کارت ویزیت';
    }
    this.initForm();
    if (this.isEdit) {
      this.loadUserGroups();
      if (this.bcModel.template && this.bcModel.template.id !== -1) {
        this.loadNumberAndTemplate(true);
      }
    }
  }

  initPanel(): BusinessCardModel {
    const panel: BusinessCardModel = {
      isBlocked: false,
      key: '',
      status: 1,
      group: {
        id: -1,
        status: 0,
        title: null,
        descriptions: null,
        name: null,
        picture: null,
        parent: null
      },
      createDate: -1,
      id: -1,
      user: {
        id: -1,
        username: null,
        lName: null,
        fName: null,
        specialDay: -1,
        birthday: -1,
        gender: 0,
        mobilePhone: null,
        address: null,
        instagram: null,
        latitude: null,
        longitude: null,
        nationalCode: null,
        nickName: null,
        telegram: null,
        formId: null,
        picture: null,
        credit: 0
      },
      number: {
        type: 1,
        number: null,
        id: -1,
        owner: -1,
        isShared: false,
        password: null,
        username: null,
        createDate: -1,
        isBlocked: false,
        user: null
      },
      template: {
        body: null,
        id: -1,
        title: null,
        userId: -1
      }
    };
    return panel;
  }

  initForm() {
    this.bcFrom = this.fb.group({
      key: [this.bcModel.key, Validators.compose([
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(100),
      ])
      ],
      owner: [this.isEdit ? {
        name: this.bcModel.user.fName + ' ' + this.bcModel.user.lName,
        id: this.bcModel.user.id
      } : '', Validators.compose([
        Validators.required,
      ])
      ],
      group: ['', Validators.compose([
      ])
      ],
      sendNumber: ['', Validators.compose([
        this.checkForSendNumber.bind(this)
      ])
      ],
      template: ['', Validators.compose([
        this.checkForTemplate.bind(this)
      ])
      ],

    });
    this.isLoadingContacts = true;
    this.panelService.getAllUsersForAssign().subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.contacts = res.Result;
          this.contactsDisplay = map(res.Result, (data: ContactModel) => {
            return {
              name: data.fName + ' ' + data.lName + ' ( ' + data.username + ')',
              id: data.id
            };
          });
          this.isContactsLoaded = false;
        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
        this.isLoadingContacts = false;
      },
      err => {
        this.isLoadingContacts = false;
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
      }
    );
  }

  checkForSendNumber(group: FormGroup) {
    if (!group.parent || !group.parent.get('sendNumber')) {
      return null;
    }
    const link = group.parent.get('sendNumber').value;
    return ( (link && link.id)) ? null : {noSendNumber: true};
  }

  checkForTemplate(group: FormGroup) {
    if (!group.parent || !group.parent.get('template')) {
      return null;
    }
    const link = group.parent.get('template').value;
    return ( (link && link.id)) ? null : {noTemplate: true};
  }

  isPanelHasError(controlName: string, validationType: string): boolean {
    const control = this.bcFrom.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }


  submit() {
    if (this.bcFrom.valid) {
      this.bcModel.key = this.bcFrom.controls['key'].value;
      this.bcModel.user.id = +this.bcFrom.controls['owner'].value.id;
      this.bcModel.group.id = +this.bcFrom.controls['group'].value.id;
      this.bcModel.template.id = (this.bcFrom.controls['template'].value.id ) ?
        +this.bcFrom.controls['template'].value.id :
        -1;
      this.bcModel.number.id = (this.bcFrom.controls['sendNumber'].value.id ) ?
        +this.bcFrom.controls['sendNumber'].value.id :
        -1;
      this.activeModal.close(this.bcModel);
    }
  }
  loadUserAllData() {
    this.loadUserGroups();
    this.loadNumberAndTemplate(true);
  }

  loadUserGroups() {
    this.isLoadingContacts = true;
    const owner = this.bcFrom.controls['owner'].value;
    this.panelService.getUserGroupsForAssign(owner.id).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.contacts = res.Result;
          this.groupsDisplay = map(res.Result, (data: GroupModel) => {
            return {
              name: data.title,
              id: data.id
            };
          });
          this.groupsDisplay.push({
            name: 'افزودن گروه ...',
            id: '-1'
          });
          if (this.isEdit) {
            this.bcFrom.controls['group'].setValue({
              name: this.bcModel.group.title,
              id: this.bcModel.group.id
            })
            ;
          }
          this.isContactsLoaded = false;
        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
        this.isLoadingContacts = false;
      },
      err => {
        this.isLoadingContacts = false;
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
      }
    );
    this.loadNumberAndTemplate(true);
  }

  selectedTemplateChanged(event) {
    const owner = this.bcFrom.controls['owner'].value;
    if (owner && event && event.id === '-1') {
      const ref = this.modalService.open(AddTemplateModalComponent);
      ref.result.then(res => {
        if ((typeof (res)) !== 'string') {
          this.templateService.createTemplateForUser(owner.id, res).subscribe(
            res => {
              this.successAndLoadTemplates(res);
            }
            ,
            err => {
              this.toaster.error('خطا در ذخیره سازی.', 'خطا');
            });

        }
        this.bcFrom.controls['group'].setValue(null);
        console.log(res);
        console.log((typeof (res)));
      });
      ref.result.catch(err => {
      });
    }
  }

  selectedGroupChanged(event) {
    const owner = this.bcFrom.controls['owner'].value;
    if (owner && event && event.id === '-1') {
      const ref = this.modalService.open(AddGroupModalComponent);
      ref.result.then(res => {
        if ((typeof (res)) !== 'string') {
          this.groupService.createGroupForUser(owner.id, res).subscribe(
            res => {
              this.successAndLoadGroups(res);
            }
            ,
            err => {
              this.toaster.error('خطا در ذخیره سازی.', 'خطا');
            });

        }
        this.bcFrom.controls['group'].setValue(null);
        console.log(res);
        console.log((typeof (res)));
      });
      ref.result.catch(err => {
      });
    }
  }

  successAndLoadGroups(res: ResponseModel) {
    if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
      this.toaster.success('ثبت اطلاعات با موفقیت انجام پذیرفت.', res.Status[0].message);
      this.loadUserGroups();
    } else {
      for (const itm of res.Status) {
        this.toaster.error(res.Status[0].message, 'خطا');
      }
    }
  }

  successAndLoadTemplates(res: ResponseModel) {
    if (res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
      this.toaster.success('ثبت اطلاعات با موفقیت انجام پذیرفت.', res.Status[0].message);
      this.loadNumberAndTemplate(true);
    } else {
      for (const itm of res.Status) {
        this.toaster.error(res.Status[0].message, 'خطا');
      }
    }
  }

  loadNumberAndTemplate(event) {
    if (!event) {
      return;
    }
    this.isLoadingContacts = true;
    const owner = this.bcFrom.controls['owner'].value;
    if (!owner) {
      return;
    }
    this.panelService.getUserNumbersForAssign(owner.id).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.contacts = res.Result;
          this.numbersDisplay = map(res.Result, (data: NumberModel) => {
            return {
              name: data.number + ' ' + (data.type === 0 ? 'خدماتی' : 'تبلیغاتی'),
              id: data.id
            };
          });
          if (this.isEdit) {
            this.bcFrom.controls['sendNumber'].setValue({
              name: this.bcModel.number.number + ' ' + (this.bcModel.number.type === 0 ? 'خدماتی' : 'تبلیغاتی'),
              id: this.bcModel.number.id
            })
            ;
          }
          this.panelService.getUserTemplatesForAssign(owner.id).subscribe(
            res => {
              if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
                this.contacts = res.Result;
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
                  this.bcFrom.controls['template'].setValue({
                    name: this.bcModel.template.title,
                    id: this.bcModel.template.id
                  })
                  ;
                }
              } else {
                this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
              }
              this.isLoadingContacts = false;
            },
            err => {
              this.isLoadingContacts = false;
              this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
            }
          );
        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
        this.isLoadingContacts = false;
      },
      err => {
        this.isLoadingContacts = false;
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
      }
    );
  }

}
