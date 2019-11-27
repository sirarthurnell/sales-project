import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FiltersAggregate } from '@/models/filters-aggregate';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FiltersService {
  private readonly url = environment.apiUrl + 'filters';
  private filters: FiltersAggregate;

  constructor(private http: HttpClient) {}

  getFilters(): Observable<FiltersAggregate> {
    if (!this.filters) {
      return this.http
        .get<FiltersAggregate>(this.url)
        .pipe(tap(filters => (this.filters = filters)));
    } else {
      return of(this.filters);
    }
  }
}
