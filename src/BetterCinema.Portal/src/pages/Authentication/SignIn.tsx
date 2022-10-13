import React, { useState } from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import userService from '../../services/user-service';
import { useNavigate } from 'react-router-dom';
import { LoginRequest } from '../../contracts/auth/LoginRequest';
import { setToken, setName } from '../../features/user-slice';
import { useAppDispatch } from '../../app/hooks';


function Copyright(props: any) {
	return (
		<Typography variant="body2" color="text.secondary" align="center" {...props}>
			{'Copyright © '}
			<Link color="inherit" href="https://mui.com/">
        Your Website
			</Link>{' '}
			{new Date().getFullYear()}
			{'.'}
		</Typography>
	);
}

export default function SignIn() {
	const dispatch = useAppDispatch();
	const navigate = useNavigate();

	const [email, setEmail] = useState<string>('');
	const [password, setPassword] = useState<string>('');
	const [error, setError] = useState<string>('');

	async function handleSubmit () {
		const loginUserRequest: LoginRequest = {
			email: email,
			password: password
		};
		
		const loginResponse = await userService.login(loginUserRequest);

		if (loginResponse.token) {
			dispatch(setToken(loginResponse.token));
			dispatch(setName(loginResponse.name));
			navigate('/theaters');
		} else {
			setError('El.paštas arba slaptažodis yra netesingas');
		}
	}
	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
			<Box
				sx={{
					marginTop: 8,
					display: 'flex',
					flexDirection: 'column',
					alignItems: 'center',
				}}
			>
				<Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Sign in
				</Typography>
				<Box sx={{ mt: 1 }}>
					<TextField onChange={(e) => setEmail(e.target.value)}
						margin="normal"
						required
						fullWidth
						id="email"
						label="El. Paštas"
						name="email"
						autoComplete="email"
						autoFocus
					/>
					<TextField onChange={(e) => setPassword(e.target.value)}
						margin="normal"
						required
						fullWidth
						name="password"
						label="Slaptažodis"
						type="password"
						id="password"
						autoComplete="current-password"
					/>
					<Typography fontSize={20} color={'red'}>
						{error}
					</Typography>
					<Button onClick={handleSubmit}
						type="submit"
						fullWidth
						variant="contained"
						sx={{ mt: 3, mb: 2 }}
					>
						Sign In
					</Button>
					<Grid container>
						<Grid item xs>
						</Grid>
						<Grid item>
							<Link href="/sign-up" variant="body2">
								{'Don\'t have an account? Sign Up'}
							</Link>
						</Grid>
					</Grid>
				</Box>
			</Box>
			<Copyright sx={{ mt: 8, mb: 4 }} />
		</Container>
	);
}