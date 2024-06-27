import { Component } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { RevenueService } from '../revenue.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {
  taxPayerId: string = '';
  filter: string = '';
  subFilter: string = '';
  searchResults: any[] = [];
  pagedResults: any[] = [];

  constructor(private revenueService: RevenueService) { }

  selectFilter(filter: string): void {
    this.filter = filter;
    this.subFilter = '';
  }

  selectSubFilter(subFilter: string): void {
    this.subFilter = subFilter;
  }

  search(): void {
    this.searchResults = this.revenueService.searchTaxPayers(this.taxPayerId, this.filter, this.subFilter);
    this.setPageResults(0);
  }

  pageChanged(event: PageEvent): void {
    this.setPageResults(event.pageIndex);
  }

  private setPageResults(pageIndex: number): void {
    const pageSize = 5;
    const startIndex = pageIndex * pageSize;
    this.pagedResults = this.searchResults.slice(startIndex, startIndex + pageSize);
  }
}
