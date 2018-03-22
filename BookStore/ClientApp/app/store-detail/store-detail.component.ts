import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";

import { ProductService } from '../common/ProductService';
import { CartService } from '../common/CartService';
import { IBookProduct } from '../common/IBookProduct';
import { IOffer } from '../common/IOffer';
import { Order } from '../common/Cart';

@Component({
  selector: 'bw-store-detail',
  templateUrl: "./store-detail.component.html",
  styles: []
})
export class StoreDetailComponent implements OnInit {

    book: IBookProduct;
    selectedOffer: IOffer;

    constructor(private _productService: ProductService
                        , private _cartService: CartService
                        , private route: ActivatedRoute) { }

    ngOnInit() {
        this.route.params.subscribe( params => {
            this._productService.getProduct(params['id']).subscribe(book => {
                this.book = book;
            })
        });
    }

    addToCart() {
        var offer = this.selectedOffer;
        var book: IBookProduct = {
            id: this.book.id,
            title: this.book.title,
            summary: this.book.summary,
            isbn: this.book.isbn,
            image: this.book.image,
            genre: this.book.genre,
            offers: []
        }
        offer.book = book;

        var order = new Order();
        order.offer = offer;

        this._cartService.addOrderToCart(order);
    }



}
