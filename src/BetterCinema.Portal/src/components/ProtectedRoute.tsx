import React, { useEffect, useState } from 'react';
import {Navigate, Outlet, useParams} from 'react-router-dom';
import theaterService from '../services/theater-service';


const ProtectedRoute = () => {
	const { theaterId } = useParams();
	const [isOwnedTheater, setIsOwnedTheater] = useState<boolean>(false);
	
	useEffect(() => {		
		checkIfOwnedTheater();
	}, []);

	async function checkIfOwnedTheater() {
		const isOwned = await theaterService.isOwnedTheater(Number(theaterId));
		setIsOwnedTheater(isOwned);
	}

	return isOwnedTheater ? <Outlet/> : <Navigate to="/home"/>;
};

export default ProtectedRoute;