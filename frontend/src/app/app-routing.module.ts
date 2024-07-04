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
          this.router.navigate(['/dashboard']);

        },
        (error) => {
          console.error(error);
          // Handle the error as needed
        }
      );
    } else {
      this.router.navigate(['/home']);
    }
    console.log("result is: "+result);
    window.history.replaceState({}, document.title, window.location.pathname);
    
  } else {
    console.error('Authorization code not found.');
  }

    return true;
  }
}

const routes: Routes = [
  // { path: 'dashboard', component: DashboardComponent, pathMatch: 'full' },
  { path: 'home', component: HomeComponent, pathMatch: 'full' },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' }, // Default route
  { path: '**', redirectTo: '/dashboard' } // Wildcard route for a 404 page
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }