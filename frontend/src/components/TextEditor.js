import React from "react";
import ReactDOM from "react-dom";
import { Editor, editorState, onEditorStateChange } from "react-draft-wysiwyg";
import "react-draft-wysiwyg/dist/react-draft-wysiwyg.css";

function TextEditor() {
  //   const [editorState, setEditorState] = React.useState(() =>
  //     EditorState.createEmpty()
  //   );
  return (
    <Editor
      editorState={editorState}
      toolbarClassName="toolbarClassName"
      wrapperClassName="wrapperClassName"
      editorClassName="editorClassName"
      onEditorStateChange={onEditorStateChange}
    ></Editor>
  );
}

export default TextEditor;
