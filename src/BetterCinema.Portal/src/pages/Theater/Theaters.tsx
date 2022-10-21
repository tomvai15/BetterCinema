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

const Theaters = () => {

	const user  = useAppSelector((state) => state.user);
	const navigate = useNavigate();
	const [theaters, setTheaters] = useState<Theater[]>([]);
	
	useEffect(() => {		
		fetchTheaters();
	}, []);

	// methods
	async function fetchTheaters() {
		const response = await theaterService.getTheaters();
		setTheaters(response.theaters);
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
					pt: 2,
					pb: 6,
				}}
			>
			</Box>
			<Container sx={{ py: 1 }} maxWidth="md">
				{
					user.role == 'Owner' &&
					<Button onClick={()=>{navigate('/theaters/create');}}
						type="submit"
						variant="contained"
						sx={{ mt: 3, mb: 2 }}
					>
						Naujas teatras
					</Button>	
				}
				<Grid container spacing={4}>
					{ theaters.length > 0 ?
						theaters.map((theater: Theater) => (
							<Grid item key={theater.theaterId} xs={12} sm={6} md={4}>
								<Card
									sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
								>
									<CardMedia
										component="img"
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
									</CardActions>
								</Card>
							</Grid>
						))
						:
						<Typography gutterBottom variant="h5" component="h2">
							Nėra kino teatrų
						</Typography>
					}
				</Grid>
			</Container>
		</main>    
	);
};
export default Theaters;