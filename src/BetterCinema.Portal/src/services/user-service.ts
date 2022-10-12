import axios, { AxiosError } from 'axios';
import { CreateUserRequest } from '../contracts/auth/CreateUserRequest';
import { LoginRequest } from '../contracts/auth/LoginRequest';
import { LoginResponse } from '../contracts/auth/LoginResponse';

const API_URL = process.env.REACT_APP_BACKEND;

const userUri = API_URL+ '/users';

function isExpectedStatus(status: number, expectedStatus: number)
{
	if (expectedStatus == status)
	{
		return true;
	}
	return false;
}

class UserService {
	async register(createUserRequest: CreateUserRequest): Promise<boolean>
	{
		try {
			console.log(createUserRequest);
			const res = await axios.post(userUri, createUserRequest, {headers:{'Content-Type': 'application/json'}}); 
			return isExpectedStatus(res.status, 204);
		} catch (err) {
			if (err instanceof AxiosError) {
				console.log(err.response?.data.message);
			}
			return false;
		}
	} 
	async login(loginRequest: LoginRequest): Promise<LoginResponse>
	{
		let loginResponse: LoginResponse = { token:'', userName: ''};
		const loginUrl = `${userUri}/token`;
		try {
			const res = await axios.post(loginUrl, loginRequest, {headers:{'Content-Type': 'application/json'}}); 
			loginResponse = res.data;
			if (isExpectedStatus(res.status, 200)) {
				return loginResponse;
			} else {
				return loginResponse;
			}
		} catch (err) {
			if (err instanceof AxiosError) {
				console.log(err.response?.data.message);
			}
			return loginResponse;
		}
	} 
}
export default new UserService ();