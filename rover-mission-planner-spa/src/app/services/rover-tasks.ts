import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RoverTask {
  id: string;
  roverName: string;
  taskType: string;
  latitude: number;
  longitude: number;
  startsAt: string;
  durationMinutes: number;
  status: string;
}

@Injectable({
  providedIn: 'root'
})
export class RoverTasksService {
  private apiUrl = 'http://localhost:5098/rovers'; 

  constructor(private http: HttpClient) {}

  getTasks(roverName: string, date: string): Observable<RoverTask[]> {
    return this.http.get<RoverTask[]>(`${this.apiUrl}/${roverName}/tasks?date=${date}`);
  }

  createTask(roverName: string, task: RoverTask): Observable<RoverTask> {
    return this.http.post<RoverTask>(`${this.apiUrl}/${roverName}/tasks`, task);
  }

  getUtilization(roverName: string, date: string): Observable<{ utilization: number }> {
    return this.http.get<{ utilization: number }>(
      `${this.apiUrl}/${roverName}/utilization?date=${date}`
    );
  }
}
