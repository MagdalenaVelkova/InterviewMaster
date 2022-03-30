import axios from "axios";
import { useEffect, useState } from "react";

const ProfilePage = (props) => {
  const [profile, setProfile] = useState("");
  const [favouriteQuestions, setFavouriteQuestions] = useState([]);
  const [myApplications, setMyApplications] = useState([]);

  const getProfile = async () => {
    const res = await axios.get("http://localhost:8000/api/user/myprofile");
    setProfile(res.data);
  };

  const getFavouriteQuestions = async () => {
    const res = await axios.get(
      "http://localhost:8000/api/interview/favourites"
    );
    setFavouriteQuestions(res.data);
  };

  const getMyApplications = async () => {
    const res = await axios.get("http://localhost:8000/api/my-applications");
    setMyApplications(res.data);
  };

  useEffect(() => {
    getProfile();
    getFavouriteQuestions();
    getMyApplications();
  }, []);
  return (
    <div>
      <div>
        <p>{profile.email}</p>
        <p>{profile.first_name}</p>
        <p>{profile.last_name}</p>
      </div>

      {/* get a question list of favourite questions here */}
      <div>
        {favouriteQuestions.map((question) => (
          <p>{question.question_title}</p>
        ))}
      </div>
    </div>
  );
};
export default ProfilePage;
