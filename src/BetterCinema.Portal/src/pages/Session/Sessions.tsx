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

const cards = [1, 2, 3, 4];

const Sessions = () => {

	const navigate = useNavigate();
	const { theaterId, movieId } = useParams();

	const [sessions, setSessions] = useState<GetSessionResponse[]>([]);
	
	
	useEffect(() => {		
		fetchMovies();
	}, []);

	// methods
	async function fetchMovies() {
		const response = await sessionsService.getSessions(Number(theaterId), Number(movieId));
		console.log(response.sessions);
		setSessions(response.sessions);
	}

	function navigateToTheater () {
		navigate(`/theaters/${theaterId}`);
	}

	async function navigateToMovie() {
		navigate(`/theaters/${theaterId}/movies/${movieId}`);
	}

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
				<Button onClick={navigateToMovie}
					type="submit"
					variant="contained"
					sx={{ mt: 3, mb: 2 }}
				>
					Grįžti atgal
				</Button>
				<Grid container spacing={4}>
					{sessions.map((session) => (
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
									<Button size="small">Peržiūrėti</Button>
								</CardActions>
							</Card>
						</Grid>
					))}
				</Grid>
			</Container>
		</main>    
	);
};
export default Sessions;