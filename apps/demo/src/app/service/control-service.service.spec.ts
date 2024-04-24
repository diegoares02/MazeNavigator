import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { ControlServiceService } from './control-service.service';

describe('ControlServiceService', () => {
  let service: ControlServiceService;
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports:[HttpClientTestingModule],
      providers:[ControlServiceService]
    });
    service = TestBed.inject(ControlServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
