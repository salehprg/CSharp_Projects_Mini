import {Component, OnInit} from '@angular/core';
import {select, Store} from '@ngrx/store';
import {AppState} from '../../../shared/reducers';
import {UserModel} from '../../../shared/model/user.model';
import {currentUser} from '../../../shared/selector/auth/auth.selector';
import {take} from 'rxjs/operators';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {AccountInfoModel} from '../../../shared/model/account-info.model';
import {PersonalInfoModel} from '../../../shared/model/personal-info.model';
import {AdditionalInfoModel} from '../../../shared/model/additional-info.model';
import {AddressInfoModel} from '../../../shared/model/address-info.model';

@Component({
  selector: 'app-user-profile-page',
  templateUrl: './user-profile-page.component.html',
  styleUrls: ['./user-profile-page.component.scss']
})

export class UserProfilePageComponent implements OnInit {

  // Variable Declaration
  currentPage = 'AccountInfo';
  user: UserModel;
  imageFile: File;
  accountInfoForm: FormGroup;
  personalInfoForm: FormGroup;
  additionalInfoForm: FormGroup;
  address: string;
  isEditMode = false;

  constructor(private store: Store<AppState>,
              private fb: FormBuilder) {

  }

  ngOnInit() {
    this.user = this.initUser();
    this.initForm();
    this.store.pipe(
      select(currentUser),
      take(1)).subscribe(res => {
      if (res) {
        this.user = res;
      }
    });
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

  showPage(page: string) {
    this.currentPage = page;
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
}
