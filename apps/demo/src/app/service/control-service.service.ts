import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

interface Position {
  xAxis: number;
  yAxis: number;
}
interface PositionTemplateDTO {
  currentPosition: Position;
  template: string;
  obstacles: Position[];
  startMaze: Position;
  exitMaze: Position;
}

@Injectable({
  providedIn: 'root',
})
export class ControlServiceService {
  constructor(private http: HttpClient) {}
  private Url_Server = 'http://localhost:5000';
  getControls(): Observable<string[]> {
    return this.http.get<string[]>(this.Url_Server + '/maze').pipe(
      tap((_) => console.log('data')),
      catchError(this.handleError<string[]>('GetNextAvailableMoves', []))
    );
  }

  move(data:any): Observable<any> {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');
    return this.http
      .post<any>(this.Url_Server + '/moveposition', data,{headers: headers})
      .pipe(catchError(this.handleError<any>('moveposition', null)));
  }

  uploadMaze(data: any): Observable<any> {
    return this.http
      .post<any>(this.Url_Server + '/uploadMaze', data)
      .pipe(catchError(this.handleError<any>('uploadMaze', null)));
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
