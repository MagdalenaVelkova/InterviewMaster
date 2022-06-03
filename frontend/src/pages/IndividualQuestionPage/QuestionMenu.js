import AccountCircleRoundedIcon from "@material-ui/icons/AccountCircleRounded";
import ArrowBackRoundedIcon from "@material-ui/icons/ArrowBackRounded";
import ArrowForwardRoundedIcon from "@material-ui/icons/ArrowForwardRounded";
import ExitToAppRoundedIcon from "@material-ui/icons/ExitToAppRounded";
import FavoriteRoundedIcon from "@material-ui/icons/FavoriteRounded";
import ListRoundedIcon from "@material-ui/icons/ListRounded";
import React from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { useHistory } from "react-router";
import { LinkContainer } from "react-router-bootstrap";
import { logoutUser } from "../../redux/actions";
import QuestionMenuIconLink from "./QuestionsMenuIconLink";

const QuestionMenu = () => {
  let dispatch = useDispatch();
  const history = useHistory();

  const handleQuestionsLibrary = () => {
    history.push(`/questionslibrary`);
  };

  const handlePreviousQuestion = () => {
    history.push(`/questionslibrary`);
  };

  const handleNextQuestion = () => {
    history.push(`/questionslibrary`);
  };

  const handleFavoriteQuestion = () => {
    history.push(`/questionslibrary`);
  };

  const handleMyProfile = () => {
    history.push(`/myprofile`);
  };

  const handleLogOut = () => dispatch(logoutUser());
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
    {
      icon: <FavoriteRoundedIcon style={{ color: "white" }} />,
      action: handleFavoriteQuestion,
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
