import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IntegralResult } from '../models/integral-result';

export interface ResultsFilter {
  fileName?: string;
  startTimeFrom?: string;
  startTimeTo?: string;
  avgIndicatorFrom?: number;
  avgIndicatorTo?: number;
  avgExecutionTimeFrom?: number;
  avgExecutionTimeTo?: number;
}

@Injectable({ providedIn: 'root' })
export class IntegralResultsService {
    constructor(private http: HttpClient) {}

    getResults(filter?: ResultsFilter): Observable<IntegralResult[]> {
        let params: any = {};
        
        if (filter) {
            if (filter.fileName) params.fileName = filter.fileName;
            if (filter.startTimeFrom) params.startTimeFrom = filter.startTimeFrom;
            if (filter.startTimeTo) params.startTimeTo = filter.startTimeTo;
            if (filter.avgIndicatorFrom !== undefined && filter.avgIndicatorFrom !== null) 
                params.avgIndicatorFrom = filter.avgIndicatorFrom;
            if (filter.avgIndicatorTo !== undefined && filter.avgIndicatorTo !== null) 
                params.avgIndicatorTo = filter.avgIndicatorTo;
            if (filter.avgExecutionTimeFrom !== undefined && filter.avgExecutionTimeFrom !== null) 
                params.avgExecutionTimeFrom = filter.avgExecutionTimeFrom;
            if (filter.avgExecutionTimeTo !== undefined && filter.avgExecutionTimeTo !== null) 
                params.avgExecutionTimeTo = filter.avgExecutionTimeTo;
        }

        return this.http.get<IntegralResult[]>('/api/results', { params });
    }
}