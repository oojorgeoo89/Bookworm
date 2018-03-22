import { IOffer } from './IOffer';

export interface IBookProduct {
    id: number;
    title: string;
    summary: string;
    isbn: string;
    image: string;
    genre;
    offers: IOffer[];
}