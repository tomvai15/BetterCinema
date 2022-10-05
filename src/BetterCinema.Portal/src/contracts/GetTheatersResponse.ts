import { Theater } from '../models/Theater';

export type GetTheatersResponse = {
    totalCount: number,
    theaters: Theater[]
}