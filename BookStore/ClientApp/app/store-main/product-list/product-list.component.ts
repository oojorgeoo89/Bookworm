import { Component, OnInit } from '@angular/core';

import { ProductService } from '../../common/ProductService';
import { IBookProduct } from '../../common/IBookProduct';
import { FilterBundle } from '../../common/FilterBundle';

@Component({
  selector: 'bw-product-items',
  templateUrl: "./product-list.component.html",
  styles: []
})
export class ProductListComponent implements OnInit {

    productList: IBookProduct[];
    private pageNumber: number;

    filterBundle: FilterBundle;
    orderBy: string = "date,desc";

    constructor(private _productService: ProductService) {
    }

    ngOnInit() {
        this.loadPage(0);
    }

    public loadNextPage() {
        this.loadPage(this.pageNumber + 1);
    } 

    public loadPreviousPage() {
        if (this.pageNumber == 0) {
            return;
        }

        this.loadPage(this.pageNumber - 1);
    }

    onFiltersChanged(filterBundle: FilterBundle) {
        this.filterBundle = filterBundle;
        this.loadPage(0);
    }

    loadPage(pageNumber: number) {
        this.pageNumber = pageNumber;

        this._productService.getProducts(pageNumber, this.orderBy, this.filterBundle)
                .subscribe(products => {
                    this.productList = products;
                }
                    /* Handle error case */
                );
    }

    onSortChanged(orderBy: string) {
        this.orderBy = orderBy;
        this.loadPage(0);
    }

}
