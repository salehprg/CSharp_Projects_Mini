import {Component, OnInit} from '@angular/core';
import {SmsService} from '../../../shared/services/sms/sms.service';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-add-credit',
  templateUrl: './add-credit.component.html',
  styleUrls: ['./add-credit.component.scss']
})
export class AddCreditComponent implements OnInit {
  cardNumber: string;
  cardId: string;
  holder: string;

  constructor(private smsService: SmsService,
              public toaster: ToastrService) {

  }

  ngOnInit() {
    this.smsService.getBankInfo().subscribe(
      res => {
        if (res && res.Status && res.Status.length === 1 && res.Status[0].status === 200) {
          this.cardNumber = res.Result.cardNumber;
          this.cardId = res.Result.cardId;
          this.holder = res.Result.owner;

        } else {
          for (const itm of res.Status) {
            this.toaster.error(res.Status[0].message, 'خطا');
          }
        }
      },
      err => {
        this.toaster.error('خطا در عملیات.', 'خطا');
      }
    );
  }

}
