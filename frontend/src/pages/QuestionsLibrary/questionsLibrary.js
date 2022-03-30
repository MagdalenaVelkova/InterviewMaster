import axios from "axios";
import React, { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import QuestionItem from "../../components/QuestionItem";
import styles from "./QuestionsLibrary.module.css";

const QuestionsLibrary = () => {
  const [questions, setQuestions] = useState([]);

  const getQuestions = async () => {
    const res = await axios.get("http://localhost:5000/api/questions");
    setQuestions(res.data);
  };

  useEffect(() => {
    getQuestions();
  }, []);

  const columnNumber = 3;

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
        <Container>
          <Row>
            {questions.map((question, index) => (
              <Col md={columnNumber}>
                <QuestionItem data={question} key={index}></QuestionItem>
              </Col>
            ))}
          </Row>
        </Container>
      </section>
    </div>
  );
};
export default QuestionsLibrary;
