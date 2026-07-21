import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IntegralResultsService } from '../../services/integral-results';
import { IntegralResult } from '../../models/integral-result';

@Component({
  selector: 'app-integral-results',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './integral-results.html',
})
export class IntegralResults {
  results = signal<IntegralResult[]>([]);

  constructor(private service: IntegralResultsService) {}

  fetchResults(): void {
    this.service.getResults().subscribe((data) => {
      this.results.set(data);
    });
  }
}