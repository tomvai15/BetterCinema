import React, { useState } from 'react';
import Button from '@mui/material/Button';
import { useParams } from 'react-router-dom';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import { Dayjs } from 'dayjs';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import { CreateSessionRequest } from '../../contracts/session/CreateSessionRequest';
import sessionsService from '../../services/sessions-service';


const CreateSession = () => {

	const { theaterId, movieId } = useParams();

	const navigate = useNavigate();

	const [hall, setHall] = useState<string>('');
	const [start, setStart] = React.useState<Dayjs | null>(null);
	const [end, setEnd] = React.useState<Dayjs | null>(null);

	const [successMessage, setSuccessMessage] = useState<string>('');

	async function creteTheater() {
		const createSessionRequest: CreateSessionRequest = {
			start: start ? start.toISOString() : '',
			end: end ? end.toISOString() : '',
			hall: hall
		};
		const isCreated = await sessionsService.addSession(Number(theaterId), Number(movieId), createSessionRequest);

		if (isCreated) {
			setHall('');
			setSuccessMessage('Sesija buvo sukurta');
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
				<Button onClick={()=>{navigate(`/theaters/${theaterId}/movies/${movieId}/sessions`);}}
					type="submit"
					variant="contained"
					sx={{ mt: 3, mb: 2 }}
				>
					Grįžti į sesijų sąrašą
				</Button>
				<Typography  variant="h4">
					Naujas seansas
				</Typography>	
				<Box sx={{ mt: 3 }}>
					<Grid container spacing={2}>
						<Grid item xs={12} sm={6}>
							<TextField onChange={(e) => {setHall(e.target.value);setSuccessMessage('');}}
								required
								fullWidth
								label="Pavadinimas"
								autoFocus
								value={hall}
							/>
						</Grid>
						<Grid item xs={12}>
							<LocalizationProvider dateAdapter={AdapterDayjs}>
								<DatePicker
									label="Sesijos pradžia"
									value={start}
									onChange={(newValue) => {
										setStart(newValue);
									}}
									renderInput={(params) => <TextField {...params} />}
								/>
							</LocalizationProvider>	
						</Grid>	
						<Grid item xs={12}>
							<LocalizationProvider dateAdapter={AdapterDayjs}>
								<DatePicker
									label="Sesijos pabaiga"
									value={end}
									onChange={(newValue) => {
										setEnd(newValue);
									}}
									renderInput={(params) => <TextField {...params} />}
								/>
							</LocalizationProvider>	
						</Grid>		
					</Grid>
					<Typography fontSize={20} color={'red'}>
						{''}
					</Typography>
					<Typography fontSize={20} color={'green'}>
						{successMessage}
					</Typography>
					<Button onClick={creteTheater} disabled={!(hall)}
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
export default CreateSession;