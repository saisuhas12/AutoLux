import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth/authservice';
import { HttpErrorResponse } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { AuthLoginRequest } from '../../../models/auth/authLoginRequest';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './signin.html',
  styleUrl: './signin.css'
})
export class Signin {
  
  // Template-driven form models
  signinModel: AuthLoginRequest = {
    username: '',
    password: ''
  };

  registerModel = {
    username: '',
    password: '',
    confirmPassword: ''
  };

  // State management with simple properties
  signinLoading: boolean = false;
  registerLoading: boolean = false;
  signinError: string = '';
  registerError: string = '';
  signinSuccess: string = '';
  registerSuccess: string = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  // Sign in method
  // Register method
onRegister(registerForm?: NgForm) {
  if (this.isRegisterValid()) {
    this.registerLoading = true;
    this.registerError = '';
    this.registerSuccess = '';

    this.authService.register({
      name: this.registerModel.username,
      password: this.registerModel.password,
      confirmPassword: this.registerModel.confirmPassword
    }).pipe(
      finalize(() => {
        this.registerLoading = false;
      })
    ).subscribe({
      next: (response) => {
  this.registerSuccess = 'Registration successful! You can now sign in.';

  // Pass the form so it resets completely
  this.resetRegisterForm(registerForm);

  // Switch to Sign In tab after short delay
  setTimeout(() => {
    const signinTab = document.getElementById('signin-tab') as HTMLElement;
    signinTab?.click();
  }, 800);
},
      error: (error: HttpErrorResponse) => {
        console.error('Registration error:', error);
        this.registerSuccess = '';
        if (error.error && error.error.message) {
          this.registerError = error.error.message;
        } else if (error.error && typeof error.error === 'string') {
          this.registerError = error.error;
        } else if (error.message) {
          this.registerError = error.message;
        } else {
          this.registerError = 'Registration failed. Please try again.';
        }
      }
    });
  }
}


// Sign in method
onSignin(signinForm?: NgForm) {
  if (this.isSigninValid()) {
    this.signinLoading = true;
    this.signinError = '';
    this.signinSuccess = '';

    this.authService.login(this.signinModel).pipe(
      finalize(() => {
        this.signinLoading = false;
      })
    ).subscribe({
      next: (response) => {
        this.signinSuccess = 'Login successful! Redirecting...';

        // Reset form state and model
        this.resetSigninForm(signinForm);

        setTimeout(() => {
          this.router.navigate(['/home']);
        }, 1000);
      },
      error: (error: HttpErrorResponse) => {
        console.error('Login error:', error);
        this.signinSuccess = '';

        if (error.error && error.error.message) {
          this.signinError = error.error.message;
        } else if (error.error && typeof error.error === 'string') {
          this.signinError = error.error;
        } else if (error.message) {
          this.signinError = 'Something went wrong. Please try again!';
        } else {
          this.signinError = 'Login failed. Please check your credentials.';
        }
      }
    });
  }
}


  // Validation methods for template-driven forms
  isSigninValid(): boolean {
    return this.signinModel.username.length >= 3 && 
           this.signinModel.password.length >= 6;
  }

  isRegisterValid(): boolean {
    return this.registerModel.username.length >= 3 && 
           this.registerModel.password.length >= 6 &&
           this.registerModel.confirmPassword === this.registerModel.password;
  }

  // Password match validation
  passwordsMatch(): boolean {
    return this.registerModel.password === this.registerModel.confirmPassword;
  }

  // Reset form methods
  resetSigninForm(form?: NgForm) {
  this.signinModel = {
    username: '',
    password: ''
  };
  form?.resetForm(); // Clears touched/dirty state
}

 resetRegisterForm(form?: NgForm) {
  this.registerModel = {
    username: '',
    password: '',
    confirmPassword: ''
  };
  
  form?.resetForm(); // Clears validation/touched state
}

  // Field validation helpers for template
  isUsernameInvalid(model: any): boolean {
    return model.username && model.username.length < 3;
  }

  isPasswordInvalid(model: any): boolean {
    return model.password && model.password.length < 6;
  }

  isConfirmPasswordInvalid(): boolean {
    return this.registerModel.confirmPassword.length > 0 && 
           this.registerModel.confirmPassword !== this.registerModel.password;
  }
  isAuthenticated(): boolean {
  return this.authService.isAuthenticated();
}
}
