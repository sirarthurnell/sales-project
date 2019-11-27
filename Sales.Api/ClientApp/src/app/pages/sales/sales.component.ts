import { Component, OnInit, OnDestroy } from '@angular/core';
import { SalesService } from '@/services/sales.service';
import { PaginatedSalesResponse } from '@/models/paginated-sales-response';
import { Subscription } from 'rxjs';
import { Sale } from '@/models/sale';
import { SearchRequest } from '@/models/search-request';
import { FiltersService } from '@/services/filters.service';
import { FiltersAggregate } from '@/models/filters-aggregate';
import { OrderRequest } from '@/models/order-request';

@Component({
  selector: 'app-sales',
  templateUrl: './sales.component.html',
  styleUrls: ['./sales.component.scss']
})
export class SalesComponent implements OnInit, OnDestroy {
  private subscription: Subscription;

  ordering = new OrderRequest();
  filters: FiltersAggregate;
  searchRequest = new SearchRequest();
  paginated: PaginatedSalesResponse;
  currentSale: Sale;
  editOperation = '';
  updateFunction: (updatedSale: Sale) => void;

  constructor(
    public salesService: SalesService,
    public filtersService: FiltersService
  ) {}

  ngOnInit() {
    this.filtersService
      .getFilters()
      .subscribe(filters => (this.filters = filters));

    this.ordering.field = 'region';
    this.ordering.direction = 'asc';

    this.subscription = this.salesService.sales$.subscribe(
      paginated => (this.paginated = paginated)
    );

    this.goToPage(0);
  }

  ngOnDestroy() {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  nextPage(e: Event): void {
    e.preventDefault();

    if (this.paginated.currentPage < this.paginated.totalPages) {
      this.goToPage(this.paginated.currentPage + 1);
    }
  }

  previousPage(e: Event): void {
    e.preventDefault();

    if (this.paginated.currentPage > 0) {
      this.goToPage(this.paginated.currentPage - 1);
    }
  }

  search(): void {
    this.goToPage(this.paginated.currentPage);
  }

  applyOrdering(e: Event, field: string): void {
    e.preventDefault();

    this.ordering.field = field;
    this.ordering.direction =
      this.ordering.direction === 'asc' ? 'desc' : 'asc';

    this.goToPage(this.paginated.currentPage);
  }

  private goToPage(page: number): void {
    for (const prop in this.searchRequest) {
      if (this.searchRequest[prop]) {
        this.salesService.getPaginated(page, this.searchRequest, this.ordering);
        return;
      }
    }

    this.salesService.getPaginated(page, null, this.ordering);
  }

  isLastPage(): boolean {
    return this.paginated.currentPage === this.paginated.totalPages;
  }

  isFirstPage(): boolean {
    return this.paginated.currentPage === 0;
  }

  deleteSale(sale: Sale): void {
    this.salesService.delete(sale);
  }

  updateSale(sale: Sale): void {
    this.editOperation = 'Update';
    this.currentSale = Object.assign({}, sale);

    this.updateFunction = (updatedSale: Sale) => {
      this.salesService.update(updatedSale);
    };
  }

  createSale(): void {
    this.editOperation = 'Create';
    this.currentSale = {
      region: 'Europe',
      country: 'Slovakia',
      itemType: 'Clothes',
      orderDate: new Date().toISOString(),
      orderPriority: 'M',
      salesChannel: 'Offline',
      shipDate: new Date().toISOString(),
      unitCost: 0,
      unitPrice: 0,
      unitsSold: 0,
      totalCost: 0,
      totalProfit: 0,
      totalRevenue: 0,
      id: null,
      orderId: 0
    };

    this.updateFunction = (createdSale: Sale) => {
      this.salesService.create(createdSale);
    };
  }
}
