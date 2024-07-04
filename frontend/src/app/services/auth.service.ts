import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private tokenUrl = 'https://bbdrs.auth.us-east-1.amazoncognito.com/oauth2/token';

  constructor(private http: HttpClient) { }

  getToken(authCode: string): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/x-www-form-urlencoded',
      'Cookie': 'XSRF-TOKEN=eaf4e54e-f9a8-493b-a040-e3703d6aa6ca'
    });

    const body = new HttpParams()
      .set('grant_type', 'authorization_code')
      .set('client_id', '165320a8ntn332eks21c3q7r8l')
      .set('client_secret', '123nqgvun6p08j55givh5r15v2b0re5g31bh526luir5e6clieqv')
      .set('code', authCode)
      .set('redirect_uri', 'https://mers.projects.bbdgrad.com');

    return this.http.post(this.tokenUrl, body.toString(), { headers });
  }
}
