import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {AccountActionService} from '../../../shared/services/AccountInfo/AccountAction.service';
import {finalize, takeUntil, tap} from 'rxjs/operators';
import {Logout} from '../../../shared/actions/auth/auth.actions';
import {Store} from '@ngrx/store';
import {AppState} from '../../../shared/reducers';
import {MatDialog} from '@angular/material';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit {

  ChangePassForm: FormGroup;
  error: string[] = [];
  loading: boolean;
  constructor(private router: Router,
              private fb: FormBuilder,
              private store: Store<AppState>,
              private AccActionSvc: AccountActionService,
              private route: ActivatedRoute,
              public toaster: ToastrService) {
    
              }

  ngOnInit() {
    this.ChangePassForm = this.fb.group({
      OldPass: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      NewPass: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      ConfirmPass: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ]
    });
  }

  public onChangePasswordClick() {
    // this.loginForm.reset();
    const controls = this.ChangePassForm.controls;
    /** check form */
    if (this.ChangePassForm.invalid) {
      Object.keys(controls).forEach(controlName =>
        controls[controlName].markAsTouched()
      );
      return;
    }
    this.loading = true;
    const authData = {
      OldPass: controls['OldPass'].value,
      NewPass: controls['NewPass'].value,
      ConfirmPass: controls['ConfirmPass'].value
    };
    this.AccActionSvc
      .ChangePassword(authData.OldPass, authData.NewPass)
      .pipe(
        tap(
          res => {
            if (res && res.status && res.status.length === 1 && res.status[0].status === 200) {
              this.store.dispatch(new Logout());
            } else {
              for (const itm of res.status) {
                this.toaster.error(res.status[0].message, 'خطا');
              }
            }
          },
          err => {
            this.toaster.error('خطا در عملیات.', 'خطا');
          }),
        // takeUntil(this.unsubscribe),
        finalize(() => {
          this.loading = false;
          // this.cdr.markForCheck();
        })
      )
      .subscribe(
      );
  }
}
