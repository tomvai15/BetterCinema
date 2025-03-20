import { Suspense } from "react";
import DataComponent from "./DataComponent";
import Loading from "./Loading";
import FancyComponent from "@/components/FancyComponent";

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
