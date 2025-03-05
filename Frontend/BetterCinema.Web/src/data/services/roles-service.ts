import { BACKEND_URL } from '../constants';
import { notFound } from 'next/navigation';
import { GetUserPermissionsResponse } from '../contracts/Roles/Responses/GetUserPermissionsResponse';

const userPermissionsUri = BACKEND_URL+ '/roles/user-permissions';

function isSuccess(status: number)
{
	return 200 <= status && status < 300
}

class RolesService {
	async getPermissions (): Promise<GetUserPermissionsResponse> {
		const response = await fetch(userPermissionsUri);

		const dataResponse: GetUserPermissionsResponse = await response.json()
		if (!dataResponse) {
			notFound()
		}

		console.log(dataResponse);
		return dataResponse
	}
}
export default new RolesService ();