import React, { useState } from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import userService from '../../services/user-service';
import { CreateUserRequest } from '../../contracts/auth/CreateUserRequest';
import { useNavigate } from 'react-router-dom';

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

function doPasswordsMatch(password: string, confirmPassword: string): boolean {
	return password == confirmPassword || confirmPassword == '';
}

export default function SignUp() {
	const navigate = useNavigate();

	const [email, setEmail] = useState<string>('');
	const [password, setPassword] = useState<string>('');
	const [confirmPassword, setConfirmPassword] = useState<string>('');
	const [error, setError] = useState<string>('');


	async function handleSubmit () {
		const createUserRequest: CreateUserRequest = {
			email: email,
			password: password
		};

		const isRegistered = await userService.register(createUserRequest);

		if (isRegistered) {
			navigate('/sign-in');
		} else {
			setError('El.paštas jau naudojamas');
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
        Sign up
				</Typography>
				<Box sx={{ mt: 3 }}>
					<Grid container spacing={2}>
						<Grid item xs={12} sm={6}>
							<TextField
								autoComplete="given-name"
								name="firstName"
								required
								fullWidth
								id="firstName"
								label="Vardas"
								autoFocus
							/>
						</Grid>
						<Grid item xs={12} sm={6}>
							<TextField 
								required
								fullWidth
								id="lastName"
								label="Pavardė"
								name="lastName"
								autoComplete="family-name"
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField onChange={(e) => setEmail(e.target.value)}
								required
								fullWidth
								id="email"
								label="El. Paštas"
								name="email"
								autoComplete="email"
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField onChange={(e) => setPassword(e.target.value)}
								required
								fullWidth
								name="password"
								label="Slaptažodis"
								type="password"
								id="password"
								error={false}
								helperText=""
								autoComplete="new-password"
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField onChange={(e) => setConfirmPassword(e.target.value)}
								required
								fullWidth
								name="password"
								label="Patvirtinti Slaptažodį"
								type="password"
								id="password"
								error={!doPasswordsMatch(password, confirmPassword)}
								helperText={!doPasswordsMatch(password, confirmPassword) ? 'Slaptažodžiai nesutampa' : ''}
								autoComplete="new-password"
							/>
						</Grid>
					</Grid>
					<Typography fontSize={20} color={'red'}>
						{error}
					</Typography>
					<Button onClick={handleSubmit}
						type="submit"
						fullWidth
						variant="contained"
						sx={{ mt: 3, mb: 2 }}
					>
            Sign Up
					</Button>
					<Grid container justifyContent="flex-end">
						<Grid item>
							<Link href="/sign-in" variant="body2">
                Already have an account? Sign in
							</Link>
						</Grid>
					</Grid>
				</Box>
			</Box>
			<Copyright sx={{ mt: 5 }} />
		</Container>
	);
}