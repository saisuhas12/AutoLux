import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent {
  
  // Contact form model
  contactForm = {
    name: '',
    email: '',
    subject: '',
    message: ''
  };

  isSubmitting = false;
  isSubmitted = false;

  // Submit contact form
  onSubmit(form: NgForm) {
    if (form.valid) {
      this.isSubmitting = true;
      
      // Simulate API call
      setTimeout(() => {
        console.log('Contact form submitted:', this.contactForm);
        this.isSubmitting = false;
        this.isSubmitted = true;
        
        // Reset form after showing success message
        setTimeout(() => {
          this.resetForm(form);
        }, 3000);
      }, 1000);
    }
  }

  // Reset the form
  resetForm(form: NgForm) {
    this.contactForm = {
      name: '',
      email: '',
      subject: '',
      message: ''
    };
    this.isSubmitted = false;
    form.resetForm();
  }
}
