import { Component, OnInit, Input } from '@angular/core';
import { Sale } from '@/models/sale';
import { FiltersService } from '@/services/filters.service';
import { FiltersAggregate } from '@/models/filters-aggregate';

@Component({
  selector: 'app-edit-dialog',
  templateUrl: './edit-dialog.component.html',
  styleUrls: ['./edit-dialog.component.scss']
})
export class EditDialogComponent implements OnInit {
  private psale: Sale;

  @Input() set sale(value: Sale) {
    this.psale = value;

    if (this.psale) {
      this.currentRegionCountry = `${this.psale.region}: ${this.psale.country}`;
      this.currentOrderDate = new Date(this.psale.orderDate);
      this.currentShipDate = new Date(this.psale.shipDate);
    }
  }

  get sale(): Sale {
    return this.psale;
  }

  @Input() title: 'Edit';
  @Input() updateFunction: (sale: Sale) => void;

  filters: FiltersAggregate;
  currentRegionCountry: string;
  currentOrderDate: Date;
  currentShipDate: Date;

  constructor(private filtersService: FiltersService) {}

  ngOnInit() {
    this.filtersService.getFilters().subscribe(filters => {
      this.filters = filters;
    });
  }

  saveChanges(): void {
    const updatedSale = Object.assign({}, this.sale);
    updatedSale.orderDate = this.currentOrderDate.toISOString();
    updatedSale.shipDate = this.currentShipDate.toISOString();
    updatedSale.region = this.currentRegionCountry.split(': ')[0];
    updatedSale.country = this.currentRegionCountry.split(': ')[1];
    this.updateFunction(updatedSale);
  }
}
