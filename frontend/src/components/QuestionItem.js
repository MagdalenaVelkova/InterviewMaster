import Card from "@material-ui/core/Card";
import CardContent from "@material-ui/core/CardContent";
import IconButton from "@material-ui/core/IconButton";
import { makeStyles } from "@material-ui/core/styles";
import Typography from "@material-ui/core/Typography";
import FavoriteIcon from "@material-ui/icons/Favorite";
import React from "react";
import { Col, Row } from "react-bootstrap";
import CardHeader from "react-bootstrap/esm/CardHeader";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";

function QuestionItem(props) {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );
  const question = props.data;

  const useStyles = makeStyles({
    root: {
      background: "rgba(242, 240, 249)",
      marginTop: "0.3rem",
      marginBottom: "0.3rem",
      alignContents: "center",
    },
    title: {
      color: "#242A2D",
      textAlign: "center",
      alignContents: "center",
      fontWeight: "300",
      fontSize: "1.2rem",
    },
    topic: {
      fontSize: "0.9rem",
      paddingTop: "0.3rem",
      color: "#664FBA",
    },
    header: {
      paddingTop: 0,
      paddingBottom: 0,
      paddingLeft: "0.6rem",
      paddingRight: "0.6rem",
      backgroundColor: "rgba(189, 179, 230,0.3)",
      borderBottom: 0,
    },
    icon: {
      padding: "0.3rem",
      borderBottom: 0,
    },
    actionArea: {
      paddingRight: "0.3rem",
    },
    cardButton: {
      alignContents: "center",
      padding: "0",
    },
    cardContent: {
      cursor: "pointer",
    },
    icon: {
      fill: "rgba(189, 179, 230,1)",
    },
  });

  const classes = useStyles();
  const history = useHistory();

  const getIndividualQuestion = (id) => {
    history.push(`/questionslibrary/${id}`);
  };

  const favouriteQuestionIconClick = (event) => {
    event.target.style.fill = "#664FBA";
  };
  return (
    <Card className={classes.root}>
      <CardHeader className={classes.header}>
        <Row>
          <Col>
            <p className={classes.topic}>{question.topic.value}</p>
          </Col>

          {isAuthenticated ? (
            <Col>
              <Typography align="right">
                <IconButton
                  className={classes.iconButton}
                  onClick={favouriteQuestionIconClick}
                >
                  <FavoriteIcon className={classes.icon} />
                </IconButton>
              </Typography>
            </Col>
          ) : (
            <div></div>
          )}
        </Row>
      </CardHeader>
      <CardContent
        className={classes.cardContent}
        onClick={() => getIndividualQuestion(question.id)}
      >
        <div className={classes.sizer}>
          <Typography variant="h6" component="h2" className={classes.title}>
            {question.question}
          </Typography>
        </div>
      </CardContent>
    </Card>
  );
}

export default QuestionItem;
