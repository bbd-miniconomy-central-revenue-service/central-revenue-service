import { Injectable } from '@angular/core';
import { HistoryService } from './services/history.service';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RevenueService {
  private taxPayers = this.generateDummyData();

  private historySubscription: Subscription | undefined;

  historyData: any[] = [];
  filteredData: any[] = [];

  constructor(private historyService: HistoryService) { }

  searchTaxPayers(id: string, filter: string, subFilter: string): any[] {
    let results = this.taxPayers;

    if (id) {
      this.filteredData =this.filteredData.filter(t => t.id === id); // Exact match on ID
    }

    if (filter === 'individuals') {
      this.filteredData = this.filteredData.filter(data => data.type === 'INDIVIDUAL');
    } else if (filter === 'companies') {
      this.filteredData = this.filteredData.filter(data => data.type === 'BUSINESS');
      if (subFilter) {
        console.log("subfilter value is: "+subFilter);
        this.filteredData = this.filteredData.filter(data => data.taxType === subFilter.toUpperCase());
      }
    } else if (filter === 'hasPaid') {
      this.filteredData = this.filteredData.filter(data => data.hasPaid === 1);
    } else if (filter === 'inArrears') {
      this.filteredData = this.filteredData.filter(data => data.hasPaid === 0);
    }

    return this.filteredData;
  }

  generateDummyData() {
    this.historySubscription = this.historyService.getHistory().subscribe(
      (data) => {
        // Ensure data is parsed as JSON if it's a string
        if (typeof data === 'string') {
          this.historyData = JSON.parse(data);
        } else {
          this.historyData = data;
        }
        this.filteredData = this.historyData; // Initialize filteredData with historyData
      },
      (error) => {
        console.error('Error fetching history data:', error);
      }
    );
  }
}