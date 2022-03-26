import { useState, useEffect } from "react";
import axios from "axios";

const AllApplicationsPage = (props) => {
  const [myApplications, setMyApplications] = useState("");

  const getMyApplications = async () => {
    const res = await axios.get("http://localhost:8000/api/my-applications");
    setMyApplications(res.data);
  };

  useEffect(() => {
    getMyApplications();
  }, []);
  return <div>{myApplications.id}</div>;
};
export default AllApplicationsPage;
