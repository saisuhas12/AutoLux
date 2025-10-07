import { RouterLink, RouterModule } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'app-home-component',
  imports: [RouterModule,RouterLink],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css'
})
export class HomeComponent {

}