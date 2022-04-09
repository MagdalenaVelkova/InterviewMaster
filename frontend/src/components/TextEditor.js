import axios from "axios";
import "easymde/dist/easymde.min.css";
import React, { useCallback, useEffect, useState } from "react";
import EasyMDE from "react-simplemde-editor";
import BaseButton from "./buttons/BaseButton";

export const TextEditor = ({ interviewQuestionId }) => {
  const USER_ID = "6248688a883d00ca573c9392";
  const [userSolution, setUserSolution] = useState({});
  const [text, setText] = useState("");

  const onChange = useCallback((value) => {
    setText(value);
  }, []);

  const handleSubmit = async (event) => {
    console.log("handle sub");
    try {
      event.preventDefault();
      const payload = {
        id: userSolution.id || "",
        userId: userSolution.userId,
        interviewQuestionId: userSolution.interviewQuestionId,
        response: text,
      };

      const res = await axios.post(
        `http://localhost:5000/api/usersolutions/`,
        payload
      );
    } catch (error) {
      console.error("submission failed", error);
    }
  };

  console.log("In Text Editor interviewQuestionId=", interviewQuestionId);

  const fetchSolution = async () => {
    const res = await axios.get(
      `http://localhost:5000/api/usersolutions/${interviewQuestionId}/${USER_ID}`
    );
    setUserSolution(res.data);
    setText(res.data.response.value);
  };

  useEffect(() => {
    fetchSolution();
  }, []);
  console.info(userSolution, text);
  return (
    <div>
      <EasyMDE value={text} onChange={onChange} />
      <BaseButton text="Submit Response" clickHandler={handleSubmit} />
    </div>
  );
};

export default TextEditor;
