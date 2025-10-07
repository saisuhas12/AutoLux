import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth/authservice';

export const authGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    return true;
  } else {
    router.navigate(['/signin']);
    return false;
  }
};
