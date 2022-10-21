import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface User {
    token: string,
	name: string,
	role: string
}

const initialState: User = {
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
		logOutUser(state) {
			state.token = '';
			state.name = '';
			state.role = '';
		},
	}
});

export const { setToken, logOutUser, setName, setRole} = userSlice.actions;
export default userSlice.reducer;
