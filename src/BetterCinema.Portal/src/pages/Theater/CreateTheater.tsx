import React, { useState } from 'react';
import Button from '@mui/material/Button';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import theaterService from '../../services/theater-service';
import { useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import Paper from '@mui/material/Paper';
import { CreateTheaterRequest } from '../../contracts/theater/CreateTheaterRequest';

const CreateTheater = () => {
	const navigate = useNavigate();

	const [name, setName] = useState<string>('');
	const [address, setAddress] = useState<string>('');
	const [description, setDescription] = useState<string>('');
	const [imageUrl, setImageUrl] = useState<string>('');
	const [successMessage, setSuccessMessage] = useState<string>('');

	async function creteTheater() {
		const createTheaterRequest: CreateTheaterRequest = {
			name: name,
			description: address,
			address: description,
			imageUrl: imageUrl

		};
		const isCreated = await theaterService.addTheater(createTheaterRequest);

		if (isCreated) {
			setName('');
			setAddress('');
			setDescription('');
			setImageUrl('');
			setSuccessMessage('Kino tetras buvo sukurtas');
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
				<Button onClick={()=>{navigate('/theaters');}}
					type="submit"
					variant="contained"
					sx={{ mt: 3, mb: 2 }}
				>
					Grįžti į teatrų sąrašą
				</Button>
				<Typography  variant="h4">
					Naujas kino teatras
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
					<Button onClick={creteTheater} disabled={!(name && description && address && imageUrl)}
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
export default CreateTheater;