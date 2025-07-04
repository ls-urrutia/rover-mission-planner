import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RoverTasksService, RoverTask } from '../../services/rover-tasks';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-rover-timeline',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './rover-timeline.html',
  styleUrls: ['./rover-timeline.scss']
})
export class RoverTimelineComponent implements OnInit, OnChanges {
  @Input() roverName = 'Curiosity';
  @Input() date = '2025-07-04';
  tasks: RoverTask[] = [];
  utilization = 0;

  newTask: Partial<RoverTask> = {
    taskType: 'Drill', // Valor por defecto válido
    latitude: 0,
    longitude: 0,
    startsAt: '',
    durationMinutes: 1,
    status: 'Planned'
  };

  errorMsg = '';

  constructor(private roverTasksService: RoverTasksService) {}

  ngOnInit(): void {
    this.refresh();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['date'] || changes['roverName']) {
      this.refresh();
    }
  }

  refresh() {
    this.roverTasksService.getTasks(this.roverName, this.date).subscribe(tasks => {
      this.tasks = tasks;
      console.log('Tareas recibidas:', tasks); // <-- agrega esto
    });
    this.roverTasksService.getUtilization(this.roverName, this.date).subscribe(data => {
      this.utilization = data.utilization;
    });
  }

  addTask() {
    console.log('Enviando tarea:', this.newTask); 
    if (!this.newTask.startsAt) {
      alert('Debes ingresar una fecha y hora de inicio.');
      return;
    }
    const date = new Date(this.newTask.startsAt);
    if (isNaN(date.getTime())) {
      alert('Fecha y hora inválidas.');
      return;
    }

    const task: RoverTask = {
      ...this.newTask,
      id: crypto.randomUUID(),
      roverName: this.roverName,
      startsAt: new Date(this.newTask.startsAt!).toISOString(),
      status: 'Planned'
    } as RoverTask;

    console.log('Objeto POST:', task);

    this.roverTasksService.createTask(this.roverName, task).subscribe({
      next: () => {
        this.refresh();
        this.newTask = {
          taskType: 'Drill', // Valor por defecto válido
          latitude: 0,
          longitude: 0,
          startsAt: '',
          durationMinutes: 1,
          status: 'Planned'
        };
        this.errorMsg = ''; // limpia el error al éxito
      },
      error: err => {
        // Intenta mostrar los mensajes de FluentValidation
        if (err.error?.errors) {
          // FluentValidation devuelve un objeto { campo: [errores] }
          this.errorMsg = Object.values(err.error.errors).flat().join('\n');
        } else if (err.error) {
          this.errorMsg = typeof err.error === 'string' ? err.error : JSON.stringify(err.error);
        } else {
          this.errorMsg = 'Error al crear la tarea';
        }
      }
    });
  }
}
