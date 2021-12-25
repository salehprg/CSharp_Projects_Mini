import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ContactModel} from '../../shared/model/contact/contact.model';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';
import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {map} from 'lodash';

import {PanelsService} from '../../shared/services/Panel/Panels.service';
import {TemplatesService} from '../../shared/services/Templates/Templates.service';

import {GroupModel} from '../../shared/model/group.model';
import {AddGroupModalComponent} from '../add-group-modal/add-group-modal.component';
import {GroupService} from '../../shared/services/Groups/group.service';
import {ResponseModel} from '../../shared/model/Response/responseModel';
import {PanelModel} from '../../shared/model/sms/panel.model';
import {NumberModel} from '../../shared/model/number/number.model';
import {TemplateModel} from '../../shared/model/sms/template.model';
import {AddTemplateModalComponent} from '../add-template-modal/add-template-modal.component';

@Component({
  selector: 'app-add-panel-modal',
  templateUrl: './add-panel-modal.component.html',
  styleUrls: ['./add-panel-modal.component.scss']
})
export class AddPanelModalComponent implements OnInit {
  panelForm: FormGroup;
  panelModel: PanelModel;
  contacts: ContactModel[] = [];
  contactsDisplay: any[] = [];
  groupsDisplay: any[] = [];
  numbersDisplay: any[] = [];
  templatesDisplay: any[] = [];
  isContactsLoaded = false;
  isLoadingContacts = false;
  hasTemplate = false;
  isEdit = false;
  height = window.screen.availHeight - 320;
  header = 'افزودن پنل';

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private panelService: PanelsService,
              private templateService: TemplatesService,
              private groupService: GroupService,
              private modalService: NgbModal,
              private store: Store<AppState>,
              private router: Router,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    if (!this.panelModel) {
      this.panelModel = this.initPanel();
    } else {
      this.isEdit = true;
      this.header = 'اصلاح پنل';
    }
    this.initForm();
    if (this.isEdit) {
      this.loadUserGroups();
      if (this.panelModel.template && this.panelModel.template.id !== -1) {
        this.hasTemplate = true;
        this.loadNumberAndTemplate(this.hasTemplate);
      }
    }
  }

  initPanel(): PanelModel {
    const panel: PanelModel = {
      isBlocked: false,
      status: 1,
      serial: '',
      group: {
        id: -1,
        status: 0,
        title: null,
        descriptions: null,
        name: null,
        picture: null,
        parent: null
      },
      name: '',
      createDate: -1,
      hashId: null,
      id: -1,
      lastActivity: -1,
      number: null,
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
      version: null,
      sendNumber: {
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
    this.panelForm = this.fb.group({
      serial: [this.panelModel.serial.length > 6 ? this.panelModel.serial.substr(5) : '', Validators.compose([
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(100),
      ])
      ],
      name: [this.panelModel.name, Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      owner: [this.isEdit ? {
        name: this.panelModel.user.fName + ' ' + this.panelModel.user.lName,
        id: this.panelModel.user.id
      } : '', Validators.compose([
        Validators.required,
      ])
      ],
      group: ['', Validators.compose([
        Validators.required,
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
    return (!this.hasTemplate || (link && link.id)) ? null : {noSendNumber: true};
  }

  checkForTemplate(group: FormGroup) {
    if (!group.parent || !group.parent.get('template')) {
      return null;
    }
    const link = group.parent.get('template').value;
    return (!this.hasTemplate || (link && link.id)) ? null : {noTemplate: true};
  }

  isPanelHasError(controlName: string, validationType: string): boolean {
    const control = this.panelForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }


  submit() {
    if (this.panelForm.valid) {
      this.panelModel.serial = 'HS.P-' + this.panelForm.controls['serial'].value;
      this.panelModel.name = this.panelForm.controls['name'].value;
      this.panelModel.user.id = +this.panelForm.controls['owner'].value.id;
      this.panelModel.group.id = +this.panelForm.controls['group'].value.id;
      this.panelModel.template.id = (this.panelForm.controls['template'].value.id && this.hasTemplate) ?
        +this.panelForm.controls['template'].value.id :
        -1;
      this.panelModel.sendNumber.id = (this.panelForm.controls['sendNumber'].value.id && this.hasTemplate) ?
        +this.panelForm.controls['sendNumber'].value.id :
        -1;
      console.log(this.panelModel);
      this.activeModal.close(this.panelModel);
    }
  }

  loadUserGroups() {
    this.isLoadingContacts = true;
    const owner = this.panelForm.controls['owner'].value;
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
            this.panelForm.controls['group'].setValue({
              name: this.panelModel.group.title,
              id: this.panelModel.group.id
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
    this.loadNumberAndTemplate(this.hasTemplate);
  }

  selectedTemplateChanged(event) {
    const owner = this.panelForm.controls['owner'].value;
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
        this.panelForm.controls['group'].setValue(null);
        console.log(res);
        console.log((typeof (res)));
      });
      ref.result.catch(err => {
      });
    }
  }

  selectedGroupChanged(event) {
    const owner = this.panelForm.controls['owner'].value;
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
        this.panelForm.controls['group'].setValue(null);
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
      this.loadNumberAndTemplate(this.hasTemplate);
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
    const owner = this.panelForm.controls['owner'].value;
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
            this.panelForm.controls['sendNumber'].setValue({
              name: this.panelModel.sendNumber.number + ' ' + (this.panelModel.sendNumber.type === 0 ? 'خدماتی' : 'تبلیغاتی'),
              id: this.panelModel.sendNumber.id
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
                  this.panelForm.controls['template'].setValue({
                    name: this.panelModel.template.title,
                    id: this.panelModel.template.id
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
