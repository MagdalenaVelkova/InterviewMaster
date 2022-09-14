import AccountCircleRoundedIcon from "@material-ui/icons/AccountCircleRounded";
import ArrowBackRoundedIcon from "@material-ui/icons/ArrowBackRounded";
import ArrowForwardRoundedIcon from "@material-ui/icons/ArrowForwardRounded";
import ExitToAppRoundedIcon from "@material-ui/icons/ExitToAppRounded";
import ListRoundedIcon from "@material-ui/icons/ListRounded";
import React from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router";
import { LinkContainer } from "react-router-bootstrap";
import { logoutUser } from "../../redux/actions";
import QuestionMenuIconLink from "./QuestionsMenuIconLink";

const QuestionMenu = ({
  questionIds,
  questionId,
  setPrevious,
  setNext,
  setFavourite,
}) => {
  let dispatch = useDispatch();
  const history = useHistory();

  const handleQuestionsLibrary = () => {
    history.push(`/questionslibrary`);
  };

  const handlePreviousQuestion = () => {
    let previous = setPrevious();
    history.push({
      pathname: `/questionslibrary/${previous}`,
      state: { questionIds: questionIds },
    });
    history.go(0);
  };

  const handleNextQuestion = () => {
    let next = setNext();
    history.push({
      pathname: `/questionslibrary/${next}`,
      state: { questionIds: questionIds },
    });
    history.go(0);
  };

  const handleMyProfile = () => {
    history.push(`/myprofile`);
  };

  const handleLogOut = () => {
    dispatch(logoutUser());
    history.push(`/`);
  };
  const leftIconActions = [
    {
      icon: <ListRoundedIcon style={{ color: "white" }} />,
      action: handleQuestionsLibrary,
    },
    {
      icon: <ArrowBackRoundedIcon style={{ color: "white" }} />,
      action: handlePreviousQuestion,
    },
    {
      icon: <ArrowForwardRoundedIcon style={{ color: "white" }} />,
      action: handleNextQuestion,
    },
  ];

  const rightIconActions = [
    {
      icon: <AccountCircleRoundedIcon style={{ color: "white" }} />,
      action: handleMyProfile,
    },
    {
      icon: <ExitToAppRoundedIcon style={{ color: "white" }} />,
      action: handleLogOut,
    },
  ];

  return (
    <Navbar
      collapseOnSelect
      expand="lg"
      variant="dark"
      fixed="top"
      style={{ backgroundColor: "rgba(189, 179, 230, 0.1)", maxHeight: "3rem" }}
    >
      <Container fluid>
        <LinkContainer to="/">
          <Navbar.Brand>
            Interview
            <span style={{ color: "#664fba" }}>
              <b>Master</b>
            </span>
          </Navbar.Brand>
        </LinkContainer>

        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
        <Navbar.Collapse id="responsive-navbar-nav">
          <Nav className="me-auto">
            {leftIconActions.map((item) => (
              <QuestionMenuIconLink
                icon={item.icon}
                action={item.action}
              ></QuestionMenuIconLink>
            ))}
          </Nav>

          <Nav>
            <Nav className="me-auto">
              {rightIconActions.map((item) => (
                <QuestionMenuIconLink
                  icon={item.icon}
                  action={item.action}
                ></QuestionMenuIconLink>
              ))}
            </Nav>
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
};

export default QuestionMenu;
