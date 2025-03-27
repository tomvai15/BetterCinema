import React from 'react';
import { Routes, Route, Outlet } from 'react-router-dom';
import './App.css';
import MovieInfo from './pages/Movie/MovieInfo';
import Movies from './pages/Movie/Movies';
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
import EditMovie from './pages/Movie/EditMovie';
import CreateSession from './pages/Session/CreateSession';
import Users from './pages/Users/Users';
import NotFound from './components/NotFound';
import ProtectedRoute from './components/ProtectedRoute';

const theme = createTheme();

function App() {
	return (
		<ThemeProvider theme={theme}>
			<Routes>
				<Route index element={<><Header/> <Home /><Footer /></>} />
				<Route path='/home' element={<><Header/> <Home /><Footer /></>}/>
				<Route path='/users' element={<><Header/> <Users /></>}/>
				<Route path='/theaters' element={<><Header/> <Outlet /><Footer /></>}>
					<Route index element={<Theaters/>}/>
					<Route path='create' element={<CreateTheater/>}/>
					<Route path=':theaterId'>
						<Route index element={ <TheaterInfo/>}/>
						<Route path='edit' element={<ProtectedRoute/>}>
							<Route index element={<EditTheater/>}/>
						</Route>
						<Route path='movies'>
							<Route index element={<Movies/>}/>
							<Route path='create' element={<ProtectedRoute/>}>
								<Route index element={<CreateMovie/>}/>
							</Route>
							<Route path=':movieId'>
								<Route index element={<MovieInfo/>}/>
								<Route path='edit' element={<ProtectedRoute/>}>
									<Route index element={<EditMovie/>}/>
								</Route>
								<Route path='sessions'>
									<Route index element={<Sessions/>}/>
									<Route path='create' element={<ProtectedRoute/>}>
										<Route index element={<CreateSession/>}/>
									</Route>
								</Route>
							</Route>
						</Route>
					</Route>
				</Route>
				<Route path='/sign-up' element={<><Header/> <SignUp /></>} />
				<Route path='/sign-in' element={<><Header/> <SignIn /></>} />
				<Route path='*' element={<><Header/> <NotFound /></>} />
			</Routes>
		</ThemeProvider>
	);
}

export default App;
