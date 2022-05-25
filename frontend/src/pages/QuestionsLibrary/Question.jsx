import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import IconButton from "@material-ui/core/IconButton";
import Typography from "@material-ui/core/Typography";
import FavoriteIcon from "@material-ui/icons/Favorite";
import axios from "axios";
import React, { useState } from "react";
import { Col, Row } from "react-bootstrap";
import CardHeader from "react-bootstrap/esm/CardHeader";
import { useHistory } from "react-router";
import styles from "./QuestionItem.module.css";

const Question = ({ question, isFavourite, fetchFavourites }) => {
  const favouritedQuestion = "#F97D7D";
  const nonFavouritedQuestion = "#6C6A71";
  const [favourite, setFavourite] = useState(isFavourite);

  const favouriteQuestionIconClick = async (event) => {
    setFavourite(!favourite);

    if (favourite) {
      console.log(favourite);
      event.target.style.fill = nonFavouritedQuestion;
      {
        try {
          await axios.post(
            `http://localhost:5000/api/users/removefavourite/${question.id}`
          );
          fetchFavourites();
        } catch (error) {
          console.error("submission failed", error);
        }
      }
    } else {
      event.target.style.fill = favouritedQuestion;
      {
        try {
          await axios.post(
            `http://localhost:5000/api/users/addfavourite/${question.id}`,
            {
              questionId: question.id,
            }
          );
          fetchFavourites();
        } catch (error) {
          console.error("submission failed", error);
        }
      }
    }
  };

  const history = useHistory();
  const getIndividualQuestion = (id) => {
    history.push(`/questionslibrary/${id}`);
  };
  return (
    <Card className={styles.root}>
      <CardHeader className={styles.header}>
        <Row>
          <Col>
            <p className={styles.topic}>{question.topic.value}</p>
          </Col>
          <Col>
            <Typography align="right">
              <IconButton
                className={styles.iconButton}
                onClick={favouriteQuestionIconClick}
              >
                <FavoriteIcon
                  style={
                    isFavourite
                      ? { color: favouritedQuestion }
                      : { color: nonFavouritedQuestion }
                  }
                />
              </IconButton>
            </Typography>
          </Col>
        </Row>
      </CardHeader>
      <CardContent
        className={styles.cardContent}
        onClick={() => getIndividualQuestion(question.id)}
      >
        <div className={styles.sizer}>
          <Typography variant="h6" component="h2" className={styles.title}>
            {question.question}
          </Typography>
        </div>
      </CardContent>
    </Card>
  );
};

export default Question;
