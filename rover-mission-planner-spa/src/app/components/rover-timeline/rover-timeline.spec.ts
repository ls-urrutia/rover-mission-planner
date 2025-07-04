import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoverTimeline } from './rover-timeline';

describe('RoverTimeline', () => {
  let component: RoverTimeline;
  let fixture: ComponentFixture<RoverTimeline>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RoverTimeline]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RoverTimeline);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
