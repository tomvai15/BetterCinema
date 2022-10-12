import axios from 'axios';
import { GetMovieResponse } from '../contracts/movie/GetMovieResponse';
import { GetMoviesResponse } from '../contracts/movie/GetMoviesResponse';
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

class MovieService {
	async getMovies(theaterId: number): Promise<GetMoviesResponse>
	{
		const moviesUrl = `${theaterUri}/${theaterId}/movies`;
		const response = await axios.get(moviesUrl, { headers: {} });
		return response.data;
	}
	async getMovie(theaterId: number, movieId: number): Promise<GetMovieResponse>
	{
		const moviesUrl = `${theaterUri}/${theaterId}/movies/${movieId}`;
		const response = await axios.get(moviesUrl, { headers: {} });
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
export default new MovieService ();