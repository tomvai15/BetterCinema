import React from 'react';
import { Routes, Route } from 'react-router-dom';
import './App.css';
import Movie from './pages/Movie/Movie';
import Movies from './pages/Movie/Movies';
import Session from './pages/Session/Session';
import Sessions from './pages/Session/Sessions';
import Theater from './pages/Theater/Theater';
import Theaters from './pages/Theater/Theaters';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Header from './components/Header';

const theme = createTheme();

function App() {
	return (
		<ThemeProvider theme={theme}>
			<Header/>
			<Routes>
				<Route path='/theaters'>
					<Route index element={<Theaters/>}/>
					<Route path=':theaterId'>
						<Route index element={<Theater/>}/>
						<Route path='movies'>
							<Route index element={<Movies/>}/>
							<Route path=':movieId'>
								<Route index element={<Movie/>}/>
								<Route path='sessions'>
									<Route index element={<Sessions/>}/>
									<Route path=':sessionId' element={<Session/>}/>
								</Route>
							</Route>
						</Route>
					</Route>
				</Route>
			</Routes>
		</ThemeProvider>
	);
}

export default App;
