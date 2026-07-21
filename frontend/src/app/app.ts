import { Component } from '@angular/core';
import { IntegralResults } from './components/integral-results/integral-results';
import { LatestValues } from './components/latest-values/latest-values';
import { ImportValues } from './components/import-values/import-values';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [IntegralResults, LatestValues, ImportValues],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {

}