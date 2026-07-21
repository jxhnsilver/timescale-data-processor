import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { IntegralResultsService, ResultsFilter } from '../../services/integral-results';
import { IntegralResult } from '../../models/integral-result';

@Component({
  selector: 'app-integral-results',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './integral-results.html',
})
export class IntegralResults {
  constructor(private service: IntegralResultsService) {}

  results = signal<IntegralResult[]>([]);
  loading = signal<boolean>(false);
  
  filter: ResultsFilter = {
    fileName: '',
    startTimeFrom: '',
    startTimeTo: '',
    avgIndicatorFrom: undefined,
    avgIndicatorTo: undefined,
    avgExecutionTimeFrom: undefined,
    avgExecutionTimeTo: undefined
  };

  fetchResults(): void {
    this.loading.set(true);
    
    const cleanFilter: ResultsFilter = {};
    
    if (this.filter.fileName?.trim()) cleanFilter.fileName = this.filter.fileName.trim();
    if (this.filter.startTimeFrom) cleanFilter.startTimeFrom = this.filter.startTimeFrom;
    if (this.filter.startTimeTo) cleanFilter.startTimeTo = this.filter.startTimeTo;
    if (this.filter.avgIndicatorFrom !== undefined && this.filter.avgIndicatorFrom !== null) 
        cleanFilter.avgIndicatorFrom = this.filter.avgIndicatorFrom;
    if (this.filter.avgIndicatorTo !== undefined && this.filter.avgIndicatorTo !== null) 
        cleanFilter.avgIndicatorTo = this.filter.avgIndicatorTo;
    if (this.filter.avgExecutionTimeFrom !== undefined && this.filter.avgExecutionTimeFrom !== null) 
        cleanFilter.avgExecutionTimeFrom = this.filter.avgExecutionTimeFrom;
    if (this.filter.avgExecutionTimeTo !== undefined && this.filter.avgExecutionTimeTo !== null) 
        cleanFilter.avgExecutionTimeTo = this.filter.avgExecutionTimeTo;

    this.service.getResults(Object.keys(cleanFilter).length > 0 ? cleanFilter : undefined).subscribe({
      next: (data) => {
        this.results.set(data);
        this.loading.set(false);
      },
      error: (err) => {
        console.error('Ошибка загрузки:', err);
        this.loading.set(false);
      }
    });
  }

  resetFilters(): void {
    this.filter = {
      fileName: '',
      startTimeFrom: '',
      startTimeTo: '',
      avgIndicatorFrom: undefined,
      avgIndicatorTo: undefined,
      avgExecutionTimeFrom: undefined,
      avgExecutionTimeTo: undefined
    };
  }
}