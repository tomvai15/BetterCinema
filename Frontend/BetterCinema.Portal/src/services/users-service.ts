import axios from 'axios';
import { store } from '../app/store';
import { GetUserResponse } from '../contracts/user/GetUserResponse';
import { UpdateUserResponse } from '../contracts/user/UpdateUserResponse';

axios.interceptors.request.use(function (config) {
	const token = store.getState().user.token;
	if (!(config.headers && token)) return config;
	config.headers.Authorization =  `Bearer ${token}`;

	return config;
});

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

class UsersService {
	async getUsers(): Promise<GetUserResponse[]>
	{
		const response = await axios.get(userUri, { headers: {} });
		return response.data;
	}
	async updateUser(userId: number, updateUserResponse: UpdateUserResponse): Promise<boolean>
	{
		const uri = `${userUri}/${userId}`;
		const res = await axios.patch(uri, updateUserResponse);

		return isExpectedStatus(res.status, 204);
	}
	async deleteUser(userId: number): Promise<boolean>
	{
		const uri = `${userUri}/${userId}`;
		const res = await axios.delete(uri);

		return isExpectedStatus(res.status, 204);
	}
}
export default new UsersService ();