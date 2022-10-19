import React, {useEffect, useState} from 'react';
import { useParams } from 'react-router-dom';
import Paper from '@mui/material/Paper';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';
import theaterService from '../../services/theater-service';
import { Theater } from '../../models/Theater';
import Button from '@mui/material/Button';
import Stack from '@mui/material/Stack';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogContentText from '@mui/material/DialogContentText';
import DialogTitle from '@mui/material/DialogTitle';

const TheaterInfo = () => {
	const navigate = useNavigate();
	const { theaterId } = useParams();

	const [theater, setTheater] = useState<Theater>();
	const [open, setOpen] = React.useState(false);

	useEffect(() => {		
		fetchTheater();
	}, []);

	// methods
	async function fetchTheater() {
		const response = await theaterService.getTheater(Number(theaterId));
		setTheater(response);
	}

	async function navigateToTheaters() {
		navigate('/theaters');
	}

	async function navigateToMovies() {
		navigate(`/theaters/${theaterId}/movies`);
	}

	async function deleteTheater() {
		const isDeleted = await theaterService.deleteTheater(Number(theaterId));
		if (isDeleted) {
			navigate('/theaters');
		}
	}

	const handleClickOpen = () => {
		setOpen(true);
	};
	
	const handleClose = () => {
		setOpen(false);
	};

	return <main>
		<Box
			sx={{
				bgcolor: 'background.paper',
				pt: 2,
				pb: 6,
			}}>
		</Box>
		<Container maxWidth="lg">
			<Button onClick={navigateToTheaters}
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
					backgroundImage: `url(${theater?.imageUrl})`
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
						{theater?.name}
					</Typography>
					<Typography variant="subtitle1" fontSize={20} paragraph>
						{theater?.description}
					</Typography>
				</Grid>
				<Grid item xs={12} md={4}>
					<Paper elevation={0} sx={{ p: 2, bgcolor: 'grey.200' }}>
						<Typography variant="h6" gutterBottom>
							Adresas
						</Typography>
						<Typography>{theater?.address}</Typography>
					</Paper>
				</Grid>
			</Grid>
			<Stack direction={'row'} spacing={2}>
				<Button onClick={navigateToMovies}
					type="submit"
					variant="contained"
				>
					Peržiūrėti filmus
				</Button>
				<Button onClick={()=>navigate(`/theaters/${theaterId}/edit`)} color="success"
					type="submit"
					variant="contained"
				>
					Redaguoti
				</Button>
				<Button onClick={handleClickOpen} color="error"
					type="submit"
					variant="contained"
				>
					Pašalinti
				</Button>
			</Stack>
			<Dialog
				open={open}
				onClose={handleClose}
				aria-labelledby="alert-dialog-title"
				aria-describedby="alert-dialog-description"
			>
				<DialogTitle id="alert-dialog-title">
					{'Ar tikrai norite pašalinti kino teatrą?'}
				</DialogTitle>
				<DialogContent>
					<DialogContentText id="alert-dialog-description">
					Pašalinus kino teatrą bus pašalinti visi filmai ir jų seansai.
					</DialogContentText>
				</DialogContent>
				<DialogActions>
					<Button onClick={handleClose}>Atšaukti</Button>
					<Button onClick={() => {handleClose();deleteTheater();}} autoFocus>Pašalinit</Button>
				</DialogActions>
			</Dialog>
		</Container>
	</main>;
};
export default TheaterInfo;