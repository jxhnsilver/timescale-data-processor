import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ValueRecordsService } from '../../services/value-records';

@Component({
  selector: 'app-import-values',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './import-values.html',
})
export class ImportValues {
    constructor(private service: ValueRecordsService) {}
    
    isLoading = signal<boolean>(false);
    successMessage = signal<string | null>(null);
    errorMessage = signal<string | null>(null);
    selectedFile = signal<File | null>(null);
    
  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.selectedFile.set(input.files[0]);
      this.successMessage.set(null);
      this.errorMessage.set(null);
    }
  }

  uploadFile(): void {
    const file = this.selectedFile();
    
    if (!file) {
      this.errorMessage.set('Файл не выбран');
      return;
    }

    this.isLoading.set(true);
    this.successMessage.set(null);
    this.errorMessage.set(null);

    this.service.importValues(file).subscribe({
      next: (message) => {
        this.successMessage.set(message);
        this.isLoading.set(false);
      },
      error: (err: Error) => {
        this.errorMessage.set(err.message);
        this.isLoading.set(false);
      }
    });
  }
}