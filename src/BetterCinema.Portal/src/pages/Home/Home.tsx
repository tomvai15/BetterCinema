import React from 'react';
import Paper from '@mui/material/Paper';
import Typography from '@mui/material/Typography';
import Grid from '@mui/material/Grid';
import Link from '@mui/material/Link';
import Box from '@mui/material/Box';
import Header from '../../components/Header';
import Container from '@mui/material/Container';
import { useNavigate } from 'react-router-dom';

interface MainFeaturedPostProps {
post: {
description: string;
image: string;
imageText: string;
linkText: string;
title: string;
};
}

export default function Home () {
	const navigate = useNavigate();

	function goToTheaters () {
		navigate('/theaters');
	}
	return <main>
		<Header/>
		<Box
			sx={{
				bgcolor: 'background.paper',
				pt: 8,
				pb: 6,
			}}>
		</Box>
		<Container maxWidth="lg">
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
							<Typography onClick={goToTheaters} component="h1" variant="h3" color="inherit" gutterBottom>
                                Peržiūrėti kino teatrus
							</Typography>
							<Typography variant="h5" color="inherit" paragraph>
                                test
							</Typography>
							<Link variant="subtitle1" href="#">
                                test
							</Link>
						</Box>
					</Grid>
				</Grid>
			</Paper>
		</Container>
	</main>;
}