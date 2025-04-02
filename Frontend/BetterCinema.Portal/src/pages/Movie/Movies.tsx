import React, { useState, useEffect } from 'react';
import Button from '@mui/material/Button';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import movieService from '../../services/movie-service';
import { GetMovieResponse } from '../../contracts/movie/GetMovieResponse';
import { useNavigate } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import Stack from '@mui/material/Stack';
import { useAppSelector } from '../../app/hooks';
import theaterService from '../../services/theater-service';
import CircularProgress from '@mui/material/CircularProgress';

const Movies = () => {

	const user  = useAppSelector((state) => state.user);

	const navigate = useNavigate();
	const { theaterId } = useParams();

	const [isOwnedTheater, setIsOwnedTheater] = useState<boolean>(false);
	const [isLoading, setIsLoading] = useState<boolean>(true);
	const [movies, setTheaters] = useState<GetMovieResponse[]>([]);
	
	useEffect(() => {		
		fetchMovies();
	}, []);

	// methods
	async function fetchMovies() {
		const response = await movieService.getMovies(Number(theaterId));
		setTheaters(response.movies);
		await checkIfOwnedTheater();
	}

	async function checkIfOwnedTheater() {
		const isOwned = await theaterService.isOwnedTheater(Number(theaterId));
		setIsOwnedTheater(isOwned);
		setIsLoading(false);
	}

	function navigateToTheater () {
		navigate(`/theaters/${theaterId}`);
	}

	async function navigateToMovie(id: number) {
		navigate(`/theaters/${theaterId}/movies/${id}`);
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
				<Stack direction={'row'} spacing={2}>
					<Button onClick={navigateToTheater}
						type="submit"
						variant="contained"
					>
						Grįžti atgal
					</Button>
					{
						isOwnedTheater && user.role == 'Owner' &&
						<Button onClick={()=>{navigate(`/theaters/${theaterId}/movies/create`);}}
							type="submit"
							variant="contained"
						>
							Naujas filmas
						</Button>
					}
				</Stack>
				<Grid sx={{ py: 4 }} container spacing={4}>
					{
						movies.length != 0 ?
							movies.map((movie) => (
								<Grid item key={movie.id} xs={12} sm={6} md={4}>
									<Card 
										sx={{ height: '100%', display: 'flex', flexDirection: 'column'}}
									>
										<CardMedia
											height='200'
											component="img"
											sx={{
												16:9
											}}
											image={movie.imageUrl}
											alt="random"
										/>
										<CardContent sx={{ flexGrow: 1 }}>
											<Typography gutterBottom variant="h5" component="h2">
												{movie.title}
											</Typography>
											<Typography sx={{
												display: '-webkit-box',
												overflow: 'hidden',
												WebkitBoxOrient: 'vertical',
												WebkitLineClamp: 3,
											}}>
												{`${movie.description}`}
											</Typography>
										</CardContent>
										<CardActions>
											<Button onClick={()=>navigateToMovie(movie.id)} size="small">Peržiūrėti</Button>
										</CardActions>
									</Card>
								</Grid>
							))
							:
							<Grid item sm={12} container justifyContent="center">
								<Typography variant="h5" component="h2">
									{isLoading ? <CircularProgress /> : 'Nėra filmų'}
								</Typography>
							</Grid>
					}
				</Grid>
			</Container>
			<Box
				sx={{
					bgcolor: 'background.paper',
					pt: 10,
					pb: 10,
				}}
			>
			</Box>
		</main>
	);
};
export default Movies;