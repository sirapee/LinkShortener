import { TestBed } from '@angular/core/testing';

import { AlltouseService } from './alltouse.service';

describe('AlltouseService', () => {
  let service: AlltouseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AlltouseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
