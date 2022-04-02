import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import IconButton from "@material-ui/core/IconButton";
import Typography from "@material-ui/core/Typography";
import FavoriteIcon from "@material-ui/icons/Favorite";
import React from "react";
import { Col, Row } from "react-bootstrap";
import CardHeader from "react-bootstrap/esm/CardHeader";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";
import styles from "./QuestionItem.module.css";

function QuestionItem(props) {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );
  const question = props.data;

  const history = useHistory();

  const getIndividualQuestion = (id) => {
    history.push(`/questionslibrary/${id}`);
  };

  const favouriteQuestionIconClick = (event) => {
    event.target.style.fill = "#664FBA";
  };
  return (
    <Card className={styles.root}>
      <CardHeader className={styles.header}>
        <Row>
          <Col>
            <p className={styles.topic}>{question.topic.value}</p>
          </Col>

          {isAuthenticated ? (
            <Col>
              <Typography align="right">
                <IconButton
                  className={styles.iconButton}
                  onClick={favouriteQuestionIconClick}
                >
                  <FavoriteIcon className={styles.icon} />
                </IconButton>
              </Typography>
            </Col>
          ) : (
            <div></div>
          )}
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
}

export default QuestionItem;
