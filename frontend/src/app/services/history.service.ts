import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HistoryService {

  private apiUrl = 'http://localhost:5000/api/records/history';

  constructor(private http: HttpClient) { }

  getHistory(): Observable<string> {
    const headers = new HttpHeaders()
      .set('Accept', 'text/plain')
      .set('X-Origin', 'central_revenue');

    return this.http.get(this.apiUrl, { headers, responseType: 'text' });
  }
}
