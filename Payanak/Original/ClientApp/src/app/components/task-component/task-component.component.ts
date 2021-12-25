import {ChangeDetectorRef, Component, EventEmitter, OnDestroy, OnInit, Output} from '@angular/core';
import {TaskService} from '../../shared/services/task.service';
import {interval, Subscription} from 'rxjs';
import {ToastrService} from 'ngx-toastr';

@Component({
  selector: 'app-task-component',
  templateUrl: './task-component.component.html',
  styleUrls: ['./task-component.component.scss']
})
export class TaskComponentComponent implements OnInit, OnDestroy {
  unsubscription: Subscription[] = [];
  tasks: any[] = [];

  @Output() ItemChanged: EventEmitter<any> = new EventEmitter();

  constructor(private taskService: TaskService,
              public toaster: ToastrService,
              private cdr: ChangeDetectorRef) {
  }

  ngOnDestroy(): void {
    for (const itm of this.unsubscription) {
      itm.unsubscribe();
    }
  }

  ngOnInit() {
    const tim = interval(1000);
    const subs = tim.subscribe(
      res => {
        for (const itm of this.tasks) {
          if (itm.completed) {
            continue;
          }
          this.taskService.getTaskResult(itm.guid).subscribe(
            res2 => {
              if (res2 && res2.Status && res2.Status.length === 1 && res2.Status[0].status === 200) {
                if (res2.Result.status && res2.Result.status.length === 1 && res2.Result.status[0].status === 200) {
                  itm.messages = [];
                  itm.messages.push({
                    class: 'bg-success',
                    body: 'در حال انجام...',
                    header: res2.Result.header
                  });
                  itm.percent = res2.Result.percent;
                  if (res2.Result.percent && res2.Result.percent === 100) {
                    this.taskService.RemoveTaskId(itm);
                    itm.messages = [];
                    for (const itm2 of res2.Result.status) {
                      itm.messages.push({
                        class: 'bg-success',
                        body: itm2.message,
                        header: res2.Result.header
                      });
                    }
                    itm.completed = true;
                  }
                } else {
                  itm.messages = [];
                  for (const itm2 of res2.Result.status) {
                    itm.messages.push({
                      class: 'bg-danger',
                      body: itm2.message,
                      header: res2.Result.header
                    });
                  }
                  if (itm.messages.length > 0) {
                    itm.completed = true;
                  }
                }

              } else if (res2 && res2.Status && !res2.Result) {
                itm.messages = [];
                itm.messages.push({
                  class: 'bg-danger',
                  body: 'خطای نامشخص',
                  header: ''
                });
              } else {
                itm.messages = [];
                for (const itm2 of res2.Status) {
                  itm.messages.push({
                    class: 'bg-danger',
                    body: itm2.message,
                    header: res2.Result.header
                  });
                }
                if (itm.messages.length > 0) {
                  itm.completed = true;
                }
              }
              this.cdr.markForCheck();
            },
            error => {
              let header = '';
              if (itm.messages && itm.messages.length > 0) {
                header = itm.messages[0].header;
              }
              itm.messages = [];
              itm.messages.push({
                class: 'bg-danger',
                body: 'خطا در بارگذاری داده.',
                header
              });
              if (itm.messages.length > 0) {
                itm.completed = true;
              }
              this.taskService.RemoveTaskId(itm);
              this.cdr.markForCheck();
            }
          );
        }
      }
    );
    this.unsubscription.push(subs);
    const tskAded = this.taskService.TaskAdded.subscribe(
      res => {
        this.tasks.push({
          guid: res, messages: [{
            class: 'bg-success',
            body: 'در حال انجام...',
            header: ''
          }], completed: false, percent: 0
        });
        this.ItemChanged.emit(this.tasks.length);
        this.cdr.markForCheck();
      }
    );
    this.unsubscription.push(tskAded);
    const cls = this.taskService.TaskReset.subscribe(
      res => {
        if (res) {
          this.tasks = [];
          this.ItemChanged.emit(this.tasks.length);
        }
      }
    );
    this.unsubscription.push(cls);
    //
    // this.taskService.Tasks.subscribe(
    //   res => {
    //     const tim = timer(1000, 1000);
    //     this.timers.push(tim);
    //     const timSubs = tim.subscribe(res1 => {
    //       this.taskService.getTaskResult(res).subscribe(
    //         res2 => {
    //           if (res2 && res2.Status && res2.Status.length === 1 && res2.Status[0].status === 200) {
    //           } else {
    //             for (const itm of res2.Status) {
    //               this.toaster.error(itm.message, 'خطا');
    //             }
    //           }
    //         },
    //         error => {
    //           this.taskService.RemoveTaskId(res);
    //           timSubs.unsubscribe();
    //         }
    //       );
    //     });
    //
    //   }
    // );
  }

}
