import React, { useEffect, useState } from 'react';
import Button from '@mui/material/Button';
import { useParams } from 'react-router-dom';
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
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import Paper from '@mui/material/Paper';
import { CreateTheaterRequest } from '../../contracts/theater/CreateTheaterRequest';
import { UpdateTheaterRequest } from '../../contracts/theater/UpdateTheaterRequest';

const EditTheater = () => {
	const { theaterId } = useParams();
	const navigate = useNavigate();

	const [name, setName] = useState<string>('');
	const [address, setAddress] = useState<string>('');
	const [description, setDescription] = useState<string>('');
	const [imageUrl, setImageUrl] = useState<string>('');
	const [successMessage, setSuccessMessage] = useState<string>('');

	useEffect(() => {		
		fetchAndSetTheater();
	}, []);

	async function fetchAndSetTheater() {
		const theater = await theaterService.getTheater(Number(theaterId));
		setName(theater.name);
		setAddress(theater.address);
		setDescription(theater.description);
		setImageUrl(theater.imageUrl);
	}

	async function updateTheater() {
		const updateTheaterRequest: UpdateTheaterRequest = {
			name: name,
			description: address,
			address: description,
			imageUrl: imageUrl

		};
		const isCreated = await theaterService.updateTheater(Number(theaterId), updateTheaterRequest);

		if (isCreated) {
			setSuccessMessage('Kino tetras buvo atnaujintas');
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
				<Button onClick={()=>{navigate(`/theaters/${theaterId}`);}}
					type="submit"
					variant="contained"
					sx={{ mt: 3, mb: 2 }}
				>
					Grįžti į teatrų sąrašą
				</Button>
				<Typography  variant="h4">
					Redaguoti kino teatrą
				</Typography>	
				<Box sx={{ mt: 3 }}>
					<Grid container spacing={2}>
						<Grid item xs={12} sm={6}>
							<TextField onChange={(e) => {setName(e.target.value);setSuccessMessage('');}}
								required
								fullWidth
								label="Pavadinimas"
								autoFocus
								value={name}
							/>
						</Grid>
						<Grid item xs={12} sm={6}>
							<TextField onChange={(e) => {setAddress(e.target.value);setSuccessMessage('');}}
								required
								fullWidth
								label="Adresas"
								value={address}
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
							<TextField onChange={(e) => {setImageUrl(e.target.value);setSuccessMessage('');}}
								required
								fullWidth
								label="Paveikslėlio nuoroda"
								value={imageUrl}
							/>
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
					<Button onClick={updateTheater} disabled={!(name && description && address && imageUrl)}
						type="submit"						
						variant="contained"
						sx={{ mt: 3, mb: 2 }}
					>
						Atnaujinti
					</Button>					
				</Box>
			</Container>
		</main>    
	);
};
export default EditTheater;