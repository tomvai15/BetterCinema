import React from 'react';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';

const NotFound = () => {
	return (
		<main>
			<Container maxWidth="lg">
				<Typography component="h1" variant="h3" color="inherit" gutterBottom>
					{'Page not found :('}
				</Typography>
			</Container>
		</main>
	);
};

export default NotFound;