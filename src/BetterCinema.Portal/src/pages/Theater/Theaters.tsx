import React, { useEffect, useState } from 'react';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { Theater } from '../../models/Theater';
import theaterService from '../../services/theater-service';
import { useNavigate } from 'react-router-dom';
import { useAppSelector } from '../../app/hooks';
import Stack from '@mui/material/Stack';
import CircularProgress from '@mui/material/CircularProgress';

const Theaters = () => {

	const user  = useAppSelector((state) => state.user);
	const navigate = useNavigate();
	const [theaters, setTheaters] = useState<Theater[]>([]);
	const [isLoading, setIsLoading] = useState<boolean>(true);
	
	useEffect(() => {		
		fetchTheaters();
	}, []);

	// methods
	async function fetchTheaters() {
		const response = await theaterService.getTheaters();
		setTheaters(response.theaters);
		setIsLoading(false);
	}

	async function navigateToHome() {
		navigate('/home');
	}

	function navigateToTheater (id: number) {
		navigate(`/theaters/${id}`);
	}

	return (
		<main>	
			{/* Hero unit */}
			<Box
				sx={{
					bgcolor: 'background.paper',
					pt: 3,
					pb: 4,
				}}
			>
			</Box>
			<Container sx={{ py: 1 }} maxWidth="md">
				<Stack direction={'row'} spacing={2}>
					<Button onClick={navigateToHome}
						type="submit"
						variant="contained"
					>
							Grįžti į pradinį puslapį
					</Button>
					{
						user.role == 'Owner' &&
						<Button onClick={()=>{navigate('/theaters/create');}}
							type="submit"
							variant="contained"
						>
							Naujas teatras
						</Button>	
					}
				</Stack>
				<Grid sx={{ py: 4 }} container spacing={4}>
					{ theaters.length > 0 ?
						theaters.map((theater: Theater) => (
							<Grid item key={theater.theaterId} xs={12} sm={6} md={4}>
								<Card style={theater.userId == user.userId ? { border: '1px solid green' } : {}}
									sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
								>
									<CardMedia
										component="img"
										height='200'
										sx={{
											// 16:9
											pt: '10.25%',
										}}
										image={theater.imageUrl}
										alt="random"
									/>
									<CardContent sx={{ flexGrow: 1 }}>
										<Typography gutterBottom variant="h5" component="h2">
											{theater.name}
										</Typography>
										<Typography>
											{theater.description}
										</Typography>
									</CardContent>
									<CardActions>
										<Button onClick={()=>navigateToTheater(theater.theaterId)} size="small">Peržiūrėti</Button>
										{
											!theater.isConfirmed &&
											<Typography color={'red'}>Nepatvirtintas</Typography>
										}
									</CardActions>
								</Card>
							</Grid>
						))
						:
						<Grid item sm={12} container justifyContent="center">
							<Typography variant="h5" component="h2">
								{isLoading ? <CircularProgress /> : 'Nėra teatrų'}
							</Typography>
						</Grid>
					}
				</Grid>
			</Container>
		</main>    
	);
};
export default Theaters;