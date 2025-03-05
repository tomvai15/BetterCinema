"use client"
import { AccessType } from "@/data/auth/AccessType";
import useAuth from "@/data/auth/hooks/useAuth";
import { ResourceType } from "@/data/auth/ResourceType";
import { Theater } from "@/data/contracts/theater/Theater";
import { UpdateTheaterRequest } from "@/data/contracts/theater/UpdateTheaterRequest";
import theaterService from "@/data/services/theater-service";
import { useParams } from "next/navigation";
import { FormEvent, useEffect, useMemo, useState } from "react";

const resource = ResourceType.Theater;

export default function Page() {
  const params = useParams();
  const [hasAccess] = useAuth();
  const [theater, setThearer] = useState<Theater>()

  const theaterId = useMemo(() => Number(params?.slug), [params?.slug])

  useEffect(() => {
    theaterService.getTheater(theaterId).then(x => setThearer(x))
  }, [])

  async function onSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault()
 
    const request: UpdateTheaterRequest = {
      ...theater!
    }
    theaterService.updateTheater(theaterId, request)
  }

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setThearer({ ...theater!, [e.target.name]: e.target.value });
  };

  if (theater == undefined) {
    return <p>Loading</p>
  }

  return (
    <div>
      {hasAccess(resource, AccessType.Create) 
        ? 
        <form onSubmit={onSubmit}>
          <input type="text" name="name" value={theater.name} onChange={handleChange} />
          <button type="submit">Submit</button>
        </form>
        :
        <p>Unauthorized ninja</p>
      }
    </div>
  );
}