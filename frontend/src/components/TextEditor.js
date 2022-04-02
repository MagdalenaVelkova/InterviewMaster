import { ContentState, convertFromHTML, EditorState } from "draft-js";
import React, { Component } from "react";
import * as reactDraftWysiwyg from "react-draft-wysiwyg";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";

class TextEditor extends Component {
  constructor(props) {
    super(props);
    this.state = {
      editorState: EditorState.createWithContent(
        ContentState.createFromBlockArray(
          convertFromHTML("<p>My initial content.</p>")
        )
      ),
    };
    // this.state.editorState = EditorState.createWithContent(
    // convertFromRaw(JSON.parse(this.props.rawState))
    // );
  }

  onEditorStateChange = (editorState) => {
    this.setState({
      editorState,
    });
  };

  render() {
    const { editorState } = this.state;
    return (
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
        }}
        onEditorStateChange={this.onEditorStateChange}
      />
    );
  }
}

export default TextEditor;
