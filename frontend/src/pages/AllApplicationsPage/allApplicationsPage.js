import axios from "axios";
import react from "react";

const AllApplicationsPage = (props) => {
  const [myApplications, setMyApplications] = react.useState("");

  const getMyApplications = async () => {
    const res = await axios.get("http://localhost:8000/api/my-applications");
    setMyApplications(res.data);
  };

  react.useEffect(() => {
    getMyApplications();
  }, []);
  return <div>{myApplications.id}</div>;
};
export default AllApplicationsPage;
