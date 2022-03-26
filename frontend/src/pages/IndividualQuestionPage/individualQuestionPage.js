import { Container, Row, Col, Form, Button } from "react-bootstrap";
import Card from "@material-ui/core/Card";
import styles from "./styles.module.css";
import { useState, useEffect } from "react";
import axios from "axios";
import { useParams } from "react-router-dom";
import CardHeader from "react-bootstrap/esm/CardHeader";
import Typography from "@material-ui/core/Typography";
import { CardContent } from "@material-ui/core";
import TextEditor from "../../components/TextEditor";
import "draft-js/dist/Draft.css";
import { ReflexContainer, ReflexSplitter, ReflexElement } from "react-reflex";
import QuestionMenu from "../../components/QuestionMenu";

import "react-reflex/styles.css";

const IndividualQuestionPage = (props) => {
  const [question, setQuestion] = useState([]);
  const [exampleAnswers, setExampleAnswers] = useState([]);
  const [questionPrompts, setQuestionPrompts] = useState([]);
  let { id } = useParams();

  const getQuestion = async () => {
    const res = await axios.get(
      `http://localhost:8000/api/interview/question/${id}`
    );
    setQuestion(res.data);
    setExampleAnswers(res.data.example_answers);
    setQuestionPrompts(res.data.question_prompts);
  };

  useEffect(() => {
    getQuestion();
  }, []);
  return (
    <div>
      <section className={styles.mainSection}>
        <QuestionMenu></QuestionMenu>
        <ReflexContainer
          orientation="vertiverticalcal"
          className={styles.root}
          windowResizeAware="true"
        >
          <ReflexElement className="left-pane">
            <div className="pane-content">
              <Card className={styles.Cards + " " + "fluid"}>
                <CardHeader className={styles.cardHeaders} align="left">
                  <p className={styles.typographyHeadings}>Prompts</p>
                </CardHeader>
                <CardContent className={styles.rowOne}>
                  <h1>{question.question_title}</h1>
                  {questionPrompts.map((answer, index) => (
                    <div>
                      <h2>Tip {index + 1}:</h2>
                      <p>{answer}</p>
                    </div>
                  ))}
                </CardContent>
              </Card>
              <Card className={styles.Cards}>
                <CardHeader className={styles.cardHeaders}>
                  <p className={styles.typographyHeadings}>Example Answer</p>
                </CardHeader>
                <CardContent className={styles.rowTwo}>
                  {exampleAnswers.map((answer, index) => (
                    <div>
                      <h2>Example Answer {index + 1}:</h2>
                      <p>{answer}</p>
                    </div>
                  ))}
                </CardContent>
              </Card>
            </div>
          </ReflexElement>
          <ReflexSplitter className={styles.splitter} />
          <ReflexElement className="right-pane">
            <div className="pane-content">
              <Card className={styles.Cards}>
                <CardHeader className={styles.cardHeaders}>
                  <p className={styles.typographyHeadings}>Your Answer</p>
                </CardHeader>
                <CardContent className={styles.rowOne} style={{ padding: 0 }}>
                  <TextEditor></TextEditor>
                </CardContent>
              </Card>
              <Card className={styles.Cards}>
                <CardHeader className={styles.cardHeaders}>
                  <p className={styles.typographyHeadings}>
                    Your Bullet Points
                  </p>
                </CardHeader>
                <CardContent style={{ padding: 0 }} className={styles.rowTwo}>
                  <TextEditor></TextEditor>
                </CardContent>
              </Card>
            </div>
          </ReflexElement>
        </ReflexContainer>
      </section>
    </div>
  );
};
export default IndividualQuestionPage;
