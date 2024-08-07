import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  private apiUrl = 'https://api.mers.projects.bbdgrad.com/api/records/history';

  constructor(private http: HttpClient) { }

  getHistory(): Observable<string> {
    const headers = new HttpHeaders()
      .set('Accept', 'application/json')
      .set('X-Origin', 'central_revenue');

    return this.http.get(this.apiUrl, { headers, responseType: 'text' });
  }
}
