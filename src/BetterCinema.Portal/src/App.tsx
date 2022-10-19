import React from 'react';
import { Routes, Route, Outlet } from 'react-router-dom';
import './App.css';
import MovieInfo from './pages/Movie/MovieInfo';
import Movies from './pages/Movie/Movies';
import Session from './pages/Session/Session';
import Sessions from './pages/Session/Sessions';
import TheaterInfo from './pages/Theater/TheaterInfo';
import Theaters from './pages/Theater/Theaters';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Header from './components/Header';
import SignUp from './pages/Authentication/SignUp';
import SignIn from './pages/Authentication/SignIn';
import Home from './pages/Home/Home';
import CreateTheater from './pages/Theater/CreateTheater';
import EditTheater from './pages/Theater/EditTheater';
import Footer from './components/Footer';
import CreateMovie from './pages/Movie/CreateMovie';

const theme = createTheme();

function App() {
	return (
		<ThemeProvider theme={theme}>
			<Routes>
				<Route index element={<><Header/> <Home /><Footer /></>} />
				<Route path='/home' element={<><Header/> <Home /></>}/>
				<Route path='/theaters' element={<><Header/> <Outlet /></>}>
					<Route index element={<Theaters/>}/>
					<Route path='create' element={<CreateTheater/>}/>
					<Route path=':theaterId'>
						<Route index element={<TheaterInfo/>}/>
						<Route path='edit' element={<EditTheater/>}/>
						<Route path='movies'>
							<Route index element={<Movies/>}/>
							<Route path='create' element={<CreateMovie/>}/>
							<Route path=':movieId'>
								<Route index element={<MovieInfo/>}/>
								<Route path='sessions'>
									<Route index element={<Sessions/>}/>
									<Route path=':sessionId' element={<Session/>}/>
								</Route>
							</Route>
						</Route>
					</Route>
				</Route>
				<Route path='/sign-up' element={<SignUp/>} />
				<Route path='/sign-in' element={<SignIn/>} />
			</Routes>
		</ThemeProvider>
	);
}

export default App;
