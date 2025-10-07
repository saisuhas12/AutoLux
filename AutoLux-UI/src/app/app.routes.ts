import { Routes } from '@angular/router';
// import { ProductComponent } from './components/product/product.component';
import { AboutComponent } from './components/about/about.component';
import { ContactComponent } from './components/contact/contact.component';
import { authGuard } from './guards/auth.guard';  
import { BrandComponent } from './components/brand/brand.component';
import { CarComponent } from './components/car/car.component';
import { HomeComponent } from './components/home-component/home-component';


export const routes: Routes = [
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  // { path: 'signin', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'brands', component: BrandComponent, canActivate: [authGuard] },
  { path: 'cars', component: CarComponent, canActivate: [authGuard] },

  { path: 'about', component: AboutComponent },
  { path: 'contact', component: ContactComponent },
  { path: 'signin', loadComponent: () => import('./components/auth/signin/signin').then(m => m.Signin) },
  { path: 'register', redirectTo: '/signin' },
   { 
    path: 'change-password',
    loadComponent: () => import('./components/change-password/change-password').then(m => m.ChangePassword),
    canActivate: [authGuard] // Protect this route
  },
  { path: '**', redirectTo: '/home' }
];