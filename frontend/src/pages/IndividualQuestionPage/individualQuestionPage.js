import { CardContent } from "@material-ui/core";
import Card from "@material-ui/core/Card";
import axios from "axios";
import "draft-js/dist/Draft.css";
import { useEffect, useState } from "react";
import CardHeader from "react-bootstrap/esm/CardHeader";
import { ReflexContainer, ReflexElement, ReflexSplitter } from "react-reflex";
import "react-reflex/styles.css";
import { useParams } from "react-router-dom";
import NewTextEditor from "../../common/NewTextEditor";
import styles from "./IndividualQuestionPage.module.css";
import QuestionMenu from "./QuestionMenu";

// https://www.npmjs.com/package/react-draft-wysiwyg
// https://stackoverflow.com/questions/51180361/react-draft-wysiwyg-render-saved-content-to-update
// https://joshtronic.com/2017/10/05/react-draft-wysiwyg-with-mongodb/
const IndividualQuestionPage = () => {
  let { interviewQuestionId } = useParams("");

  const [question, setQuestion] = useState({});
  const [exampleAnswers, setExampleAnswers] = useState([]);
  const [questionPrompts, setQuestionPrompts] = useState([]);

  const getQuestion = async () => {
    const res = await axios.get(
      `http://localhost:5000/api/questions/${interviewQuestionId}`
    );
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
          orientation="vertivertical"
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
                  <h1>{question.question}</h1>
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

                <NewTextEditor
                  interviewQuestionId={interviewQuestionId}
                ></NewTextEditor>
              </Card>
            </div>
          </ReflexElement>
        </ReflexContainer>
      </section>
    </div>
  );
};
export default IndividualQuestionPage;
