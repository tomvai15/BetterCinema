import React, { useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import sessionsService from '../../services/sessions-service';
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import { GetSessionResponse } from '../../contracts/session/GetSessionResponse';
import Stack from '@mui/material/Stack';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';
import { useAppSelector } from '../../app/hooks';
import theaterService from '../../services/theater-service';


const Sessions = () => {

	const user  = useAppSelector((state) => state.user);
	const navigate = useNavigate();
	const { theaterId, movieId } = useParams();

	const [open, setOpen] = React.useState(false);
	const [isOwnedTheater, setIsOwnedTheater] = useState<boolean>(false);
	const [sessionToDelete, setSessionToDelete] = React.useState<number>(0);
	const [sessions, setSessions] = useState<GetSessionResponse[]>([]);
	
	
	useEffect(() => {		
		fetchMovies();
		checkIfOwnedTheater();
	}, []);

	// methods
	async function fetchMovies() {
		const response = await sessionsService.getSessions(Number(theaterId), Number(movieId));
		setSessions(response.sessions);
	}

	async function checkIfOwnedTheater() {
		const isOwned = await theaterService.isOwnedTheater(Number(theaterId));
		setIsOwnedTheater(isOwned);
	}


	async function navigateToMovie() {
		navigate(`/theaters/${theaterId}/movies/${movieId}`);
	}

	async function deleteSession(sessionId: number) {
		const isDeleted = await sessionsService.deleteSession(Number(theaterId), Number(movieId), Number(sessionId));
		if (isDeleted) {
			fetchMovies();
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
					<Button onClick={navigateToMovie}
						type="submit"
						variant="contained"
					>
						Grįžti filmo informaciją
					</Button>
					{
						isOwnedTheater && user.role== 'Owner' &&
						<Button onClick={ ()=>{navigate(`/theaters/${theaterId}/movies/${movieId}/sessions/create`);}}
							type="submit"
							variant="contained"
						>
							Sukurti naują sesiją
						</Button>
					}
				</Stack>
				<Grid container spacing={4}>
					{ sessions.length!=0 ?
						sessions.map((session) => (
							<Grid item key={session.sessionId} xs={12} sm={6} md={4}>
								<Card
									sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
								>
									<CardContent sx={{ flexGrow: 1 }}>
										<Typography gutterBottom variant="h5" component="h2">
											{`Salė ${session.hall}`}
										</Typography>
										<Typography>
											{`Data: ${session.start.toISOString().split('T')[0]}`}
										</Typography>
										<Typography>
											{`Pradžia: ${session.start.getHours()}:${session.start.getMinutes()}`}
										</Typography>
										<Typography>
											{`Pabaiga: ${session.end.getHours()}:${session.end.getMinutes()}`}
										</Typography>
									</CardContent>
									<CardActions>
										<Stack direction={'row'} spacing={2}>
											{
												isOwnedTheater && user.role== 'Owner' &&
											<>
												<Button onClick={()=>navigate(`/theaters/${theaterId}/movies/${movieId}/sessions/${session.sessionId}/edit`)} color="success"
													type="submit"
													variant="contained"
												>
													Redaguoti
												</Button>
												<Button onClick={()=>{handleClickOpen();setSessionToDelete(session.sessionId);}} color="error"
													type="submit"
													variant="contained"
												>
													Pašalinti
												</Button>
											</>
											}
										</Stack>
									</CardActions>
								</Card>
							</Grid>
						)) :
						<Grid item sm={12} container justifyContent="center">
							<Typography variant="h5" component="h2">
								Nėra seansų
							</Typography>
						</Grid>
					}
					
				</Grid>
			</Container>
			<Dialog
				open={open}
				onClose={handleClose}
				aria-labelledby="alert-dialog-title"
				aria-describedby="alert-dialog-description"
			>
				<DialogTitle id="alert-dialog-title">
					{'Ar tikrai norite pašalinti filmą'}
				</DialogTitle>
				<DialogContent>
					<DialogContentText id="alert-dialog-description">
						Pašalinus filmą bus pašalinti visi jo seansai.
					</DialogContentText>
				</DialogContent>
				<DialogActions>
					<Button onClick={handleClose}>Atšaukti</Button>
					<Button onClick={() => {handleClose();deleteSession(sessionToDelete);}} autoFocus>Pašalinit</Button>
				</DialogActions>
			</Dialog>
		</main>    
	);
};
export default Sessions;