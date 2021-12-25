import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthService} from '../../../shared/services/auth/auth.service';
import {finalize, takeUntil, tap} from 'rxjs/operators';
import {Login} from '../../../shared/actions/auth/auth.actions';
import {Store} from '@ngrx/store';
import {AppState} from '../../../shared/reducers';
import {MatDialog} from '@angular/material';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss']
})

export class LoginPageComponent implements OnInit {
  loginForm: FormGroup;
  error: string[] = [];
  loading: boolean;
  constructor(private router: Router,
              private fb: FormBuilder,
              private store: Store<AppState>,
              private authService: AuthService,
              private route: ActivatedRoute,
              public toaster: ToastrService) {
  }

  /**
   * On init
   */
  ngOnInit(): void {
    this.initLoginForm();
  }

  /**
   * Form initalization
   * Default params, validators
   */
  initLoginForm() {
    // demo message to show
    // if (!this.authNoticeService.onNoticeChanged$.getValue()) {
    //   const initialNotice = this.translate.instant('AUTH.GENERAL.TEXT');
    //   this.authNoticeService.setNotice(initialNotice, 'brand');
    // }

    this.loginForm = this.fb.group({
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
      ]
    });
  }

  // On submit button click
  onSubmit() {
    // this.loginForm.reset();
    const controls = this.loginForm.controls;
    /** check form */
    if (this.loginForm.invalid) {
      Object.keys(controls).forEach(controlName =>
        controls[controlName].markAsTouched()
      );
      return;
    }
    this.loading = true;
    const authData = {
      username: controls['username'].value,
      password: controls['password'].value
    };
    this.authService
      .login(authData.username, authData.password)
      .pipe(
        tap(
          res => {
            console.log(res.Status[0].status);
            if (res && res.Status.length == 1 && res.Status[0].status === 200) {
              this.store.dispatch(new Login({ authToken: res.Result }));
              this.router.navigate(['/dashboard/dashboard1']);
            } else {
                this.toaster.error("ورود نا موفق", 'خطا');
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

  // On Forgot password link click
  onForgotPassword() {
    this.router.navigate(['forgotpassword'], {relativeTo: this.route.parent});
  }

  // On registration link click
  onRegister() {
    this.router.navigate(['register'], {relativeTo: this.route.parent});
  }

  isControlHasError(controlName: string, validationType: string): boolean {
    const control = this.loginForm.controls[controlName];
    if (!control) {
      return false;
    }
    // for (const err in control.errors) {
    //   if (err === validationType && (control.dirty || control.touched)) {
    //     return true;
    //   }
    // }
    // return false;
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  hasError() {
    this.error = this.authService.getErrors(this.loginForm);
    return (this.error && this.error.length > 0);
  }
}
