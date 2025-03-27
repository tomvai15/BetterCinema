import axios from 'axios';
import { UpdateMovieRequest } from '../contracts/movie/UpdateMovieRequest';
import { CreateSessionRequest } from '../contracts/session/CreateSessionRequest';
import { GetSessionResponse } from '../contracts/session/GetSessionResponse';
import { GetSessionsResponse } from '../contracts/session/GetSessionsResponse';
import { store } from '../app/store';

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
	async addSession(theaterId: number, movieId: number, createSessionRequest: CreateSessionRequest): Promise<boolean>
	{
		const sessionUrl = `${theaterUri}/${theaterId}/movies/${movieId}/sessions`;
		const res = await axios.post(sessionUrl, createSessionRequest , { headers: {} });

		return isExpectedStatus(res.status, 201);
	} 
	async updateSession(theaterId: number, movieId: number, sessionId: number, updateMovieRequest: UpdateMovieRequest): Promise<boolean>
	{
		const sessionUrl = `${theaterUri}/${theaterId}/movies/${movieId}/sessions/${sessionId}`;
		const res = await axios.patch(sessionUrl, updateMovieRequest , { headers: {} });

		return isExpectedStatus(res.status, 201);
	} 
	async deleteSession(theaterId: number, movieId: number, sessionId: number): Promise<boolean>
	{
		const sessionUrl = `${theaterUri}/${theaterId}/movies/${movieId}/sessions/${sessionId}`;
		const res = await axios.delete(sessionUrl, { headers: {} });

		return isExpectedStatus(res.status, 204);
	}
}
export default new SessionService ();