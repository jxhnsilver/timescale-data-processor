import {Injectable} from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { ValueRecord } from '../models/value-record';

@Injectable({ providedIn: 'root' })
export class ValueRecordsService {
    constructor(private http: HttpClient) {}

    importValues(file: File): Observable<string> {
        const formData = new FormData();
        formData.append('file', file);

        return this.http.post('api/values/import', formData, { responseType: 'text' as const }).pipe(
            catchError(this.handleError)
        );
    }

    getLatestValues(): Observable<ValueRecord[]> {
      return this.http.get<ValueRecord[]>('/api/values/latest');
    }

    private handleError(error: HttpErrorResponse): Observable<never> {
        const errorObj = JSON.parse(error.error);
        const message = errorObj.detail;
        
        return throwError(() => new Error(message));
    }
}