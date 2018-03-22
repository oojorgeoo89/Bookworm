import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';

import { StoreMainComponent } from './store-main/store-main.component';

import { ProductListComponent } from './store-main/product-list/product-list.component';
import { ProductComponent } from './store-main/product-list/product.component';
import { ProductService } from './common/ProductService';

import { FilterListComponent } from './store-main/product-filter/filter-list.component';
import { FilterService } from './store-main/product-filter/FilterService';

import { StoreDetailComponent } from './store-detail/store-detail.component';

import { CartComponent } from './cart/cart.component';
import { CartService } from './common/CartService';

const appRoutes: Routes = [
  { path: 'shop',       component: StoreMainComponent },
  { path: 'shop/:id',   component: StoreDetailComponent },
  { path: 'cart',   component: CartComponent },
  { path: '',           redirectTo: 'shop', pathMatch: 'full' }
];

@NgModule({
  declarations: [
    AppComponent,
    StoreMainComponent,
    StoreDetailComponent,
    ProductListComponent,
    ProductComponent,
    FilterListComponent,
    CartComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot(
        appRoutes,
        { 
            useHash: true,
            enableTracing: false // Debugging purposes only
        } 
    )
  ],
  providers: [ 
    ProductService, 
    FilterService, 
    CartService
  ],
  bootstrap: [ 
    AppComponent 
  ]
})
export class AppModule { }
