import axios, { AxiosError } from 'axios';
import { CreateUserRequest } from '../contracts/auth/CreateUserRequest';

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
}
export default new UserService ();