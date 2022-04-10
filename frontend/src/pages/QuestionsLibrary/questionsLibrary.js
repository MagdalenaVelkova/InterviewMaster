import React from "react";
import { Container, Row } from "react-bootstrap";
import Tabs from "../../components/tabs/BaseTabs";
import styles from "./QuestionsLibrary.module.css";

const QuestionsLibrary = () => {
  return (
    <div>
      <section className={styles.sectionTitle}>
        <Container className={styles.titleContainer}>
          <Row className={styles.row}>
            <h1 className={styles.mainHeadings}>Interview Questions Library</h1>
            <p className="lead text-center text-muted mb-6 mb-lg-8">
              Concquer all interviews and get on top of your employability game!
            </p>
          </Row>
        </Container>
      </section>
      <div className={styles.waveSection}></div>
      <section className={styles.sectionContent}>
        <Container>
          <Tabs />
        </Container>
      </section>
    </div>
  );
};
export default QuestionsLibrary;
