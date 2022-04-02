import axios from "axios";
import { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import QuestionItem from "../../components/questionItem/QuestionItem";

const ProfilePage = (props) => {
  const columnNumber = 3;
  const [profile, setProfile] = useState("");
  const [favouriteQuestions, setFavouriteQuestions] = useState([]);
  const [respondedQuestions, setRespondedQuestions] = useState([]);

  // need to dynamically get user id with redux
  let id = "6248688a883d00ca573c9392";
  const getProfile = async () => {
    const res = await axios.get(`http://localhost:5000/api/users/${id}`);
    setProfile(res.data);
  };

  const getFavouriteQuestions = async () => {
    const res = await axios.get(
      `http://localhost:5000/api/users/${id}/favourites`
    );
    setFavouriteQuestions(res.data);
  };

  const getRespondedQuestions = async () => {
    const res = await axios.get(
      `http://localhost:5000/api/users/${id}/solutions`
    );
    setRespondedQuestions(res.data);
  };

  useEffect(() => {
    getProfile();
    getFavouriteQuestions();
    getRespondedQuestions();
  }, []);
  return (
    <div>
      <div>
        {props.data}
        <p>{profile.email}</p>
        <p>{profile.firstName}</p>
        <p>{profile.lastName}</p>
      </div>

      {/* get a question list of favourite questions here */}
      <Container>
        <Row>
          {favouriteQuestions.map((interviewQuestion, index) => (
            <Col md={columnNumber}>
              <QuestionItem data={interviewQuestion} key={index}></QuestionItem>
            </Col>
          ))}
        </Row>
      </Container>

      <Container>
        <Row>
          {respondedQuestions.map((interviewQuestion, index) => (
            <Col md={columnNumber}>
              <QuestionItem data={interviewQuestion} key={index}></QuestionItem>
            </Col>
          ))}
        </Row>
      </Container>
    </div>
  );
};
export default ProfilePage;
