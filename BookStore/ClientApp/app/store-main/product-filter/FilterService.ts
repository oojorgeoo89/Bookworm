import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

import { IFilterList } from './IFilterList'

@Injectable()
export class FilterService {

    private _productUrl = './api/filters';

    constructor(private _http: HttpClient) { }

    getFilterList(): Observable<IFilterList> {

        return this._http.get<IFilterList>(this._productUrl)
            .catch(this.handleError);

    }

    private handleError(err: HttpErrorResponse) {
        console.error(err.message);
        return Observable.throw(err.message);
    }

}