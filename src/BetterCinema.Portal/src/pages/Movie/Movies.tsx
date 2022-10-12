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

const cards = [1, 2, 3, 4];


const Movies = () => {

	const navigate = useNavigate();
	const { theaterId } = useParams();

	const [movies, setTheaters] = useState<GetMovieResponse[]>([]);
	
	
	useEffect(() => {		
		fetchMovies();
	}, []);

	// methods
	async function fetchMovies() {
		const response = await movieService.getMovies(Number(theaterId));
		setTheaters(response.movies);
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
				<Button onClick={navigateToTheater}
					type="submit"
					variant="contained"
					sx={{ mt: 3, mb: 2 }}
				>
					Grįžti atgal
				</Button>
				<Grid container spacing={4}>
					{movies.map((movie) => (
						<Grid item key={movie.movieId} xs={12} sm={6} md={4}>
							<Card
								sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}
							>
								<CardMedia
									component="img"
									sx={{
										// 16:9
										pt: '10.25%',
									}}
									image="https://media.npr.org/assets/img/2020/05/05/plazamarqueeduringclosure_custom-965476b67c1a760bdb3e16991ce8d65098605f62-s1100-c50.jpeg"
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
									<Button onClick={()=>navigateToMovie(movie.movieId)} size="small">Peržiūrėti</Button>
								</CardActions>
							</Card>
						</Grid>
					))}
				</Grid>
			</Container>
		</main>
	);
};
export default Movies;