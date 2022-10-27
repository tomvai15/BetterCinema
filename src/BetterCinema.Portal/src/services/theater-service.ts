import axios from 'axios';
import { GetTheatersResponse } from '../contracts/GetTheatersResponse';
import { CreateTheaterRequest } from '../contracts/theater/CreateTheaterRequest';
import {Theater} from '../models/Theater';
import { store } from '../app/store';
import { UpdateTheaterRequest } from '../contracts/theater/UpdateTheaterRequest';
import { ConfirmTheaterRequest } from '../contracts/theater/ConfirmTheaterRequest';

axios.interceptors.request.use(function (config) {
	const token = store.getState().user.token;
	if (!(config.headers && token)) return config;
	config.headers.Authorization =  `Bearer ${token}`;

	return config;
});

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

	async isOwnedTheater(id: number): Promise<boolean>
	{
		const uri = `${theaterUri}/${id}`;
		const response = await axios.get(uri, { headers: {} });

		const theater: Theater = response.data;

		return theater.userId == store.getState().user.userId;
	}   

	async getTheaters (): Promise<GetTheatersResponse>
	{
		const response = await axios.get(theaterUri, { headers: {} });
		return response.data;
	}
	async getTheater(id: number): Promise<Theater>
	{
		const uri = `${theaterUri}/${id}`;
		const response = await axios.get(uri, { headers: {} });

		return response.data;
	}    
	async addTheater(createTheaterRequest: CreateTheaterRequest): Promise<boolean>
	{
		const res = await axios.post(theaterUri, createTheaterRequest);

		return isExpectedStatus(res.status, 201);
	} 
	async updateTheater(theaterId: number, updateTheaterRequest: UpdateTheaterRequest): Promise<boolean>
	{
		const uri = `${theaterUri}/${theaterId}`;
		const res = await axios.patch(uri, updateTheaterRequest);

		return isExpectedStatus(res.status, 204);
	}
	async confirmTheater(theaterId: number, confirmTheaterRequest: ConfirmTheaterRequest): Promise<boolean>
	{
		const uri = `${theaterUri}/${theaterId}`;
		const res = await axios.patch(uri, confirmTheaterRequest);

		return isExpectedStatus(res.status, 204);
	}
	async deleteTheater(id: number): Promise<boolean>
	{
		const uri = `${theaterUri}/${id}`;
		const res = await axios.delete(uri);

		return isExpectedStatus(res.status, 204);
	}
}
export default new TheaterService ();