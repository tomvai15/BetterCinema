import React from 'react';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import TheatersIcon from '@mui/icons-material/Theaters';
import Button from '@mui/material/Button';
import { useNavigate } from 'react-router-dom';
import { useAppSelector } from '../app/hooks';
import Box from '@mui/material/Box';
import Menu from '@mui/material/Menu';
import { logOutUser } from '../features/user-slice';
import MenuItem from '@mui/material/MenuItem';
import { useDispatch } from 'react-redux';

const Header = () => {
	const dispatch = useDispatch();
	const navigate = useNavigate();
	const user  = useAppSelector((state) => state.user);

	const [anchorElUser, setAnchorElUser] = React.useState<null | HTMLElement>(null);

	const handleOpenUserMenu = (event: React.MouseEvent<HTMLElement>) => {
		setAnchorElUser(event.currentTarget);
	};

	const handleCloseUserMenu = () => {
		setAnchorElUser(null);
	};

	function navigateToLoginPage() {
		navigate('/sign-in');
	}

	function isLoggedIn(): boolean
	{
		return Boolean(user.token);
	}

	function logOut(): void
	{
		dispatch(logOutUser());
	}

	return (
		<AppBar position="static">
			<Toolbar>
				<TheatersIcon sx={{ mr: 2 }} />
				<Typography onClick={()=>navigate('/home')} variant="h6" color="inherit" noWrap sx={{ pr:1, flexGrow: 1 }} >
					BetterCinema
				</Typography>
				{	
					user.role == 'Admin' &&
					<Typography onClick={()=>navigate('/users')} variant="h6" color="inherit" noWrap sx={{ pr:2 }} >
						Naudotojai
					</Typography>
				}
				{isLoggedIn() ?
					<Box sx={{ flexGrow: 0 }}>
						<Typography onClick={handleOpenUserMenu}  variant="h6" color="inherit" noWrap>
							{user.name}
						</Typography>
						<Menu
							sx={{ mt: '45px' }}
							id="menu-appbar"
							anchorEl={anchorElUser}
							anchorOrigin={{
								vertical: 'top',
								horizontal: 'right',
							}}
							keepMounted
							transformOrigin={{
								vertical: 'top',
								horizontal: 'right',
							}}
							open={Boolean(anchorElUser)}
							onClose={handleCloseUserMenu}
						>
							<MenuItem key={1} onClick={handleCloseUserMenu}>
								<Typography textAlign="center">Profilis</Typography>
							</MenuItem>
							<MenuItem key={2} onClick={logOut}>
								<Typography textAlign="center">Atsijungti</Typography>
							</MenuItem>
						</Menu>
					</Box>
					:<Button onClick={navigateToLoginPage} size='large' color="inherit">Login</Button>}
			</Toolbar>
		</AppBar>
	);
};

export default Header;