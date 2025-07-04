import { Component } from '@angular/core';
import { RoverTimelineComponent } from './components/rover-timeline/rover-timeline';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FormsModule, RoverTimelineComponent],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  roverName = 'Curiosity';
  date = '2025-07-04';
}
