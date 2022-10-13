import { createSlice, PayloadAction } from '@reduxjs/toolkit';

interface User {
    token: string,
	name: string
}

const initialState: User = {
	token: '',
	name: 'Jonas'
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
		logOutUser(state) {
			state.token = '';
			state.name = '';
		},
	}
});

export const { setToken, logOutUser, setName} = userSlice.actions;
export default userSlice.reducer;
