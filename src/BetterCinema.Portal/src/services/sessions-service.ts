import axios from 'axios';
import { GetSessionResponse } from '../contracts/session/GetSessionResponse';
import { GetSessionsResponse } from '../contracts/session/GetSessionsResponse';
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

class SessionService {
	async getSessions(theaterId: number, movieId: number): Promise<GetSessionsResponse>
	{
		const sessionsUrl = `${theaterUri}/${theaterId}/movies/${movieId}/sessions`;
		const response = await axios.get(sessionsUrl, { headers: {} });

		const sessions: GetSessionResponse[] = response.data.sessions.map((s: GetSessionResponse) => ({sessionId: s.sessionId, start: new Date(s.start), end: new Date(s.end), hall: s.hall}));

		const result: GetSessionsResponse = {
			sessions: sessions
		};
		return result;
	}
	async getSession(theaterId: number, movieId: number, sessionId: number): Promise<GetSessionResponse>
	{
		const sessionUrl = `${theaterUri}/${theaterId}/movies/${movieId}/sessions/${sessionId}`;
		const response = await axios.get(sessionUrl, { headers: {} });
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
export default new SessionService ();