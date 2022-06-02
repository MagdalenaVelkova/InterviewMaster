import axios from "axios";
import { Fragment, useEffect, useState } from "react";
import Masonry from "react-masonry-css";
import Question from "./Question";
import styles from "./Questions.module.css";

const Questions = ({ topic }) => {
  const [questions, setQuestions] = useState([]);
  const [favourite, setFavourite] = useState(new Map());
  const [responded, setResponded] = useState(new Map());

  console.log(topic);
  const fetchFavourites = async () => {
    const res = await axios.get(`http://localhost:5000/favourites`);

    let temp = new Map();

    res.data.map((question) => temp.set(question.id, true));

    setFavourite(temp);
  };

  const fetchResponded = async () => {
    const res = await axios.get(`http://localhost:5000/solutions`);

    let temp = new Map();

    res.data.map((question) => temp.set(question.id, true));

    setResponded(temp);
  };

  const getQuestions = async (topic) => {
    const questionsResult = await axios.get(
      `http://localhost:5000/topic/${topic}`
    );
    setQuestions(questionsResult.data);
  };

  useEffect(() => {
    fetchFavourites();
    fetchResponded();
    getQuestions(topic);
  }, [topic]);
  const breakpointColumnsObj = {
    default: 5,
    1100: 3,
    700: 2,
    500: 1,
  };

  return (
    <Fragment>
      <Masonry
        breakpointCols={breakpointColumnsObj}
        className={styles.myMasonryGrid}
        columnClassName={styles.myMasonryGridColumn}
      >
        {questions.map((question, index) => (
          <Question
            question={question}
            isFavourite={favourite.has(question.id)}
            fetchFavourites={fetchFavourites}
            isResponded={responded.has(question.id)}
          />
        ))}
      </Masonry>
    </Fragment>
  );
};

export default Questions;
