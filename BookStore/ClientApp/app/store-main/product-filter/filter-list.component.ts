import { Component, OnInit, Output, EventEmitter } from '@angular/core';

import { FilterService } from './FilterService';
import { IFilterList } from './IFilterList';
import { FilterBundle } from '../../common/FilterBundle';

@Component({
  selector: 'bw-filter-list',
  templateUrl: "./filter-list.component.html",
  styles: []
})
export class FilterListComponent implements OnInit {

    // Loaded on Init
    filterList: IFilterList;

    filterBundle: FilterBundle;

    @Output() filtersChanged: EventEmitter<FilterBundle> = new EventEmitter<FilterBundle>();

    constructor(private _filterService: FilterService) {
        this.filterBundle = new FilterBundle();
    }

    ngOnInit() {
        this._filterService.getFilterList()
                .subscribe(filters => {
                    this.filterList = filters; 
                }
                    /* Handle error case */
                );
    }


    submitFilters() {
        this.filtersChanged.emit(this.filterBundle);
    }

    onFilterChanged(event) {
        var id = event.target.defaultValue;
        var name = event.target.name;
        var isChecked = event.target.checked;

        var filterArray;
        switch (name) {
            case "genres":
                filterArray = this.filterBundle.genres;
                break;
            case "formats":
                filterArray = this.filterBundle.formats;
                break;
            case "languages":
                filterArray = this.filterBundle.languages;
                break;
            default:
                return;
        }

        var index = filterArray.indexOf(id);

        if (isChecked && index < 0) {
            filterArray.push(id)
        } else if (!isChecked && index >= 0) {
            filterArray.splice(index, 1);
        }
    }
}
