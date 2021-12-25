import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NgbActiveModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';
import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {NumberModel} from '../../shared/model/number/number.model';
import {ContactModel} from '../../shared/model/contact/contact.model';
import {NumberService} from '../../shared/services/Numbers/number.service';
import {ToastrService} from 'ngx-toastr';
import {map} from 'lodash';

@Component({
  selector: 'app-add-number-modal',
  templateUrl: './add-number-modal.component.html',
  styleUrls: ['./add-number-modal.component.scss'],
  providers: [NumberService]
})
export class AddNumberModalComponent implements OnInit {

  numberForm: FormGroup;
  numberModel: NumberModel;
  contacts: ContactModel[] = [];
  contactsDisplay: any[] = [];
  imageFile: File;
  isContactsLoaded = false;
  isLoadingContacts = false;

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private numberService: NumberService,
              private store: Store<AppState>,
              private router: Router,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    this.numberModel = this.initNumber();
    this.initForm();
  }

  initNumber(): NumberModel {
    const number: NumberModel = {
      owner: null,
      isShared: false,
      password: '',
      username: '',
      number: '',
      createDate: undefined,
      id: undefined,
      isBlocked: false,
      type: 1,
      user: null
    };
    number.isShared = false;
    return number;
  }

  initForm() {
    this.numberForm = this.fb.group({
      number: ['', Validators.compose([
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(100),
        Validators.pattern(/^-?(0|[1-9]\d*)?$/)
      ])
      ],
      username: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      password: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      type: ['', Validators.compose([
        Validators.required,
      ])
      ],
      owner: ['', Validators.compose([])
      ],
    });
    this.selectedShareChanged(false);
  }

  isNumberHasError(controlName: string, validationType: string): boolean {
    const control = this.numberForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }


  submit() {
    if (this.numberForm.valid) {
      this.numberModel.number = this.numberForm.controls['number'].value;
      this.numberModel.username = this.numberForm.controls['username'].value;
      this.numberModel.password = this.numberForm.controls['password'].value;
      this.numberModel.type = +this.numberForm.controls['type'].value;
      this.numberModel.owner = (!this.numberModel.isShared && this.numberForm.controls['owner'].value) ?
        +this.numberForm.controls['owner'].value.id
        : -1;
      console.log(this.numberModel);
      this.activeModal.close(this.numberModel);
    }
  }

  selectedOwnerChanged(event) {
    if (!event) {
      this.numberModel.owner = null;
    } else {
      this.numberModel.owner = event.id;
    }
  }

  selectedShareChanged(event) {
    console.log(this.numberForm);
    this.numberModel.isShared = event;
    if (event) {
      this.numberModel.owner = null;
    }
    if (!event && !this.isContactsLoaded) {
      this.isLoadingContacts = true;
      this.numberService.getAllUsersForAssign().subscribe(
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
  }
}
