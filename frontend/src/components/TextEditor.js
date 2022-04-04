import axios from "axios";
import { convertFromRaw, convertToRaw, EditorState } from "draft-js";
import React, { Component } from "react";
import { Container } from "react-bootstrap";
import * as reactDraftWysiwyg from "react-draft-wysiwyg";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";
import BaseButton from "./buttons/BaseButton";

class TextEditor extends Component {
  constructor({ id, userId, interviewQuestionId, response }) {
    super(id, userId, interviewQuestionId, response);
    var example = {
      blocks: [
        {
          key: "3g264",
          text: "Hi, little bitch!",
          type: "unstyled",
          depth: 0,
          inlineStyleRanges: [],
          entityRanges: [],
          data: {},
        },
      ],
      entityMap: {},
    };
    // change write your response here to a valid alternative
    console.info(id);
    this.state = {
      editorState: EditorState.createWithContent(convertFromRaw(example)),
    };
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  onEditorStateChange = (editorState) => {
    this.setState({
      editorState,
    });
  };

  async handleSubmit(event) {
    try {
      event.preventDefault();
      const rawResponse = JSON.stringify(
        convertToRaw(this.state.editorState.getCurrentContent())
      );
      const payload = {
        id: this.props.id || "",
        userId: this.props.userId,
        interviewQuestionId: this.props.interviewQuestionId,
        response: rawResponse,
      };

      const res = await axios.post(
        `http://localhost:5000/api/usersolutions/`,
        payload
      );
    } catch (error) {
      console.error("submission failed", error);
    }
  }

  render() {
    const { editorState } = this.state;
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
            onEditorStateChange={this.onEditorStateChange}
          />
        </Container>
        {JSON.stringify(convertToRaw(editorState.getCurrentContent()))}
        <BaseButton text="Submit Response" clickHandler={this.handleSubmit} />
      </div>
    );
  }
}

export default TextEditor;
