// src/app/models/brands/brand-update-dto.ts

import { BrandCreateDto } from "./BrandCreateDto";
export interface BrandUpdateDto extends BrandCreateDto {
  id: number;
}
