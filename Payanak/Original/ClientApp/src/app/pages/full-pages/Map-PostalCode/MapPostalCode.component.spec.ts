/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { MapPostalCodeComponent } from './MapPostalCode.component';

describe('MapPostalCodeComponent', () => {
  let component: MapPostalCodeComponent;
  let fixture: ComponentFixture<MapPostalCodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MapPostalCodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MapPostalCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
