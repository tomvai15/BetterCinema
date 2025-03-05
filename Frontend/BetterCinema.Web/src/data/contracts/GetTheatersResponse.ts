import { Theater } from "./theater/Theater"


export type GetTheatersResponse = {
    totalCount: number,
    theaters: Theater[]
}