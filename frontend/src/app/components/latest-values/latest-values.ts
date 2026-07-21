import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValueRecordsService } from '../../services/value-records';
import { ValueRecord } from '../../models/value-record';

@Component({
  selector: 'app-latest-values',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './latest-values.html',
})
export class LatestValues {
  values = signal<ValueRecord[]>([]);

  constructor(private service: ValueRecordsService) {}

  fetchValues(): void {
    this.service.getLatestValues().subscribe((data) => {
      this.values.set(data);
    });
  }
}