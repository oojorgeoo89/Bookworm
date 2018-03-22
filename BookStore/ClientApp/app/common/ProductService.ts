import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/observable/of';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

import { IBookProduct } from './IBookProduct';
import { FilterBundle } from './FilterBundle';

@Injectable()
export class ProductService {

    private _productUrl = './api/book';

    private _cachedLastResponse: IBookProduct[];

    constructor(private _http: HttpClient) { }

    /*
    *  Fetches a page of data from the server.
    */
    getProducts(pageNumber: number, orderBy: string, filterBundle: FilterBundle): Observable<IBookProduct[]> {

        let params = new HttpParams();
        params = params.append('page', pageNumber.toString());
        params = params.append('orderBy', orderBy);
        params = params.append('size', '12');

        if (filterBundle) {
            if (filterBundle.searchString && filterBundle.searchString != "") {
                params = params.append('searchString', filterBundle.searchString);
            }

            params = this.appendFilterParams(params, 'genres', filterBundle.genres);
            params = this.appendFilterParams(params, 'formats', filterBundle.formats);
            params = this.appendFilterParams(params, 'languages', filterBundle.languages);
        }

        return this._http.get<IBookProduct[]>(this._productUrl, {
                                                    params: params
                                                }).map( (books: IBookProduct[]) => {
                                                    this._cachedLastResponse = books;
                                                    return books;
                                                })
                                                .catch(this.handleError);

    }

    /*
    *  Fetches a specific product from the server.
    */
    getProduct(id: number): Observable<IBookProduct> {
        if (this._cachedLastResponse) {
            var book = this._cachedLastResponse.find(book => book.id == id);
            if (book) {
                return Observable.of(book);
            }
        }

        return this._http.get<IBookProduct[]>(this._productUrl + "/" + id)
                                                .catch(this.handleError);
    }

    private handleError(err: HttpErrorResponse) {
        console.error(err.message);
        return Observable.throw(err.message);
    }

    private appendFilterParams(params: HttpParams, name: string, filterArray: number[]): HttpParams {
        if (filterArray) {
            for (var i=0; i<filterArray.length; i++) {
                params = params.append(name, filterArray[i].toString());
            }
        }

        return params;
    }
}