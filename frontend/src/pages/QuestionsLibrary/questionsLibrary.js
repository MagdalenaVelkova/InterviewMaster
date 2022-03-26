import { Container, Row, Col, Form, Button } from "react-bootstrap";
import styles from "./styles.module.css";
import { useState, useEffect } from "react";
import axios from "axios";
import QuestionListView from "../../components/QuestionListView";

const QuestionsLibrary = (props) => {
  const [questions, setQuestions] = useState([]);

  const getQuestions = async () => {
    const res = await axios.get(
      "http://localhost:8000/api/interview/questions"
    );
    setQuestions(res.data);
  };

  useEffect(() => {
    getQuestions();
  }, []);

  return (
    <div>
      <section className={styles.sectionTitle}>
        <Container className={styles.titleContainer}>
          {/* Row */}
          <Row className={styles.row}>
            <h1 className={styles.mainHeadings}>Interview Questions Library</h1>
            <p className="lead text-center text-muted mb-6 mb-lg-8">
              Concquer all interviews and get on top of your employability game!
            </p>
            {/* Row Ends */}
          </Row>
        </Container>
      </section>
      <div className={styles.waveSection}></div>
      <section className={styles.sectionContent}>
        <QuestionListView data={questions}></QuestionListView>
      </section>
    </div>
  );
};
export default QuestionsLibrary;
