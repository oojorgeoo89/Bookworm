import { IGenre } from '../../common/IGenre';
import { IFormat } from '../../common/IFormat';
import { ILanguage } from '../../common/ILanguage';

export interface IFilterList {
    genres: IGenre[],
    formats: IFormat[],
    languages: ILanguage[]
}