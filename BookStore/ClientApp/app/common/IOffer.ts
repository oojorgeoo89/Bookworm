import { IBookProduct } from './IBookProduct'

export interface IOffer {
    id: number;
    book: IBookProduct;
    format: string;
    language: string;
    price: number;
}