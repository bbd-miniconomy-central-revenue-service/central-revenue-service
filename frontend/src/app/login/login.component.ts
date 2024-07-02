// src/app/login/login.component.ts
import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {environment} from '../../environments/environment';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  confirmPassword: string = '';
  isSignUp: boolean = false;

  constructor(private http: HttpClient, private router: Router) {}

  handleSignIn(event: Event) {
    event.preventDefault();
    this.http.post<any>(`${environment.apiUrl}/api/v1/Auth/signIn`, {
      username: this.email,
      password: this.password
    }).subscribe(response => {
      const session = response;
      if (session && session.authenticationResult.accessToken) {
        sessionStorage.setItem('accessToken', session.authenticationResult.accessToken);
        if (sessionStorage.getItem('accessToken')) {
          this.router.navigate(['/home']);
        } else {
          console.error('Session token was not set properly.');
        }
      } else {
        console.error('SignIn session or AccessToken is undefined.');
      }
    }, error => {
      alert(`Sign in failed: ${error}`);
    });
  }

  handleSignUp(event: Event) {
    event.preventDefault();
    if (this.password !== this.confirmPassword) {
      alert('Passwords do not match');
      return;
    }
    this.http.post<any>(`${environment.apiUrl}/api/v1/Auth/signUp`, {
      username: this.email,
      password: this.password
    }).subscribe(() => {
      this.router.navigate(['/confirm'], { state: { email: this.email } });
    }, error => {
      alert(`Sign up failed: ${error}`);
    });
  }
}