import { Component, OnInit } from '@angular/core';
import {ActivatedRoute} from "@angular/router";

import { CartService } from '../common/CartService';
import { IBookProduct } from '../common/IBookProduct';
import { IOffer } from '../common/IOffer';
import { Cart, Order } from '../common/Cart';

@Component({
  selector: 'bw-cart',
  templateUrl: "./cart.component.html",
  styleUrls: [ './cart.component.css' ]
})
export class CartComponent implements OnInit {

    cart: Cart;

    constructor(private _cartService: CartService) { }

    ngOnInit() {
        this.cart = this._cartService.cart;
    }

    removeFromCart(order: Order) {
        this.cart.orders = this.cart.orders.filter(function(item) { 
            return item.offer.id != order.offer.id;
        });
    }
}
