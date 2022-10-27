/* eslint-disable no-useless-escape */
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
import { CreateUserRequest } from '../../contracts/auth/CreateUserRequest';
import { useNavigate } from 'react-router-dom';

function doPasswordsMatch(password: string, confirmPassword: string): boolean {
	return password == confirmPassword || confirmPassword == '';
}

export default function SignUp() {
	const navigate = useNavigate();

	const [name, setName] = useState<string>('');
	const [surname, setSurname] = useState<string>('');
	const [email, setEmail] = useState<string>('');
	const [password, setPassword] = useState<string>('');
	const [confirmPassword, setConfirmPassword] = useState<string>('');
	const [error, setError] = useState<string>('');

	async function handleSubmit () {
		const createUserRequest: CreateUserRequest = {
			email: email,
			password: password,
			name: name,
			surname: surname
		};

		const isRegistered = await userService.register(createUserRequest);

		if (isRegistered) {
			navigate('/sign-in', {state: { message: 'Registracija buvo sėkminga. Prašome prisijungti'}});
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
        Registracija
				</Typography>
				<Box sx={{ mt: 3 }}>
					<Grid container spacing={2}>
						<Grid item xs={12} sm={6}>
							<TextField onChange={(e) => setName(e.target.value)}
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
							<TextField onChange={(e) => setSurname(e.target.value)}
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
								error={!/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(email)}
								helperText={!/[\w-\.]+@([\w-]+\.)+[\w-]{2,4}/.test(email) ? 'El. paštas nėra validus' : ''}
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
						disabled={!(name && surname && email && password && confirmPassword)}
						type="submit"
						fullWidth
						variant="contained"
						sx={{ mt: 3, mb: 2 }}
					>
            Registruotis
					</Button>
					<Grid container justifyContent="flex-end">
						<Grid item>
							<Link href="/sign-in" variant="body2">
								Jau turi paskyrą? Prisijunk
							</Link>
						</Grid>
					</Grid>
				</Box>
			</Box>
		</Container>
	);
}