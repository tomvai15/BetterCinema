import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { LoginResponse } from '../contracts/auth/LoginResponse';

interface User {
	userId: number,
    token: string,
	name: string,
	role: string
}

const initialState: User = {
	userId: -1,
	token: '',
	name: '',
	role: ''
};

const userSlice = createSlice({
	name: 'user',
	initialState,
	reducers: {
		setToken(state, action: PayloadAction<string>) {
			state.token = action.payload;
		},
		setName(state, action: PayloadAction<string>) {
			state.name = action.payload;
		},
		setRole(state, action: PayloadAction<string>) {
			state.role = action.payload;
		},
		setUser(state, action: PayloadAction<LoginResponse>) {
			state.userId = action.payload.userId;
			state.name = action.payload.name;
			state.role = action.payload.role;
			state.token = action.payload.token;
		},
		logOutUser(state) {
			state.token = '';
			state.name = '';
			state.role = '';
			state.userId = -1;
		},
	}
});

export const { setToken, logOutUser, setName, setRole, setUser } = userSlice.actions;
export default userSlice.reducer;
