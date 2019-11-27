import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { BehaviorSubject } from 'rxjs';
import { Sale } from '@/models/sale';
import { PaginatedSalesResponse } from '@/models/paginated-sales-response';
import { ToastrService } from 'ngx-toastr';
import { SearchRequest } from '@/models/search-request';
import { OrderRequest } from '@/models/order-request';

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private readonly url = environment.apiUrl + 'sales';
  private subject$ = new BehaviorSubject<PaginatedSalesResponse>(null);
  private currentPage = 0;
  private currentSearch: SearchRequest = null;
  private currentOrder: OrderRequest = null;

  sales$ = this.subject$.asObservable();

  constructor(private http: HttpClient, private toastrService: ToastrService) {}

  getPaginated(
    page: number,
    search: SearchRequest = null,
    order: OrderRequest = null
  ) {
    this.currentSearch = search;
    this.currentOrder = order;

    const params = new HttpParams({
      fromObject: {
        page: page.toString(),
        ...(this.currentSearch || {}),
        ...(this.currentOrder || {})
      }
    });

    this.http
      .get<PaginatedSalesResponse>(this.url + '/paginated', { params })
      .subscribe(paginated => {
        this.currentPage = paginated.currentPage;
        this.subject$.next(paginated);
      });
  }

  delete(sale: Sale): void {
    const params = new HttpParams({
      fromObject: {
        id: sale.id
      }
    });

    this.http
      .delete<Sale>(this.url, { params })
      .subscribe(_ => {
        this.toastrService.success(
          `Sale with ID ${sale.id} deleted`,
          'Sale deleted'
        );
        this.refresh();
      });
  }

  update(sale: Sale): void {
    this.http.put<Sale>(this.url, sale).subscribe(_ => {
      this.toastrService.success(
        `Sale with ID ${sale.id} updated`,
        'Sale updated'
      );
      this.refresh();
    });
  }

  create(sale: Sale): void {
    this.http.post<Sale>(this.url, sale).subscribe(_ => {
      this.toastrService.success(`New sale created`, 'Sale created');
      this.refresh();
    });
  }

  refresh(): void {
    this.getPaginated(this.currentPage);
  }
}
