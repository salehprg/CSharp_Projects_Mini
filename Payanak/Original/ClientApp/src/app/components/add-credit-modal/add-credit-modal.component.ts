import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {NgbActiveModal, NgbModal} from '@ng-bootstrap/ng-bootstrap';
import {MatSnackBar} from '@angular/material';
import {SmsService} from '../../shared/services/sms/sms.service';
import {Store} from '@ngrx/store';
import {AppState} from '../../shared/reducers';
import {Router} from '@angular/router';
import {ToastrService} from 'ngx-toastr';
import {CreditModel} from '../../shared/model/contact/credit.model';
import {UserService} from '../../shared/services/user/user.service';

@Component({
  selector: 'app-add-credit-modal',
  templateUrl: './add-credit-modal.component.html',
  styleUrls: ['./add-credit-modal.component.scss']
})
export class AddCreditModalComponent implements OnInit {

  creditFrom: FormGroup;
  creditModel: CreditModel;
  isLoading = false;
  userId: number;
  height = window.screen.availHeight - 320;
  header = 'افزودن اعتبار';

  constructor(public activeModal: NgbActiveModal,
              private fb: FormBuilder,
              private snackbar: MatSnackBar,
              private smsService: SmsService,
              private userService: UserService,
              private modalService: NgbModal,
              private store: Store<AppState>,
              private router: Router,
              public toaster: ToastrService) {
  }

  ngOnInit() {
    this.creditModel = this.initCredit();
    this.initForm();
    this.loadUserCredit();
  }

  initCredit(): CreditModel {
    const credit: CreditModel = {
      credit: 0,
      discount: 0
    };
    return credit;
  }

  initForm() {
    this.creditFrom = this.fb.group({
      credit: [this.creditModel.credit, Validators.compose([])
      ],
      creditAdd: ['0', Validators.compose([
        Validators.required,
        Validators.pattern(/^-?(0|[1-9]\d*)?$/)
      ])
      ],
      discount: [this.creditModel.discount, Validators.compose([
        Validators.required,
        Validators.min(0),
        Validators.max(100)
      ])
      ],
    });
  }

  isCreditHasError(controlName: string, validationType: string): boolean {
    const control = this.creditFrom.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }


  submit() {
    if (this.creditFrom.valid) {
      this.creditModel.credit = +this.creditFrom.controls['creditAdd'].value;
      this.creditModel.discount = +this.creditFrom.controls['discount'].value;
      this.activeModal.close(this.creditModel);
    }
  }

  loadUserCredit() {
    this.isLoading = true;
    this.userService.getUserCredit(this.userId).subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.creditModel = res.Result;
          this.initForm();
          this.isLoading = false;
        } else {
          this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
        }
        this.isLoading = false;
      },
      err => {
        this.isLoading = false;
        this.toaster.error('خطا در بارگذاری اطلاعات.', 'خطا');
      }
    );
  }


}
