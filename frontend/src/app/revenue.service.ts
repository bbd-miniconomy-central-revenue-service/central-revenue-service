import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RevenueService {
  private taxPayers = this.generateDummyData();

  constructor() { }

  searchTaxPayers(id: string, filter: string, subFilter: string): any[] {
    let results = this.taxPayers;

    if (id) {
      results = results.filter(t => t.id === id); // Exact match on ID
    }

    if (filter === 'individuals') {
      results = results.filter(t => t.type === 'individual');
    } else if (filter === 'companies') {
      results = results.filter(t => t.type === 'company');
      if (subFilter) {
        results = results.filter(t => t.taxType === subFilter);
      }
    } else if (filter === 'hasPaid') {
      results = results.filter(t => t.hasPaid);
    } else if (filter === 'inArrears') {
      results = results.filter(t => !t.hasPaid);
    }

    return results;
  }

  generateDummyData() {
    const data = [];
    for (let i = 1; i <= 100; i++) {
      const isCompany = i % 2 === 0;
      data.push({
        id: i.toString(),
        type: isCompany ? 'company' : 'individual',
        hasPaid: Math.random() > 0.5,
        amountPaid: Math.random() * 10000,
        taxType: isCompany ? (Math.random() > 0.5 ? 'vat' : 'incomeTax') : ''
      });
    }
    return data;
  }
}