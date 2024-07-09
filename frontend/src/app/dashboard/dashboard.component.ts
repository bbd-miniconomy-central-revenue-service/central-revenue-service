import { Component, OnInit, OnDestroy } from '@angular/core';
import { HistoryService } from '../services/history.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit, OnDestroy {
  filter: string = '';
  subFilter: string = '';
  historyData: any[] = [];
  filteredData: any[] = [];
  private historySubscription: Subscription | undefined;

  constructor(private historyService: HistoryService) { }

  ngOnInit(): void {
    this.historySubscription = this.historyService.getHistory().subscribe(
      (data) => {
        if (typeof data === 'string') {
          this.historyData = JSON.parse(data);
        } else {
          this.historyData = data;
        }
        this.filteredData = this.historyData;
      },
      (error) => {
        console.error('Error fetching history data:', error);
      }
    );
  }

  ngOnDestroy(): void {
    if (this.historySubscription) {
      this.historySubscription.unsubscribe();
    }
  }

  selectFilter(filter: string): void {
    this.filter = filter;
    this.subFilter = '';
    this.applyFilters();
  }

  selectSubFilter(subFilter: string): void {
    this.subFilter = subFilter;
    this.applyFilters();
  }

  applyFilters(): void {
    this.filteredData = this.historyData;

    if (this.filter === 'individuals') {
      this.filteredData = this.filteredData.filter(data => data.type === 'INDIVIDUAL');
    } else if (this.filter === 'companies') {
      this.filteredData = this.filteredData.filter(data => data.type === 'BUSINESS');
      if (this.subFilter) {
        console.log("subfilter value is: "+this.subFilter);
        this.filteredData = this.filteredData.filter(data => data.taxType === this.subFilter.toUpperCase());
      }
    } else if (this.filter === 'hasPaid') {
      this.filteredData = this.filteredData.filter(data => data.hasPaid === 1);
    } else if (this.filter === 'inArrears') {
      this.filteredData = this.filteredData.filter(data => data.hasPaid === 0);
    }
  }

  getPaidTaxPayersCount(): number {
    return this.filteredData.filter(data => data.hasPaid === 1).length;
  }

  getAmountCollected(): number {
    return this.filteredData.filter(data => data.hasPaid === 1).reduce((acc, curr) => acc + curr.paymentAmount, 0);
  }
}
