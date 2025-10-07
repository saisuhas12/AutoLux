// src/app/services/carCategoryService.ts

import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CarCategoryDto } from '../../models/cars/CarCategoryDto';

// Adjust the API endpoint to match your backend's car category controller route
const API_BASE_URL = 'http://localhost:5176/api/Car/all';

@Injectable({
  providedIn: 'root'
})
export class CarCategoryService {
  private http = inject(HttpClient);

  constructor() { }

  // Get all car categories
  getCategories(): Observable<CarCategoryDto[]> {
    return this.http.get<CarCategoryDto[]>(API_BASE_URL);
  }

  // Create a new car category
  createCategory(category: CarCategoryDto): Observable<CarCategoryDto> {
    return this.http.post<CarCategoryDto>(API_BASE_URL, category);
  }

  // Update an existing car category
  updateCategory(id: number, category: CarCategoryDto): Observable<any> {
    return this.http.put(`${API_BASE_URL}/${id}`, category);
  }

  // Delete a car category
  deleteCategory(id: number): Observable<any> {
    return this.http.delete(`${API_BASE_URL}/${id}`);
  }
}
