import { Suspense } from "react";
import DataComponent from "./DataComponent";
import Loading from "./Loading";
import dynamic from 'next/dynamic';

const FancyComponent = dynamic(() => import('remote/FancyComponent'));

export default function Home() {
  return (
    <div>
      <h1>My Page</h1>
      <Suspense fallback={<Loading />}>
        <DataComponent />
      </Suspense>
      <FancyComponent></FancyComponent>
    </div>
  );
}
