"use client"
import { GetTheatersResponse } from "@/data/contracts/GetTheatersResponse";
import { Theater } from "@/data/contracts/theater/Theater";
import theaterService from "@/data/services/theater-service";
import { useEffect, useState } from "react";

export default function Home() {
  const [theaters, setTheaters] = useState<GetTheatersResponse>({totalCount:0, theaters:[]});

  useEffect(() => {
    theaterService.getTheaters().then(x => setTheaters(x))
  }, [])

  return (
    <div>
      {theaters.theaters.map(x => <TheaterCard key={x.theaterId}  theater={x} />)}
    </div>
  );
}

type TheaterCardProps = {
  theater: Theater
}

function TheaterCard({theater}: TheaterCardProps) {
  return (
    <div>
      <p>{theater.name}</p>
      <p>{theater.description}</p>
    </div>
  );
}

