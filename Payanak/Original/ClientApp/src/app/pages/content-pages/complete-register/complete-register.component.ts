import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, NgForm, Validators} from '@angular/forms';
import {UserModel} from '../../../shared/model/user.model';
import {ActivatedRoute, Router} from '@angular/router';
import {AuthService} from '../../../shared/services/auth/auth.service';
import {ToastrService} from 'ngx-toastr';
import {AccountInfoModel} from '../../../shared/model/account-info.model';
import {PersonalInfoModel} from '../../../shared/model/personal-info.model';
import {AdditionalInfoModel} from '../../../shared/model/additional-info.model';
import {AddressInfoModel} from '../../../shared/model/address-info.model';
import {ResponseModel} from '../../../shared/model/Response/responseModel';
import {UserService} from '../../../shared/services/user/user.service';
import * as _moment from 'jalali-moment';
const moment = _moment;
@Component({
  selector: 'app-complete-register',
  templateUrl: './complete-register.component.html',
  styleUrls: ['./complete-register.component.scss']
})
export class CompleteRegisterComponent implements OnInit {

  @ViewChild('f', {static: false}) registerForm: NgForm;
  user: UserModel;
  imageFile: File;
  accountInfoForm: FormGroup;
  personalInfoForm: FormGroup;
  additionalInfoForm: FormGroup;
  address: string;
  isSucceeded = false;
  isEdit = false;

  //  On submit click, reset field value
  constructor(private router: Router,
              private activeRoute: ActivatedRoute,
              private fb: FormBuilder,
              private userService: UserService,
              private  authService: AuthService,
              public toastr: ToastrService) {


  }

  onSubmit() {
    this.registerForm.reset();
  }

  ngOnInit(): void {
    this.user = this.initUser();
    this.initForm();
    this.activeRoute.params.subscribe(
      res => {
        if (!res || !res.guid) {
          this.router.navigate(['/pages/login']);
        } else {
          this.userService.getUserCompletionForm(res.guid).subscribe(
            (res: ResponseModel) => {
              if (res.Status.length === 1 && res.Status[0].status === 200) {
                this.user = res.Result;
                this.initForm();
              } else {
                for (const itm of res.Status) {
                  this.toastr.error(res.Status[0].message, 'خطا');
                }
                this.router.navigate(['/pages/login']);
              }
              console.log(res);
            },
            err => {
              this.router.navigate(['/pages/login']);
            }
          );
        }
        console.log(res);
      }
    );
  }

  initUser(): UserModel {
    const user = new UserModel();
    user.accountInfo = new AccountInfoModel();
    user.accountInfo.picture = 'assets/img/portrait/avatars/avatar-08.png';
    user.personalInfo = new PersonalInfoModel();
    user.additionalInfo = new AdditionalInfoModel();
    user.addressInfo = new AddressInfoModel();
    return user;
  }

  initForm() {
    this.accountInfoForm = this.fb.group({
      username: [this.user.accountInfo.username, Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      password: ['', !this.isEdit ? Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ]) : null
      ],
      confirmPass: ['', Validators.compose([
        !this.isEdit ? Validators.required : null,
        this.checkPasswords,
      ])
      ],
      email: [this.user.accountInfo.email, Validators.compose([
        Validators.email,
        Validators.maxLength(200)
      ])
      ],
      mobile: [this.user.accountInfo.mobilePhone, Validators.compose([
        Validators.required,
        Validators.minLength(7),
        Validators.maxLength(15)
      ])
      ],
      homePhone: [this.user.accountInfo.homePhone, Validators.compose([
        Validators.minLength(7),
        Validators.maxLength(15)
      ])
      ],
      businessPhone: [this.user.accountInfo.businessPhone, Validators.compose([
        Validators.minLength(7),
        Validators.maxLength(15)
      ])
      ]
    });
    this.personalInfoForm = this.fb.group({
      fname: [(this.user.personalInfo ?
        this.user.personalInfo.fName :
        '')
        , Validators.compose([
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100)
        ])
      ],
      lname: [(this.user.personalInfo ?
        this.user.personalInfo.lName :
        '')
        , Validators.compose([
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100)
        ])
      ],
      nickName: [(this.user.personalInfo ?
        this.user.personalInfo.nickName :
        '')
        , Validators.compose([
          Validators.minLength(2),
          Validators.maxLength(100)
        ])
      ],
      gender: [(this.user.personalInfo ?
        this.user.personalInfo.gender :
        '')
        , Validators.compose([
          Validators.required
        ])
      ],
      birthday: [(this.user.personalInfo ?
        this.ticksToDate(this.user.personalInfo.birthday) :
        '')
        , Validators.compose([
          Validators.required
        ])
      ],
      nationalCode: [(this.user.personalInfo ?
        this.user.personalInfo.nationalCode :
        '')
        , Validators.compose([
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(10)
        ])
      ]
    });
    this.additionalInfoForm = this.fb.group({
      specialDay: [(this.user.additionalInfo ?
        this.ticksToDate(this.user.additionalInfo.specialDay) :
        '')
        , Validators.compose([])
      ],
      instagram: [(this.user.additionalInfo ?
        this.user.additionalInfo.instagram :
        '')
        , Validators.compose([
          this.checkIsInstagram
        ])
      ],
      telegram: [(this.user.additionalInfo ?
        this.user.additionalInfo.telegram :
        '')
        , Validators.compose([
          this.checkIsTelegram
        ])
      ]
    });
  }

  checkIsInstagram(group: FormGroup) {
    if (!group.parent || !group.parent.get('instagram')) {
      return null;
    }
    const link = group.parent.get('instagram').value;
    const regex = RegExp('instagram.com');
    return (regex.test(link) || link === '') ? null : {notInstagram: true};
  }

  checkIsTelegram(group: FormGroup) {
    if (!group.parent || !group.parent.get('telegram')) {
      return null;
    }
    const link = group.parent.get('telegram').value;
    const regex = RegExp('t.me');
    return (regex.test(link) || link === '') ? null : {notTelegram: true};
  }

  checkPasswords(group: FormGroup) {
    if (!group.parent || (!group.parent.get('password') || !group.parent.get('confirmPass'))) {
      return null;
    }
    const pass = group.parent.get('password').value;
    const confirmPass = group.parent.get('confirmPass').value;

    return pass === confirmPass ? null : {notSame: true};
  }

  imageSelected(event) {
    if (!event || !event.target || !event.target.files || event.target.files.length === 0) {
      return;
    }
    this.imageFile = event.target.files[0];
    const fr = new FileReader();
    fr.onload = (ev => {
      this.user.accountInfo.picture = fr.result as string;
    });
    fr.readAsDataURL(this.imageFile);
  }

  isAccountInfoHasError(controlName: string, validationType: string): boolean {
    const control = this.accountInfoForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  isPersonalInfoHasError(controlName: string, validationType: string): boolean {
    const control = this.personalInfoForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  isAdditionalInfoHasError(controlName: string, validationType: string): boolean {
    const control = this.additionalInfoForm.controls[controlName];
    if (!control) {
      return false;
    }
    const result = control.hasError(validationType) && (control.dirty || control.touched);
    return result;
  }

  AccountInfoExit() {
    this.user.accountInfo = {
      id: undefined,
      email: this.accountInfoForm.controls['email'].value,
      businessPhone: this.accountInfoForm.controls['businessPhone'].value,
      homePhone: this.accountInfoForm.controls['homePhone'].value,
      mobilePhone: this.accountInfoForm.controls['mobile'].value,
      password: this.accountInfoForm.controls['password'].value,
      username: this.accountInfoForm.controls['username'].value,
      picture: this.user.accountInfo.picture,
      createDate: 0,
      lastLogin: 0,
      formId: undefined,
    };
  }

  PersonalInfoExit() {
    this.user.personalInfo = {
      id: undefined,
      nationalCode: this.personalInfoForm.controls['nationalCode'].value,
      gender: +this.personalInfoForm.controls['gender'].value,
      birthday:
        ((this.personalInfoForm.controls['birthday'].value).toDate().getTime() * 10000) + 621355968000000000,
      nickName: this.personalInfoForm.controls['nickName'].value,
      lName: this.personalInfoForm.controls['lname'].value,
      fName: this.personalInfoForm.controls['fname'].value
    };
  }

  AdditionalInfoExit() {
    this.user.additionalInfo = {
      id: undefined,
      telegram: this.additionalInfoForm.controls['telegram'].value,
      instagram: this.additionalInfoForm.controls['instagram'].value,
      specialDay:
        ((this.additionalInfoForm.controls['specialDay'].value) &&
        (this.additionalInfoForm.controls['specialDay'].value).toDate ?
          ((this.additionalInfoForm.controls['specialDay'].value).toDate().getTime() * 10000) + 621355968000000000 : 0)
    };
  }

  addressChanged(event) {
    this.user.addressInfo = {
      id: undefined,
      address: event.address,
      longitude: event.longitude.toString(),
      latitude: event.latitude.toString(),
      region: event.region,
      city: event.city
    };
    this.address = event.region + ' - ' + event.city + ' - ' + event.address;
  }

  sendData() {
    this.authService.register(this.user).subscribe(
      (res: ResponseModel) => {
        if (res.Status.length === 1 && res.Status[0].status === 200) {
          this.isSucceeded = true;
          this.toastr.success('ثبت اطلاعات با موفقیت انجام پذیرفت.', res.Status[0].message);
          this.router.navigate(['/pages/login']);
        } else {
          this.isSucceeded = false;
          for (const itm of res.Status) {
            this.toastr.error(res.Status[0].message, 'خطا');
          }
        }
        console.log(res);
      },
      err => {
        this.toastr.error('لطفا مجددا تلاش کنید.', 'خطا');
      }
    );
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

}
