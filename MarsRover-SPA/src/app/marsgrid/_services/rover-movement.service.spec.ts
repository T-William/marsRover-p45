/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { RoverMovementService } from './rover-movement.service';

describe('Service: RoverMovement', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [RoverMovementService]
    });
  });

  it('should ...', inject([RoverMovementService], (service: RoverMovementService) => {
    expect(service).toBeTruthy();
  }));
});
