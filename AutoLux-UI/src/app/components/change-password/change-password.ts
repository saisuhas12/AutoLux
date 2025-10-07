import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators, AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { Router } from '@angular/router';

// Custom validator to match passwords
export const passwordMatchValidator: ValidatorFn = (control: AbstractControl): ValidationErrors | null => {
  const newPassword = control.get('newPassword')?.value;
  const confirmPassword = control.get('confirmPassword')?.value;
  return newPassword && confirmPassword && newPassword !== confirmPassword
    ? { passwordMismatch: true }
    : null;
};

@Component({
  selector: 'app-change-password',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './change-password.html',
  styleUrl: './change-password.css'
})
export class ChangePassword {
  changePasswordForm;
  message: string = '';
  isSuccess: boolean = false;
  isLoading: boolean = false;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.changePasswordForm = this.fb.group(
      {
        currentPassword: ['', Validators.required],
        newPassword: ['', [Validators.required, Validators.minLength(6)]],
        confirmPassword: ['', Validators.required]
      },
      { validators: passwordMatchValidator }
    );
  }

  onSubmit() {
    if (this.changePasswordForm.invalid) return;

    this.isLoading = true;
    this.message = '';

    this.http.post(
      'http://localhost:5176/api/CarAuth/change-password',
      {
        currentPassword: this.changePasswordForm.value.currentPassword,
        newPassword: this.changePasswordForm.value.newPassword
      },
      { responseType: 'text' }
    ).subscribe({
      next: (res) => {
        this.isSuccess = true;
        this.message = res;

        // Clear token & redirect to login after short delay
        setTimeout(() => {
          localStorage.removeItem('token'); // change key if different
          this.router.navigate(['/signin']);
        }, 1500);
      },
      error: (err) => {
        this.isSuccess = false;
        this.message = err.error || 'Failed to change password.';
        this.isLoading = false;
      }
    });
  }
}
