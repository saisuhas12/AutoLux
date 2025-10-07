// src/app/models/cars/carReadDto.ts

import { CarCategoryDto } from "./CarCategoryDto";

export interface CarReadDto {
  id: number;
  model: string;
  year: number;
  price: number;
  mileage: number;
  color: string;
  isSold: boolean;
  brandId: number;
  brandName: string;
}
