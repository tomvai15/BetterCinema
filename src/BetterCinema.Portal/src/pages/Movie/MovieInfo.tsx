import React, {useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';
import Paper from '@mui/material/Paper';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';
import movieService from '../../services/movie-service';
import { GetMovieResponse } from '../../contracts/movie/GetMovieResponse';

const MovieInfo = () => {

	const navigate = useNavigate();
	const { theaterId, movieId } = useParams();

	const [movie, setMovie] = useState<GetMovieResponse>();

	useEffect(() => {		
		fetchMovie();
	}, []);

	// methods
	async function fetchMovie() {
		const response = await movieService.getMovie(Number(theaterId), Number(movieId));
		setMovie(response);
	}

	async function navigateToMovies() {
		navigate(`/theaters/${theaterId}/movies`);
	}

	async function navigateToSessions() {
		navigate(`/theaters/${theaterId}/movies/${movieId}/sessions`);
	}

	function getDate(): string {
		if  (movie?.releaseDate.toString()) {
			return movie?.releaseDate.toString();
		}
		return '2022-09-03';

	}

	return <main>
		<Box
			sx={{
				bgcolor: 'background.paper',
				pt: 2,
				pb: 6,
			}}>
		</Box>
		<Container maxWidth="lg">
			<Button onClick={navigateToMovies}
				type="submit"
				variant="contained"
				sx={{ mt: 3, mb: 2 }}
			>
				Grįžti atgal
			</Button>
			<Paper 
				sx={{
					borderRadius: '16px',
					position: 'relative',
					color: '#fff',
					mb: 4,
					backgroundSize: 'cover',
					backgroundRepeat: 'no-repeat',
					backgroundPosition: 'center',
					backgroundImage: 'url(https://source.unsplash.com/random)'
				}}
			>
				{<img style={{ display: 'none' }} src="https://media.npr.org/assets/img/2020/05/05/plazamarqueeduringclosure_custom-965476b67c1a760bdb3e16991ce8d65098605f62-s1100-c50.jpeg" alt="test" />}
				<Box
					sx={{
						position: 'absolute',
						top: 0,
						bottom: 0,
						right: 0,
						left: 0,
						backgroundColor: 'rgba(0,0,0,.3)',
					}}
				/>
				<Grid container>
					<Grid item md={6}>
						<Box
							sx={{
								position: 'relative',
								p: { xs: 3, md: 6 },
								pr: { md: 0 },
							}}
						>
						</Box>
					</Grid>
				</Grid>
			</Paper>
			<Grid container spacing={5} sx={{ mt: 3 }}>
				<Grid item xs={12} md={8}
					sx={{ '& .markdown': { py: 3 } }}
				>
					<Typography component="h1" variant="h3" color="inherit" gutterBottom>
						{movie?.title}
					</Typography>
					<Typography variant="subtitle1" fontSize={20} paragraph>
						{movie?.description}
					</Typography>
				</Grid>
				<Grid item xs={12} md={4}>
					<Paper elevation={0} sx={{ p: 2, bgcolor: 'grey.200' }}>
						<Typography variant="h6" gutterBottom>
							Išleidimo data
						</Typography>
						<Typography>
							{getDate()}
						</Typography>
						<Typography variant="h6" gutterBottom>
							Žanras
						</Typography>
						<Typography>{movie?.genre}</Typography>
					</Paper>
				</Grid>
			</Grid>
			<Button onClick={navigateToSessions}
				type="submit"
				variant="contained"
				sx={{ mt: 3, mb: 2 }}
			>
				Peržiūrėti seansus
			</Button>
		</Container>
	</main>;
};
export default MovieInfo;