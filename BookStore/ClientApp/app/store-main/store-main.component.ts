import { Component, ViewChild } from '@angular/core';

import { FilterBundle } from '../common/FilterBundle';
import { ProductListComponent } from './product-list/product-list.component';

@Component({
  selector: 'bw-store-main',
  templateUrl: "./store-main.component.html",
  styles: []
})
export class StoreMainComponent {

    @ViewChild(ProductListComponent)
    private productListChild: ProductListComponent;

    filterBundle: FilterBundle;

    onFiltersChanged(filterBundle: FilterBundle) {
        this.filterBundle = filterBundle;

        this.productListChild.onFiltersChanged(filterBundle)
    }

}
