// src/app/services/carService.ts
import { inject, Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

import { CarReadDto } from '../../models/cars/CarReadDto';

export interface CarCreateDto {
  model: string;
  year: number;
  price: number;
  mileage: number;
  color: string;
  isSold: boolean;
  brandId?: number;
  brandName?: string;
}

export interface CarUpdateDto extends CarCreateDto {
  id: number;
}

// ✅ Updated to match backend search params
export interface CarSearchQuery {
  model?: string;
  brand?: string; // matches backend [FromQuery] string? brand
  year?: number;
  price?: number;
  mileage?: number;
  color?: string;
  isSold?: boolean;
}

const API_BASE_URL = 'http://localhost:5176/api/Car';

@Injectable({
  providedIn: 'root'
})
export class CarService {
  private http = inject(HttpClient);

  constructor() {}

  // Get all cars
  getCars(): Observable<CarReadDto[]> {
    return this.http.get<CarReadDto[]>(`${API_BASE_URL}/all`);
  }

  // Get a single car by ID
  getCarById(id: number): Observable<CarReadDto> {
    return this.http.get<CarReadDto>(`${API_BASE_URL}/${id}`);
  }

  // ✅ Updated searchCars to match backend parameters
  searchCars(query: CarSearchQuery): Observable<CarReadDto[]> {
  let params = new HttpParams();

  if (query.model) params = params.set('model', query.model);
  if (query.brand) params = params.set('brand', query.brand);
  if (query.year != null) params = params.set('year', query.year.toString());
  if (query.price != null) params = params.set('price', query.price.toString());
  if (query.mileage != null) params = params.set('mileage', query.mileage.toString());
  if (query.color) params = params.set('color', query.color);
  if (query.isSold != null) params = params.set('isSold', query.isSold.toString());

  return this.http.get<CarReadDto[]>(`${API_BASE_URL}/search`, { params });
}


  // Create a new car
  createCar(car: CarCreateDto): Observable<CarReadDto> {
    return this.http.post<CarReadDto>(`${API_BASE_URL}/add`, car);
  }

  // Update an existing car
  updateCar(id: number, car: CarUpdateDto): Observable<any> {
    return this.http.put(`${API_BASE_URL}/${id}`, car);
  }

  // Delete a car
  deleteCar(id: number): Observable<any> {
    return this.http.delete(`${API_BASE_URL}/${id}`);
  }
}
