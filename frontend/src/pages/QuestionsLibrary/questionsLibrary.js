import Tab from "@mui/material/Tab";
import Tabs from "@mui/material/Tabs";
import React from "react";
import { Container, Row } from "react-bootstrap";
import Questions from "./Questions";
import styles from "./QuestionsLibrary.module.css";
function a11yProps(index) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

const QuestionsLibrary = () => {
  const [value, setValue] = React.useState(0);
  const topics = [
    "All",
    "General",
    "Collaboration",
    "Problem Solving",
    "Adaptability",
    "Organisation",
  ];

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };

  return (
    <div>
      <section className={styles.sectionTitle}>
        <Container className={styles.titleContainer}>
          <Row className={styles.row}>
            <h1 className={styles.mainHeadings}>Interview Questions Library</h1>
            <p className="lead text-center text-muted mb-6 mb-lg-8">
              Conquer all interviews and get on top of your employability game!
            </p>
          </Row>
        </Container>
      </section>
      <div className={styles.waveSection}></div>
      <section className={styles.sectionContent}>
        <Container>
          <Row stlye={{ marginBottom: "2rem" }}>
            <Tabs value={value} onChange={handleChange} centered>
              {topics.map((topic, index) => (
                <Tab label={topic} {...a11yProps(0)} key={index} margin={10} />
              ))}
            </Tabs>
          </Row>

          <Questions topic={topics[value]} questions={questions}></Questions>
        </Container>
      </section>
    </div>
  );
};
export default QuestionsLibrary;
