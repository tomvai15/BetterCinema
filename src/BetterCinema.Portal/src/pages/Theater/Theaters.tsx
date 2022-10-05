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

import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';

const Theaters = () => {

	const [theaters, setTheaters] = useState<Theater[]>([]);
	const [currentpage, setCurrentPage] = useState<number>(1);
	const [totalCount, setTotalCount] = useState<number>(0);
	const theatersPerPage = 5;
	
	useEffect(() => {		
		fetchTheaters(theatersPerPage, (currentpage-1)*theatersPerPage);
	}, [currentpage]);

	// methods
	async function fetchTheaters(limit: number, offset: number) {
		const response = await theaterService.getTheaters(limit, offset);
		setTotalCount(response.totalCount);
		setTheaters(response.theaters);
	}

	function onPageChange(e: React.ChangeEvent<any>, page:number)
	{
		setCurrentPage(page);
	}

	return (
		<main>			
			{/* Hero unit */}
			<Box
				sx={{
					bgcolor: 'background.paper',
					pt: 8,
					pb: 6,
				}}
			>
			</Box>
			<Container sx={{ py: 1 }} maxWidth="md">
				<Grid container justifyContent="center">
					<Stack spacing={2}>
						<Pagination onChange={onPageChange} page={currentpage} count={Math.ceil(totalCount/theatersPerPage)} variant="outlined" shape="rounded" />
					</Stack>
				</Grid>
				{/* End hero unit */}
				<Grid container spacing={4}>
					{theaters.map((theater) => (
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
									image="https://media.npr.org/assets/img/2020/05/05/plazamarqueeduringclosure_custom-965476b67c1a760bdb3e16991ce8d65098605f62-s1100-c50.jpeg"
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
									<Button size="small">Peržiūrėti</Button>
								</CardActions>
							</Card>
						</Grid>
					))}
				</Grid>
			</Container>
		</main>    
	);
};
export default Theaters;