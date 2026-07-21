import {Injectable} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IntegralResult } from '../models/integral-result';

@Injectable({ providedIn: 'root' })
export class IntegralResultsService {
    constructor(private http: HttpClient) {}

    getResults(): Observable<IntegralResult[]> {
      return this.http.get<IntegralResult[]>('/api/results');
    }
}