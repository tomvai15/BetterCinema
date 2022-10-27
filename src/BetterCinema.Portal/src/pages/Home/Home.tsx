import React from 'react';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';
import { Box, Button, styled, Typography } from '@mui/material';

const Home = () => {	

	const navigate = useNavigate();

	function goToTheaters () {
		navigate('/theaters');
	}

	const CustomBox = styled(Box)(({ theme }) => ({
		display: 'flex',
		justifyContent: 'center',
		gap: theme.spacing(5),
		marginTop: theme.spacing(3),
		[theme.breakpoints.down('md')]: {
			flexDirection: 'column',
			alignItems: 'center',
			textAlign: 'center',
		},
	}));
	
	const Title = styled(Typography)(({ theme }) => ({
		fontSize: '64px',
		color: '#000336',
		fontWeight: 'bold',
		margin: theme.spacing(4, 0, 4, 0),
		[theme.breakpoints.down('sm')]: {
			fontSize: '40px',
		},
	}));

	return <Box sx={{ mt:'5vh', mb:'10vh', backgroundColor: '#E6F0FF', minHeight: '70vh', pb: '10vh' }}>
		<Container>
			<CustomBox>
				<Box sx={{ flex: '1' }}>
					<Typography
						variant="body2"
						sx={{
							fontSize: '18px',
							color: '#687690',
							fontWeight: '500',
							mt: 10,
							mb: 4,
						}}
					>
						Sveikas atvykęs į BetterCinema
					</Typography>
					<Title variant="h1">
						Atrask naujus kino teatrus
					</Title>
					<Typography
						variant="body2"
						sx={{ fontSize: '18px', color: '#5A6473', my: 4 }}
					>
						Šioje svetainėje gali peržiūrėti įvairių kino teatrų siūlomus filmus ir seansus
					</Typography>
					<Button variant="contained" onClick={goToTheaters}>Peržiūrėti kino teatrus</Button>
				</Box>

				<Box sx={{ flex: '1.25' }}>
					<img
						src='https://cdni.iconscout.com/illustration/premium/thumb/cinema-movie-theater-4991884-4159602.png'
						alt="heroImg"
						style={{height:'100%',  marginBottom: '2rem' }}
					/>
				</Box>
			</CustomBox>
		</Container>
	</Box>;
};

export default Home;