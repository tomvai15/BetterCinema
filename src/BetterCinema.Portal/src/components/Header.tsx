import React from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import TheatersIcon from '@mui/icons-material/Theaters';

const Header = () => {
	return (
		<AppBar position="relative">
			<Toolbar>
				<TheatersIcon sx={{ mr: 2 }} />
				<Typography variant="h6" color="inherit" noWrap>
                BetterCinema
				</Typography>
			</Toolbar>
		</AppBar>
	);
};

export default Header;