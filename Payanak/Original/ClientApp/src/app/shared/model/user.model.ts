import {AccountInfoModel} from './account-info.model';
import {PersonalInfoModel} from './personal-info.model';
import {AdditionalInfoModel} from './additional-info.model';
import {AddressInfoModel} from './address-info.model';

export class UserModel {
  accountInfo: AccountInfoModel;
  additionalInfo: AdditionalInfoModel;
  personalInfo: PersonalInfoModel;
  addressInfo: AddressInfoModel;
  roles: number[] = [];
}
