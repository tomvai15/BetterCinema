import React from 'react';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import GitHubIcon from '@mui/icons-material/GitHub';
import IconButton from '@mui/material/IconButton';


const Footer = () => {
	return (
		<footer>
			<Box  bgcolor={'#E6F0FF'} sx={{ alignItems: 'center', minHeight: '18vh' }}>
				<Box sx={{ mt: 3, width: '10v', alignItems: 'center' }}>
					<Grid container spacing={2} >
						<Grid item xs={6} sm={6}>
							<IconButton href='https://github.com/tomvai15/BetterCinema'><GitHubIcon/></IconButton>
						</Grid>
						<Grid item xs={6} sm={6}>
							<IconButton href='https://github.com/tomvai15/BetterCinema'>
								<GitHubIcon/>
							</IconButton>
						</Grid>
						<Grid item xs={12}>
							<Typography textAlign="center">2022 Tomas Vainors</Typography>
						</Grid>
					</Grid>
				</Box>
			</Box>
		</footer>
	);
};

export default Footer;