import axios from "axios";
import { Fragment, useEffect, useState } from "react";
import { Col } from "react-bootstrap";
import Question from "./Question";

const Questions = ({ topic }) => {
  const [questions, setQuestions] = useState([]);
  const [favourite, setFavourite] = useState(new Map());

  console.log(topic);
  const fetchFavourites = async () => {
    const res = await axios.get(`http://localhost:5000/favourites`);

    let temp = new Map();

    res.data.map((question) => temp.set(question.id, true));

    setFavourite(temp);
  };

  const getQuestions = async (topic) => {
    const questionsResult = await axios.get(
      `http://localhost:5000/topic/${topic}`
    );
    setQuestions(questionsResult.data);
  };

  useEffect(() => {
    fetchFavourites();
    getQuestions(topic);
  }, [topic]);

  return (
    <Fragment>
      {questions.map((question, index) => (
        <Col md={3} key={index}>
          <Question
            question={question}
            isFavourite={favourite.has(question.id)}
            fetchFavourites={fetchFavourites}
          />
        </Col>
      ))}
    </Fragment>
  );
};

export default Questions;
