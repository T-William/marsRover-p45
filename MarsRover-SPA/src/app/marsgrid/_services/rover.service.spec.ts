/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RoverService } from './rover.service';

describe('Service: Rover', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RoverService]
    });
  });

  it('should ...', inject([RoverService], (service: RoverService) => {
    expect(service).toBeTruthy();
  }));
});
