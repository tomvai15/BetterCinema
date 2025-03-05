async function DataComponent() {
    const data = await fetch("https://jsonplaceholder.typicode.com/posts/1").then(res => res.json());
  
    return <div>{data.title}</div>;
  }
  
  export default DataComponent;