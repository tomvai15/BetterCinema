import React, { useState } from 'react';
import Button from '@mui/material/Button';
import { useParams } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import Paper from '@mui/material/Paper';
import { CreateMovieRequest } from '../../contracts/movie/CreateMovieRequest';
import movieService from '../../services/movie-service';
import { Dayjs } from 'dayjs';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';

const CreateMovie = () => {

	const { theaterId } = useParams();

	const navigate = useNavigate();

	const [title, setTitle] = useState<string>('');
	const [director, setDirector] = useState<string>('');
	const [description, setDescription] = useState<string>('');
	const [genre, setGenre] = useState<string>('');
	const [imageUrl, setImageUrl] = useState<string>('');
	const [value, setValue] = React.useState<Dayjs | null>(null);
	const [successMessage, setSuccessMessage] = useState<string>('');

	async function creteTheater() {
		const createMovieRequest: CreateMovieRequest = {
			title: title,
			description: description,
			director: director,
			genre: genre,
			releaseDate: value ? value.toISOString() : '',
			imageUrl: imageUrl

		};
		const isCreated = await movieService.addMovie(Number(theaterId), createMovieRequest);

		if (isCreated) {
			setTitle('');
			setDirector('');
			setDescription('');
			setGenre('');
			setImageUrl('');
			setSuccessMessage('Filmas buvo sukurtas');
		}
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
				<Button onClick={()=>{navigate(`/theaters/${theaterId}/movies`);}}
					type="submit"
					variant="contained"
					sx={{ mt: 3, mb: 2 }}
				>
					Grįžti į filmų sąrašą
				</Button>
				<Typography  variant="h4">
					Naujas filmas
				</Typography>	
				<Box sx={{ mt: 3 }}>
					<Grid container spacing={2}>
						<Grid item xs={12} sm={6}>
							<TextField onChange={(e) => {setTitle(e.target.value);setSuccessMessage('');}}
								required
								fullWidth
								label="Pavadinimas"
								autoFocus
								value={title}
							/>
						</Grid>
						<Grid item xs={12} sm={6}>
							<TextField onChange={(e) => {setDirector(e.target.value);setSuccessMessage('');}}
								required
								fullWidth
								label="Režisierius"
								value={director}
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField onChange={(e) => {setDescription(e.target.value);setSuccessMessage('');}}
								multiline
								required
								fullWidth
								label="Aprašymas"
								value={description}
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField onChange={(e) => {setGenre(e.target.value);setSuccessMessage('');}}
								multiline
								required
								fullWidth
								label="Žanras"
								value={genre}
							/>
						</Grid>
						<Grid item xs={12}>
							<TextField onChange={(e) => {setImageUrl(e.target.value);setSuccessMessage('');}}
								required
								fullWidth
								label="Paveikslėlio nuoroda"
								value={imageUrl}
							/>
						</Grid>	
						<Grid item xs={12}>
							<LocalizationProvider dateAdapter={AdapterDayjs}>
								<DatePicker
									label="Išleidimo data"
									value={value}
									onChange={(newValue) => {
										setValue(newValue);
									}}
									renderInput={(params) => <TextField {...params} />}
								/>
							</LocalizationProvider>	
						</Grid>		
					</Grid>
					<Paper variant="outlined">
						<img src={imageUrl} />
					</Paper>
					<Typography fontSize={20} color={'red'}>
						{''}
					</Typography>
					<Typography fontSize={20} color={'green'}>
						{successMessage}
					</Typography>
					<Button onClick={creteTheater} disabled={!(title && description && director && genre && imageUrl)}
						type="submit"						
						variant="contained"
						sx={{ mt: 3, mb: 2 }}
					>
						Sukurti
					</Button>					
				</Box>
			</Container>
		</main>    
	);
};
export default CreateMovie;