import { Component, Input, OnInit } from '@angular/core';

import { IBookProduct } from '../../common/IBookProduct';

@Component({
  selector: 'bw-product',
  templateUrl: './product.component.html',
  styles: []
})
export class ProductComponent implements OnInit {

    @Input() product: IBookProduct;
    price: number;

    ngOnInit() {
        var offers = this.product.offers;
        if (offers && offers.length > 0) {
            var minPrice = Infinity;
            for (var i=0; i<offers.length; i++) {
                minPrice = (offers[i].price < minPrice) ? offers[i].price : minPrice;
            }

            this.price = minPrice;

        } else {
            this.price = 0;
        } 
    }

}
