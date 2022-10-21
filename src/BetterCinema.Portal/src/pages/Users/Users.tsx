import React, { useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';
import Stack from '@mui/material/Stack';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import usersService from '../../services/users-service';
import { GetUserResponse } from '../../contracts/user/GetUserResponse';


const Users = () => {

	const navigate = useNavigate();

	const [open, setOpen] = React.useState(false);
	const [userToDelete, setUserToDelete] = React.useState<number>(0);
	const [users, serUsers] = useState<GetUserResponse[]>([]);
	
	useEffect(() => {		
		fetchUsers();
	}, []);

	// methods
	async function fetchUsers() {
		const response = await usersService.getUsers();
		console.log(response);
		serUsers(response);
	}

	async function navigateToHome() {
		navigate('/home');
	}

	async function deleteSession() {
		const isDeleted = await usersService.deleteUser(userToDelete);
		if (isDeleted) {
			fetchUsers();
		}
	}

	const handleClickOpen = () => {
		setOpen(true);
	};
	
	const handleClose = () => {
		setOpen(false);
	};

	return (
		<main>
			<Box
				sx={{
					bgcolor: 'background.paper',
					pt: 2,
					pb: 6,
				}}
			>
			</Box>
			<Container sx={{ py: 1 }} maxWidth="md">
				<Stack direction={'row'} spacing={2} pb='5vh'>
					<Button onClick={navigateToHome}
						type="submit"
						variant="contained"
					>
						Grįžti į pradinį puslapį
					</Button>
				</Stack>
				<Grid container spacing={4}>
					{users.map((user) => (
						<Grid item key={user.userId} xs={12} sm={12} md={12}>
							<Card
								sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
							>
								<CardContent sx={{ flexGrow: 1 }}>
									<Typography gutterBottom variant="h5" component="h2">
										{`${user.name} ${user.surname}`}
									</Typography>
									<Typography>
										{`${user.email}`}
									</Typography>
									<Typography>
										{`${user.role}`}
									</Typography>
								</CardContent>
								<CardActions>
									{
										user.role!='Admin' &&
										<Stack direction={'row'} spacing={2}>
											<Button onClick={()=>console.log('stuff')} color="success"
												type="submit"
												variant="contained"
											>
												Pakeisti į administratorių
											</Button>
											<Button onClick={()=>{handleClickOpen();setUserToDelete(user.userId);}} color="error"
												type="submit"
												variant="contained"
											>
												Pašalinti
											</Button>
										</Stack>
									}	
								</CardActions>
							</Card>
						</Grid>
					))}
				</Grid>
			</Container>
			<Dialog
				open={open}
				onClose={handleClose}
				aria-labelledby="alert-dialog-title"
				aria-describedby="alert-dialog-description"
			>
				<DialogTitle id="alert-dialog-title">
					{'Ar tikrai norite pašalinti naudotoją'}
				</DialogTitle>
				<DialogContent>
					<DialogContentText id="alert-dialog-description">
						Pašalinus naudotoją bus pašalinti ir visi jam priklausantys kino teatrai.
					</DialogContentText>
				</DialogContent>
				<DialogActions>
					<Button onClick={handleClose}>Atšaukti</Button>
					<Button onClick={() => {handleClose();deleteSession();}} autoFocus>Pašalinit</Button>
				</DialogActions>
			</Dialog>
		</main>    
	);
};
export default Users;