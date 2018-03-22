import * as _ from "lodash"

import { IOffer } from './IOffer'

export class Cart {
    orders: Order[] = [];
    
    get totalPrice(): number {
        return _.sum(_.map(this.orders, order => order.subtotal));
    }
}

export class Order {
    offer: IOffer;
    quantity: number = 1;
    
    get subtotal(): number {
        return this.offer.price * this.quantity;
    }
}