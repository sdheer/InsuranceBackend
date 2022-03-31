import { TestBed } from '@angular/core/testing';

import { InsuranceCalcServiceService } from './insurance-calc-service.service';

describe('InsuranceCalcServiceService', () => {
  let service: InsuranceCalcServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(InsuranceCalcServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
