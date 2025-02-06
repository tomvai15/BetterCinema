import React from 'react';
import Typography from '@mui/material/Typography';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import GitHubIcon from '@mui/icons-material/GitHub';
import IconButton from '@mui/material/IconButton';


const Footer = () => {
	return (
		<Box bgcolor={'#a2afb8'}  sx={{marginTop: 'calc(10% + 60px)',
			width: '100%',
			position: 'fixed',
			bottom: 0
		}}>
			<Grid  alignItems="center" container spacing={2} justifyContent="center" >
				<Grid item sm={3} container justifyContent="center">
					<IconButton href='https://github.com/tomvai15/BetterCinema' >
						<GitHubIcon/>
					</IconButton>
				</Grid>
				<Grid item xs={12}>
					<Typography textAlign="center">2022 Tomas Vainors</Typography>
				</Grid>
			</Grid>
		</Box>
	);
};

export default Footer;