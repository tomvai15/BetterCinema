import axios from 'axios';
import {Theater} from '../models/Theater';

const API_URL = 'http://localhost:3001/api/';

const theaterUri = API_URL+ 'theaters'

function isExpectedStatus(status: number, expectedStatus: number)
{
    if (expectedStatus == status)
    {
        return true;
    }
    return false;
}

class TheaterService {
    async getTheaters (): Promise<Theater[]>
    {
        const response = await axios.get(theaterUri,  { headers: {} })

        return response.data;
    }
    async getTheater(id: number): Promise<Theater>
    {
        const uri = `${theaterUri}/${id}`;
        const response = await axios.get(uri, { headers: {} })

        return response.data;
    }    
    async addTheater(theater: Theater): Promise<boolean>
    {
        const res = await axios.post(theaterUri, {body: theater, headers: {} })

        return isExpectedStatus(res.status, 201);
    } 
    async updateTheater(theater: Theater): Promise<boolean>
    {
        const uri = `${theaterUri}/${theater.id}`;
        const res = await axios.put(uri, {body: theater, headers: {} })

        return isExpectedStatus(res.status, 201);
    }
    async deleteTheater(id: number): Promise<boolean>
    {
        const uri = `${theaterUri}/${id}`;
        const res = await axios.delete(uri, { headers: {} })

        return isExpectedStatus(res.status, 201);
    }
}
export default new TheaterService ()