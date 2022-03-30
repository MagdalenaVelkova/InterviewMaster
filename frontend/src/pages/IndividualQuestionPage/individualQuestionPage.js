import { CardContent } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import axios from "axios";
import "draft-js/dist/Draft.css";
import { useEffect, useState } from "react";
import CardHeader from "react-bootstrap/esm/CardHeader";
import { ReflexContainer, ReflexElement, ReflexSplitter } from "react-reflex";
import "react-reflex/styles.css";
import { useParams } from "react-router-dom";
import QuestionMenu from "../../components/QuestionMenu";
import TextEditor from "../../components/TextEditor";
import styles from "./IndividualQuestionPage.module.css";

const IndividualQuestionPage = () => {
  const [question, setQuestion] = useState([]);
  const [exampleAnswers, setExampleAnswers] = useState([]);
  const [questionPrompts, setQuestionPrompts] = useState([]);
  let { id } = useParams();

  const getQuestion = async () => {
    const res = await axios.get(`http://localhost:5000/api/questions/${id}`);
    setQuestion(res.data);
    setExampleAnswers(res.data.exampleAnswers);
    setQuestionPrompts(res.data.prompts);
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
                      <p>{answer.value}</p>
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
                      <p>{answer.value}</p>
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
