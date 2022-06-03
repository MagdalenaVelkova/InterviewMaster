import { IconButton } from "@material-ui/core";
import React from "react";
const QuestionMenuIconLink = ({ icon, action }) => {
  return (
    <IconButton color="primary" onClick={action}>
      {icon}
    </IconButton>
  );
};

export default QuestionMenuIconLink;
