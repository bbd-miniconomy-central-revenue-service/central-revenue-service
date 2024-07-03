import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LoginComponent } from './login/login.component';
import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AuthService } from '../app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
class AuthGuard implements CanActivate {
  constructor(private router: Router,private authService: AuthService) {}

  canActivate(): boolean {

    // Define a function to handle code retrieval

  // Parse the current URL to get parameters
  const urlParams = new URLSearchParams(window.location.search);
  
  // Check if 'code' parameter is present
  if (urlParams.has('code')) {
    // Retrieve the code value
    const code = urlParams.get('code');
    
    // Now you can use the 'code' for further processing, like exchanging it for tokens
    console.log('Authorization code:', code);

    let result = "";
    if(code!=null){
      this.authService.getToken(code).subscribe(
        (result) => {
          console.log(result);
          console.log("Access token is "+result['access_token']);
          localStorage.setItem('access_token',result['access_token']);
        },
        (error) => {
          console.error(error);
          // Handle the error as needed
        }
      );
    }
    console.log("result is: "+result);
    // Optionally, clear the URL parameters to avoid exposing the code in the URL
    window.history.replaceState({}, document.title, window.location.pathname);
    
    // Example: Exchange code for tokens using a backend service
    // exchangeCodeForTokens(code);
  } else {
    // Handle the case where 'code' parameter is not present
    console.error('Authorization code not found.');
  }

    return true;
    // const token = sessionStorage.getItem('accessToken');
    // if (token) {
    //   return true;
    // } else {
    //   this.router.navigate(['/login']);
    //   return false;
    // }
  }
}

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  // { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent, pathMatch: 'full' },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: '/dashboard' } // Wildcard route for a 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }