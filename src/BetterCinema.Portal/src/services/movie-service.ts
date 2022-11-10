import axios from 'axios';
import { CreateMovieRequest } from '../contracts/movie/CreateMovieRequest';
import { GetMovieResponse } from '../contracts/movie/GetMovieResponse';
import { GetMoviesResponse } from '../contracts/movie/GetMoviesResponse';
import { store } from '../app/store';
import { UpdateMovieRequest } from '../contracts/movie/UpdateMovieRequest';

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
	async addMovie(theaterId: number, createMovieRequest: CreateMovieRequest): Promise<boolean>
	{
		const moviesUrl = `${theaterUri}/${theaterId}/movies`;
		const res = await axios.post(moviesUrl, createMovieRequest);

		return isExpectedStatus(res.status, 201);
	} 
	async updateMovie(theaterId: number, movieId: number, updateMovieRequest: UpdateMovieRequest): Promise<boolean>
	{
		const moviesUrl = `${theaterUri}/${theaterId}/movies/${movieId}`;
		const res = await axios.patch(moviesUrl, updateMovieRequest, { headers: {} });

		return isExpectedStatus(res.status, 204);
	}
	async deleteMovie(theaterId: number, movieId: number): Promise<boolean>
	{
		const moviesUrl = `${theaterUri}/${theaterId}/movies/${movieId}`;
		const res = await axios.delete(moviesUrl, { headers: {} });

		return isExpectedStatus(res.status, 204);
	}
}
export default new MovieService ();