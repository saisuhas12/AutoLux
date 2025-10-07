// src/app/models/brands/brand-read-dto.ts

import { CarViewDto } from "../cars/CarViewDto"

export interface BrandReadDto {
  id: number;
  name: string;
  cars: CarViewDto[]; // Matches your backend BrandViewDTO
}
