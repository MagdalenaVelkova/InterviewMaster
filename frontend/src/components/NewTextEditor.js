import axios from "axios";
import { convertFromRaw, convertToRaw, EditorState } from "draft-js";
import { useEffect, useState } from "react";
import { Container } from "react-bootstrap";
import * as reactDraftWysiwyg from "react-draft-wysiwyg";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import BaseButton from "./buttons/BaseButton";
const NewTextEditor = ({ interviewQuestionId }) => {
  const [profile, setProfile] = useState("");
  const [userSolution, setUserSolution] = useState({});
  const [editorState, setEditorState] = useState(EditorState.createEmpty());

  const getProfile = async () => {
    const res = await axios.get(`http://localhost:5000/userprofile`);
    setProfile(res.data);
  };

  const fetchSolution = async () => {
    const res = await axios.get(
      `http://localhost:5000/api/usersolutions/${interviewQuestionId}/${profile.userId}`
    );
    setUserSolution(res.data);
    setEditorState(
      EditorState.createWithContent(
        convertFromRaw(JSON.parse(res.data.response.value))
      )
    );
  };

  useEffect(() => {
    getProfile();
  }, []);

  useEffect(() => {
    fetchSolution();
  }, [profile]);

  const onEditorStateChange = (newState) => {
    setEditorState(newState);
  };

  const handleSubmit = async (event) => {
    try {
      event.preventDefault();
      const rawResponse = JSON.stringify(
        convertToRaw(editorState.getCurrentContent())
      );
      const payload = {
        id: userSolution.id || "",
        userId: profile.userId,
        interviewQuestionId: interviewQuestionId,
        response: rawResponse,
      };

      const res = await axios.post(
        `http://localhost:5000/api/usersolutions/`,
        payload
      );
    } catch (error) {
      console.error("submission failed", error);
    }
  };

  return (
    <div>
      <reactDraftWysiwyg.Editor
        editorState={editorState}
        wrapperClassName="wrapper-class"
        editorClassName="editor-class"
        toolbarClassName="toolbar-class"
        editorStyle={{
          marginLeft: "0.5rem",
          marginRight: "0.5rem",
          minHeight: "66vh",
          color: "#ecf3fe",
        }}
        toolbarStyle={{
          backgroundColor: "#32353D",
          border: "0",
          margin: "0",
          paddingTop: "0",
          width: "100%",
        }}
        onEditorStateChange={onEditorStateChange}
      />
      <Container style={{ marginBottom: "1rem" }}>
        <BaseButton text="Submit Response" clickHandler={handleSubmit} />
      </Container>
    </div>
  );
};
export default NewTextEditor;
