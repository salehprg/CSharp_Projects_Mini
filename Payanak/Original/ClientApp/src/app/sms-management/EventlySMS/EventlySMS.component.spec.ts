/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { EventlySMSComponent } from './EventlySMS.component'

describe('EventlySMSComponent', () => {
  let component: EventlySMSComponent;
  let fixture: ComponentFixture<EventlySMSComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EventlySMSComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EventlySMSComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
