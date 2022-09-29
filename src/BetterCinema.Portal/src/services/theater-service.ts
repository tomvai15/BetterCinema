import axios from 'axios';
import { GetTheatersResponse } from '../contracts/GetTheatersResponse';
import {Theater} from '../models/Theater';

const API_URL = process.env.REACT_APP_BACKEND;

const theaterUri = API_URL+ '/theaters';

function isExpectedStatus(status: number, expectedStatus: number)
{
	if (expectedStatus == status)
	{
		return true;
	}
	return false;
}

class TheaterService {
	async getTheaters (limit: number, offset: number): Promise<GetTheatersResponse>
	{
		const response = await axios.get(theaterUri,  { params: { limit: limit, offset: offset  }, headers: {} });
		return response.data;
	}
	async getTheater(id: number): Promise<Theater>
	{
		const uri = `${theaterUri}/${id}`;
		const response = await axios.get(uri, { headers: {} });

		return response.data;
	}    
	async addTheater(theater: Theater): Promise<boolean>
	{
		const res = await axios.post(theaterUri, {body: theater, headers: {} });

		return isExpectedStatus(res.status, 201);
	} 
	async updateTheater(theater: Theater): Promise<boolean>
	{
		const uri = `${theaterUri}/${theater.theaterId}`;
		const res = await axios.put(uri, {body: theater, headers: {} });

		return isExpectedStatus(res.status, 201);
	}
	async deleteTheater(id: number): Promise<boolean>
	{
		const uri = `${theaterUri}/${id}`;
		const res = await axios.delete(uri, { headers: {} });

		return isExpectedStatus(res.status, 201);
	}
}
export default new TheaterService ();