// src/app/components/cars/car.component.ts
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { CarService, CarCreateDto, CarUpdateDto, CarSearchQuery } from '../../services/carservices/carService';
import { CarCategoryService } from '../../services/carservices/carCategoryService';
import { AuthService } from '../../services/auth/authservice';
import { CarReadDto } from '../../models/cars/CarReadDto';
import { CarCategoryDto } from '../../models/cars/CarCategoryDto';
import { BrandReadDto } from '../../models/brands/BrandReadDto';
import { BrandService } from '../../services/brandservices/brandservice';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-car',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent implements OnInit {

  cars: CarReadDto[] = [];
  brands: BrandReadDto[] = [];
  categories: CarCategoryDto[] = [];
  loading: boolean = false;
  error: string = '';
  selectedCar: CarReadDto | null = null;
  showCreateForm: boolean = false;
  showEditForm: boolean = false;
  showSearchForm: boolean = false;

 searchModel: CarSearchQuery = {
  model: '',
  brand: undefined, // not brandName
  year: undefined,
  price: undefined,
  mileage: undefined,
  color: '',
  isSold: undefined
};


  carModel: CarCreateDto = {
    model: '',
    year: new Date().getFullYear(),
    price: 0,
    mileage: 0,
    color: '',
    isSold: false,
    brandName: ''
  };

  constructor(
    private carService: CarService,
    private carCategoryService: CarCategoryService,
    private authService: AuthService,
    private brandService: BrandService,
  ) {}

  ngOnInit() {
    if (this.isAuthenticated()) {
      this.loadCars();
      this.loadCategories();
      this.loadBrands();
    }
  }

  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  loadCars() {
    this.loading = true;
    this.error = '';
    this.carService.getCars().subscribe({
      next: (cars: CarReadDto[]) => {
        this.cars = cars;
        this.loading = false;
      },
      error: (err: any) => {
        this.error = 'Failed to load cars';
        this.loading = false;
        console.error(err);
      }
    });
  }

  loadCategories() {
    this.carCategoryService.getCategories().subscribe({
      next: (categories: CarCategoryDto[]) => this.categories = categories,
      error: (err: any) => console.error('Failed to load car categories', err)
    });
  }

  loadBrands() {
    this.brandService.getAllBrands().subscribe({
      next: (data) => this.brands = data,
      error: (err) => console.error('Failed to load brands', err)
    });
  }

  searchCars() {
  this.loading = true;
  this.error = '';
  const query: CarSearchQuery = {};

  if (this.searchModel.model?.trim()) query.model = this.searchModel.model.trim();
  if (this.searchModel.brand?.trim()) query.brand = this.searchModel.brand.trim(); // ✅ matches backend
  if (this.searchModel.year !== undefined) query.year = this.searchModel.year;
  if (this.searchModel.price !== undefined) query.price = this.searchModel.price;
  if (this.searchModel.mileage !== undefined) query.mileage = this.searchModel.mileage;
  if (this.searchModel.color?.trim()) query.color = this.searchModel.color.trim();
  if (this.searchModel.isSold !== undefined) query.isSold = this.searchModel.isSold;

  this.carService.searchCars(query).subscribe({
    next: (cars: CarReadDto[]) => {
      this.cars = cars;
      this.loading = false;
    },
    error: (err: any) => {
      this.error = 'Failed to search cars';
      this.loading = false;
      console.error(err);
    }
  });
}



  cancelSearch() {
    this.searchModel = {
      model: '',
      brand: '',
      year: undefined,
      price: undefined,
      mileage: undefined,
      color: '',
      isSold: undefined
    };
    this.loadCars();
    this.hideForms();
  }

  showCreateCarForm() {
    this.showCreateForm = true;
    this.showEditForm = false;
    this.showSearchForm = false;
    this.resetCarModel();
  }

  showSearchCarForm() {
    this.showSearchForm = true;
    this.showCreateForm = false;
    this.showEditForm = false;
    this.resetCarModel();
  }

  showEditCarForm(car: CarReadDto) {
    this.selectedCar = car;
    this.showEditForm = true;
    this.showCreateForm = false;
    this.showSearchForm = false;

    this.carModel = {
      model: car.model,
      year: car.year,
      price: car.price,
      mileage: car.mileage,
      color: car.color,
      isSold: car.isSold,
      brandId: car.brandId,
      brandName: car.brandName ?? ''
    };
  }

  hideForms() {
    this.showCreateForm = false;
    this.showEditForm = false;
    this.showSearchForm = false;
    this.selectedCar = null;
    this.resetCarModel();
  }

  resetCarModel() {
    this.carModel = {
      model: '',
      year: new Date().getFullYear(),
      price: 0,
      mileage: 0,
      color: '',
      isSold: false,
      brandName: ''
    };
  }

  createCar(form: NgForm) {
  if (form.valid) {
    if (!this.carModel.brandName || this.carModel.brandName.trim() === '') {
      Swal.fire({
        title: 'Missing Brand',
        text: 'Please enter a valid brand name.',
        icon: 'warning'
      });
      return;
    }

    this.loading = true;

    this.carService.createCar(this.carModel).subscribe({
      next: (newCar: CarReadDto) => {
        // Add new car to the list
        this.cars.push(newCar);

        // Add brand locally if not already in dropdown
        if (!this.brands.some(b => b.name === newCar.brandName)) {
          this.brands.push({
            name: newCar.brandName,
            id: 0,
            cars: []
          });
        }

        // Optional: Fetch latest brands from backend for consistency
        this.brandService.getAllBrands().subscribe({
          next: (brands) => this.brands = brands,
          error: (err) => console.error('Failed to refresh brands', err)
        });

        // Reset form state
        this.loading = false;
        this.hideForms();
        form.resetForm();

        Swal.fire({
          title: 'Created!',
          text: 'Car created successfully!',
          icon: 'success',
          timer: 2000,
          showConfirmButton: false
        });
      },
      error: (err: any) => {
        this.error = 'Failed to create car';
        this.loading = false;
        console.error(err);
        Swal.fire({
          title: 'Error',
          text: 'Failed to create car',
          icon: 'error'
        });
      }
    });
  }
}


  updateCar(form: NgForm) {
    if (form.valid && this.selectedCar) {
      const carData: CarUpdateDto = {
        id: this.selectedCar.id,
        ...this.carModel
      };

      this.loading = true;
      this.carService.updateCar(carData.id, carData).subscribe({
        next: () => {
          this.loadCars(); // refresh instantly
          this.hideForms();
          this.loading = false;

          Swal.fire({
            title: 'Success!',
            text: 'Car updated successfully!',
            icon: 'success',
            timer: 2000,
            showConfirmButton: false
          });
        },
        error: (err: any) => {
          this.error = 'Failed to update car';
          this.loading = false;
          console.error(err);
          Swal.fire({
            title: 'Error!',
            text: 'Failed to update car',
            icon: 'error'
          });
        }
      });
    }
  }

  deleteCar(car: CarReadDto) {
    Swal.fire({
      title: 'Are you sure?',
      text: `Delete car: ${car.model}?`,
      icon: 'warning',
      showCancelButton: true,
      confirmButtonColor: '#d33',
      cancelButtonColor: '#3085d6',
      confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
      if (result.isConfirmed) {
        this.loading = true;
        this.error = '';
        this.carService.deleteCar(car.id).subscribe({
          next: () => {
            this.cars = this.cars.filter(c => c.id !== car.id);
            this.loading = false;
            Swal.fire({
              title: 'Deleted!',
              text: 'Car deleted successfully!',
              icon: 'success',
              timer: 2000,
              showConfirmButton: false
            });
          },
          error: (err: any) => {
            this.error = 'Failed to delete car';
            this.loading = false;
            console.error(err);
            Swal.fire({
              title: 'Error',
              text: 'Failed to delete car',
              icon: 'error'
            });
          }
        });
      }
    });
  }

  getCategoryName(categoryId: number): string {
    const category = this.categories.find(c => c.id === categoryId);
    return category ? category.name : 'Unknown';
  }
  
}