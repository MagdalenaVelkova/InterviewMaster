import axios from "axios";
import { convertFromRaw, convertToRaw, EditorState } from "draft-js";
import { useEffect, useState } from "react";
import { Container } from "react-bootstrap";
import * as reactDraftWysiwyg from "react-draft-wysiwyg";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import BaseButton from "./buttons/BaseButton";
const NewTextEditor = ({ interviewQuestionId }) => {
  const USER_ID = "6248688a883d00ca573c9392";
  const [userSolution, setUserSolution] = useState({});
  const [editorState, setEditorState] = useState(EditorState.createEmpty());

  const fetchSolution = async () => {
    const res = await axios.get(
      `http://localhost:5000/api/usersolutions/${interviewQuestionId}/${USER_ID}`
    );
    setUserSolution(res.data);
    setEditorState(
      EditorState.createWithContent(
        convertFromRaw(JSON.parse(res.data.response.value))
      )
    );
  };

  useEffect(() => {
    fetchSolution();
  }, []);

  const onEditorStateChange = (newState) => {
    setEditorState(newState);
  };

  const handleSubmit = async (event) => {
    console.log("handle sub");
    try {
      console.log(convertToRaw(editorState.getCurrentContent()));
      event.preventDefault();
      const rawResponse = JSON.stringify(
        convertToRaw(editorState.getCurrentContent())
      );
      const payload = {
        id: userSolution.id || "",
        userId: userSolution.userId,
        interviewQuestionId: userSolution.interviewQuestionId,
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
      <Container>
        <reactDraftWysiwyg.Editor
          editorState={editorState}
          wrapperClassName="wrapper-class"
          editorClassName="editor-class"
          toolbarClassName="toolbar-class"
          editorStyle={{
            marginLeft: "0.5rem",
            marginRight: "0.5rem",
            minHeight: "100%",
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
      </Container>
      <BaseButton text="Submit Response" clickHandler={handleSubmit} />
    </div>
  );
};
export default NewTextEditor;
