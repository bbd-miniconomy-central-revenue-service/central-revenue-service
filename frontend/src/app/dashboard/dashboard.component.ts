import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  filter: string = '';
  subFilter: string = '';
  data = this.generateDummyData();
  filteredData = this.data;

  constructor() { }

  ngOnInit(): void { }

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
    this.filteredData = this.data;

    if (this.filter === 'individuals') {
      this.filteredData = this.filteredData.filter(data => data.type === 'individual');
    } else if (this.filter === 'companies') {
      this.filteredData = this.filteredData.filter(data => data.type === 'company');
      if (this.subFilter) {
        this.filteredData = this.filteredData.filter(data => data.taxType === this.subFilter);
      }
    } else if (this.filter === 'hasPaid') {
      this.filteredData = this.filteredData.filter(data => data.hasPaid);
    } else if (this.filter === 'inArrears') {
      this.filteredData = this.filteredData.filter(data => !data.hasPaid);
    }
  }

  getPaidTaxPayersCount(): number {
    return this.filteredData.filter(data => data.hasPaid).length;
  }

  getAmountCollected(): number {
    return this.filteredData.filter(data => data.hasPaid).reduce((acc, curr) => acc + curr.amountPaid, 0);
  }

  generateDummyData() {
    return [
      { id: '1', type: 'individual', hasPaid: true, amountPaid: 1000, taxType: '' },
      { id: '2', type: 'company', hasPaid: false, amountPaid: 0, taxType: 'vat' },
      { id: '3', type: 'individual', hasPaid: true, amountPaid: 1500, taxType: '' },
      { id: '4', type: 'company', hasPaid: true, amountPaid: 2000, taxType: 'incomeTax' },
      { id: '5', type: 'individual', hasPaid: false, amountPaid: 0, taxType: '' },
      { id: '6', type: 'company', hasPaid: true, amountPaid: 3000, taxType: 'vat' },
      { id: '7', type: 'company', hasPaid: false, amountPaid: 0, taxType: 'incomeTax' },
      { id: '8', type: 'individual', hasPaid: true, amountPaid: 1200, taxType: '' },
      { id: '9', type: 'company', hasPaid: true, amountPaid: 5000, taxType: 'vat' },
      { id: '10', type: 'individual', hasPaid: false, amountPaid: 0, taxType: '' }
    ];
  }
  
}
