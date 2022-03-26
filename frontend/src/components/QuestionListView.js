import React from "react";
import { Container, Row, Col } from "react-bootstrap";
import QuestionItem from "./QuestionItem";

function QuestionListView(props) {
  const columnNumber = 3;
  return (
    <Container>
      <Row>
        {props.data.map((question, index) => (
          <Col md={columnNumber}>
            <QuestionItem data={question} key={index}></QuestionItem>
          </Col>
        ))}
      </Row>
    </Container>
  );
}

export default QuestionListView;
