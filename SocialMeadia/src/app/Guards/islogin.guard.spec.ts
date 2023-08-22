import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { isloginGuard } from './islogin.guard';

describe('isloginGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => isloginGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
