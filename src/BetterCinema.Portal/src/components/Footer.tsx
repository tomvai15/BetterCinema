import React from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import TheatersIcon from '@mui/icons-material/Theaters';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';
import { useAppSelector } from '../app/hooks';
import Tooltip from '@mui/material/Tooltip';
import Box from '@mui/material/Box';
import Menu from '@mui/material/Menu';
import { logOutUser } from '../features/user-slice';
import MenuItem from '@mui/material/MenuItem';
import { useDispatch } from 'react-redux';
import Container from '@mui/material/Container';
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