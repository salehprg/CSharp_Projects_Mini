import {Component, OnInit, ViewChild} from '@angular/core';
import {FormBuilder, FormGroup, NgForm, Validators} from '@angular/forms';
import {UserModel} from '../../../shared/model/user.model';
import {AccountInfoModel} from '../../../shared/model/account-info.model';
import {PersonalInfoModel} from '../../../shared/model/personal-info.model';
import {AdditionalInfoModel} from '../../../shared/model/additional-info.model';
import {AddressInfoModel} from '../../../shared/model/address-info.model';
import {AuthService} from '../../../shared/services/auth/auth.service';
import {ResponseModel} from '../../../shared/model/Response/responseModel';
import {ToastrService} from 'ngx-toastr';
import {Router} from '@angular/router';

@Component({
  selector: 'app-register-page',
  templateUrl: './register-page.component.html',
  styleUrls: ['./register-page.component.scss']
})

export class RegisterPageComponent implements OnInit {
  @ViewChild('f', {static: false}) registerForm: NgForm;
  user: UserModel;
  imageFile: File;
  accountInfoForm: FormGroup;
  personalInfoForm: FormGroup;
  additionalInfoForm: FormGroup;
  address: string;
  isSucceeded = false;

  //  On submit click, reset field value
  constructor(private router: Router,
              private fb: FormBuilder,
              private  authService: AuthService,
              public toastr: ToastrService) {

  }

  onSubmit() {
    this.registerForm.reset();
  }

  ngOnInit(): void {
    this.user = this.initUser();
    this.initForm();
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
      confirmPass: ['', Validators.compose([
        Validators.required,
        this.checkPasswords,
      ])
      ],
      email: ['', Validators.compose([
        Validators.email,
        Validators.maxLength(200)
      ])
      ],
      mobile: ['', Validators.compose([
        Validators.required,
        Validators.minLength(7),
        Validators.maxLength(15)
      ])
      ],
      homePhone: ['', Validators.compose([
        Validators.minLength(7),
        Validators.maxLength(15)
      ])
      ],
      businessPhone: ['', Validators.compose([
        Validators.minLength(7),
        Validators.maxLength(15)
      ])
      ]
    });
    this.personalInfoForm = this.fb.group({
      fname: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      lname: ['', Validators.compose([
        Validators.required,
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      nickName: ['', Validators.compose([
        Validators.minLength(2),
        Validators.maxLength(100)
      ])
      ],
      gender: ['', Validators.compose([
        Validators.required
      ])
      ],
      birthday: ['', Validators.compose([
        Validators.required
      ])
      ],
      nationalCode: ['', Validators.compose([
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(10)
      ])
      ]
    });
    this.additionalInfoForm = this.fb.group({
      specialDay: ['', Validators.compose([])
      ],
      instagram: ['', Validators.compose([
        this.checkIsInstagram
      ])
      ],
      telegram: ['', Validators.compose([
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
      formId: undefined
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
        console.log(err)
        this.toastr.error('لطفا مجددا تلاش کنید.', 'خطا');
      }
    );
  }
}
