import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LikersAndlikeesComponent } from './likers-andlikees.component';

describe('LikersAndlikeesComponent', () => {
  let component: LikersAndlikeesComponent;
  let fixture: ComponentFixture<LikersAndlikeesComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [LikersAndlikeesComponent]
    });
    fixture = TestBed.createComponent(LikersAndlikeesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
