import { IGenre } from './IGenre';
import { IFormat } from './IFormat';
import { ILanguage } from './ILanguage';

export class FilterBundle
{
    searchString: string;
    genres: number[];
    formats: number[];
    languages: number[];

    constructor () {
        this.genres = [];
        this.formats = [];
        this.languages = [];
    }


}