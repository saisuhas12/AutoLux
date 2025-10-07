import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import Swal from 'sweetalert2'; // ✅ Import SweetAlert2

import { AuthService } from '../../services/auth/authservice';
import { BrandReadDto } from '../../models/brands/BrandReadDto';
import { BrandCreateDto } from '../../models/brands/BrandCreateDto';
import { BrandService } from '../../services/brandservices/brandservice';
import { BrandUpdateDto } from '../../models/brands/BrandUpdateDto';

@Component({
  selector: 'app-brand',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.css'],
})
export class BrandComponent implements OnInit {
  brands: BrandReadDto[] = [];
  filteredBrands: BrandReadDto[] = [];
  searchTerm: string = '';

  loading: boolean = false;
  error: string = '';
  selectedBrand: BrandReadDto | null = null;
  showCreateForm: boolean = false;
  showEditForm: boolean = false;

  brandModel: BrandCreateDto = { name: '' };

  constructor(
    private brandService: BrandService,
    private authService: AuthService
  ) {}

  ngOnInit() {
    if (this.isAuthenticated()) {
      this.loadBrands();
    }
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  loadBrands() {
    this.loading = true;
    this.brandService.getAllBrands().subscribe({
      next: (brands) => {
        this.brands = brands;
        this.filteredBrands = brands;
        this.loading = false;
      },
      error: () => {
        this.error = 'Failed to load brands';
        this.loading = false;
      },
    });
  }

  onSearchSubmit() {
    const term = this.searchTerm.trim().toLowerCase();
    if (!term) {
      this.filteredBrands = this.brands;
    } else {
      this.filteredBrands = this.brands.filter((b) =>
        b.name.toLowerCase().includes(term)
      );
    }
  }

  showCreateBrandForm() {
    this.showCreateForm = true;
    this.showEditForm = false;
    this.resetBrandModel();
  }

  showEditBrandForm(brand: BrandReadDto) {
    this.selectedBrand = brand;
    this.brandModel = { name: brand.name };
    this.showEditForm = true;
    this.showCreateForm = false;
  }

  hideForms() {
    this.showCreateForm = false;
    this.showEditForm = false;
    this.selectedBrand = null;
    this.resetBrandModel();
  }

  resetBrandModel() {
    this.brandModel = { name: '' };
  }

  createBrand(form: NgForm) {
    if (form.valid) {
      this.loading = true;
      this.brandService.createBrand(this.brandModel).subscribe({
        next: (newBrand) => {
          this.brands.push({ ...newBrand, cars: [] });
          this.onSearchSubmit();
          this.loading = false;
          this.hideForms();
          form.resetForm();

          Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'Brand created successfully!',
            timer: 2000,
            showConfirmButton: false
          });
        },
        error: () => {
          this.error = 'Brand Already Exists';
          this.loading = false;

          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Brand Already Exists'
          });
        },
      });
    }
  }

  updateBrand(form: NgForm) {
    if (form.valid && this.selectedBrand) {
      const brandData: BrandUpdateDto = {
        id: this.selectedBrand.id,
        ...this.brandModel,
      };

      this.loading = true;
      this.brandService.updateBrand(brandData.id, brandData).subscribe({
        next: () => {
          const index = this.brands.findIndex((b) => b.id === brandData.id);
          if (index !== -1) {
            this.brands[index] = { ...this.brands[index], ...brandData };
          }
          this.onSearchSubmit();
          this.hideForms();
          this.loading = false;

          Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'Brand updated successfully!',
            timer: 2000,
            showConfirmButton: false
          });
        },
        error: () => {
          this.error = 'Failed to update brand';
          this.loading = false;

          Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed to update brand'
          });
        },
      });
    }
  }

  deleteBrand(brand: BrandReadDto) {
    Swal.fire({
      title: `Delete "${brand.name}"?`,
      text: 'This action cannot be undone!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.loading = true;
        this.brandService.deleteBrand(brand.id, { responseType: 'text' as 'json' })
          .subscribe({
            next: () => {
              this.brands = this.brands.filter((b) => b.id !== brand.id);
              this.filteredBrands = this.filteredBrands.filter((b) => b.id !== brand.id);
              this.loading = false;

              Swal.fire({
                icon: 'success',
                title: 'Deleted!',
                text: 'Brand deleted successfully.',
                timer: 2000,
                showConfirmButton: false
              });
            },
            error: () => {
              this.error = 'Failed to delete brand';
              this.loading = false;

              Swal.fire({
                icon: 'error',
                title: 'Error',
                text: 'Failed to delete brand'
              });
            }
          });
      }
    });
  }
}
