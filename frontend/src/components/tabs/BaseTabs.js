import Box from "@mui/material/Box";
import Tab from "@mui/material/Tab";
import Tabs from "@mui/material/Tabs";
import axios from "axios";
import React, { useEffect, useState } from "react";
import { Col, Row } from "react-bootstrap";
import QuestionItem from "../questionItem/QuestionItem";

function TabPanel(props) {
  const { value, index, topic, ...other } = props;
  const [questions, setQuestions] = useState([]);
  const [profile, setProfile] = useState({});

  const getQuestions = async (topic) => {
    const res = await axios.get(`http://localhost:5000/topic/${topic}`);
    setQuestions(res.data);
  };

  const getProfile = async () => {
    const res = await axios.get(`http://localhost:5000/userprofile`);
    setProfile(res.data);
  };

  useEffect(() => {
    getProfile();
  }, []);

  useEffect(() => {
    getQuestions(topic);
  }, []);

  const columnNumber = 3;
  console.log("value=", value, "index=", index, "questions=", questions);
  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      <Row>
        {questions.map((question, index) => (
          <Col md={columnNumber} key={index}>
            <QuestionItem
              question={question}
              userId={profile.userId}
              favouriteQuestions={profile.favouriteQuestions}
              key={index}
            ></QuestionItem>
          </Col>
        ))}
      </Row>
    </div>
  );
}

function a11yProps(index) {
  return {
    id: `simple-tab-${index}`,
    "aria-controls": `simple-tabpanel-${index}`,
  };
}

export default function BaseTabs() {
  const [value, setValue] = React.useState(0);

  const handleChange = (event, newValue) => {
    setValue(newValue);
  };
  const topics = [
    "All",
    "General",
    "Collaboration",
    "Problem Solving",
    "Adaptability",
  ];

  console.log("value=", value);
  return (
    <Box sx={{ width: "100%" }}>
      <Box
        sx={{ borderBottom: 1, borderColor: "divider", marginBottom: "2rem" }}
      >
        <Tabs
          value={value}
          onChange={handleChange}
          aria-label="basic tabs"
          centered
        >
          {topics.map((topic, index) => (
            <Tab label={topic} {...a11yProps(index)} />
          ))}
        </Tabs>
      </Box>
      {topics.map((topic, index) => (
        <TabPanel value={value} index={index} topic={topic}></TabPanel>
      ))}
    </Box>
  );
}
