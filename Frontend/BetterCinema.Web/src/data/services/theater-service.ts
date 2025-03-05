import { GetTheatersResponse } from '../contracts/GetTheatersResponse';
import { CreateTheaterRequest } from '../contracts/theater/CreateTheaterRequest';
import { UpdateTheaterRequest } from '../contracts/theater/UpdateTheaterRequest';
import { ConfirmTheaterRequest } from '../contracts/theater/ConfirmTheaterRequest';
import { BACKEND_URL } from '../constants';
import { Theater } from '../contracts/theater/Theater';
import { notFound } from 'next/navigation';

const theaterUri = BACKEND_URL+ '/theaters';

function isExpectedStatus(status: number, expectedStatus: number)
{
	return expectedStatus == status
}

function isSuccess(status: number)
{
	return 200 <= status && status < 300
}

class TheaterService {
	// async isOwnedTheater(id: number): Promise<boolean> {
	// 	const uri = `${theaterUri}/${id}`;
	// 	const response = await axios.get(uri, { headers: {} });

	// 	const theater: Theater = response.data;

	// 	return theater.userId == store.getState().user.userId;
	// }   

	async getTheaters (): Promise<GetTheatersResponse> {
		const response = await fetch(theaterUri);

		const dataResponse: GetTheatersResponse = await response.json()
		if (!dataResponse) {
			notFound()
		}
		return dataResponse
	}
	
	async getTheater(id: number): Promise<Theater> {
		const uri = `${theaterUri}/${id}`;
		const response = await fetch(uri);

		const dataResponse: Theater = await response.json()
		if (!dataResponse) {
			notFound()
		}
		return dataResponse
	}   

	// async addTheater(createTheaterRequest: CreateTheaterRequest): Promise<boolean> {
	// 	const res = await axios.post(theaterUri, createTheaterRequest);

	// 	return isExpectedStatus(res.status, 201);
	// } 

	async updateTheater(theaterId: number, updateTheaterRequest: UpdateTheaterRequest): Promise<boolean> {
		const uri = `${theaterUri}/${theaterId}`;
		const request = {
			method: "PUT",
			headers: { "Content-Type": "application/json" },
			body: JSON.stringify(updateTheaterRequest),
		};
		
		const response = await fetch(uri, request);

		return isSuccess(response.status);
	}

	// async confirmTheater(theaterId: number, confirmTheaterRequest: ConfirmTheaterRequest): Promise<boolean> {
	// 	const uri = `${theaterUri}/${theaterId}`;
	// 	const res = await axios.patch(uri, confirmTheaterRequest);

	// 	return isExpectedStatus(res.status, 204);
	// }

	// async deleteTheater(id: number): Promise<boolean> {
	// 	const uri = `${theaterUri}/${id}`;
	// 	const res = await axios.delete(uri);

	// 	return isExpectedStatus(res.status, 204);
	// }
}
export default new TheaterService ();