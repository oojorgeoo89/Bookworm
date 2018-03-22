import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { Cart, Order } from './Cart';

/*
*
* Deals with keeping track of the Orders added to the Cart by the user.
*
* It makes use of Local Storage to persist the orders over time.
*
*/

@Injectable()
export class CartService {

    private _OrderUrl = './api/order';
    private STORAGE_KEY: string = 'bw_cart';

    public cart: Cart;

    constructor(private _http: HttpClient) { 
        var cart: Cart = JSON.parse(localStorage.getItem(this.STORAGE_KEY));
        if (cart) {
            this.reloadCartFromStorageData(cart);
        }
        else {
            this.cart = new Cart();
        }
    }

    addOrderToCart(newOrder: Order) {
        var order = this.cart.orders.find(order => order.offer.id == newOrder.offer.id);

        if (order) {
            order.quantity++;
        } else {
            this.cart.orders.push(newOrder);
        }

        localStorage.setItem(this.STORAGE_KEY, JSON.stringify(this.cart));
    }

    reloadCartFromStorageData(storageCart: Cart) {
        this.cart = new Cart();

        for (var i=0; i<storageCart.orders.length; i++) {
            let newOrder = new Order();
            newOrder.offer = storageCart.orders[i].offer;
            newOrder.quantity = storageCart.orders[i].quantity;

            this.cart.orders.push(newOrder);
        }
    }
   
}