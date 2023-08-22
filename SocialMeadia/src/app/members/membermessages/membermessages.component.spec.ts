import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembermessagesComponent } from './membermessages.component';

describe('MembermessagesComponent', () => {
  let component: MembermessagesComponent;
  let fixture: ComponentFixture<MembermessagesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MembermessagesComponent]
    });
    fixture = TestBed.createComponent(MembermessagesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
