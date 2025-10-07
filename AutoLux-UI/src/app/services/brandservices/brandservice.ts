// src/app/services/brand.service.ts
import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BrandReadDto } from '../../models/brands/BrandReadDto';
import { BrandCreateDto } from '../../models/brands/BrandCreateDto';
import { BrandUpdateDto } from '../../models/brands/BrandUpdateDto';


const API_BASE_URL = 'http://localhost:5176/api/Brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {
  private http = inject(HttpClient);

  getAllBrands(): Observable<BrandReadDto[]> {
    return this.http.get<BrandReadDto[]>(`${API_BASE_URL}/all`);
  }

  getBrandById(id: number): Observable<BrandReadDto> {
    return this.http.get<BrandReadDto>(`${API_BASE_URL}/${id}`);
  }

  createBrand(brand: BrandCreateDto): Observable<BrandReadDto> {
    return this.http.post<BrandReadDto>(`${API_BASE_URL}/add`, brand);
  }

  updateBrand(id: number, brand: BrandUpdateDto): Observable<any> {
    return this.http.put(`${API_BASE_URL}/${id}`, brand);
  }

  deleteBrand(id: number, p0: { responseType: "json"; }): Observable<any> {
    return this.http.delete(`${API_BASE_URL}/${id}`);
  }
}
