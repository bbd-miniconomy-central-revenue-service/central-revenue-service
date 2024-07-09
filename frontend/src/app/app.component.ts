import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../app/services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Central_Revenue_Service';

  accessToken: string | null;

  constructor(private router: Router) {
    this.accessToken = localStorage.getItem('access_token');}
    
  logout() {
    localStorage.removeItem('access_token');
    sessionStorage.removeItem('access_token');
    this.router.navigate(['/home']);
  }
}
