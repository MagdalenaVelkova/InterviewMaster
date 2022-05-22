import { IconButton } from "@material-ui/core";
import AccountCircleRoundedIcon from "@material-ui/icons/AccountCircleRounded";
import ArrowForwardRoundedIcon from "@material-ui/icons/ArrowForwardRounded";
import ExitToAppRoundedIcon from "@material-ui/icons/ExitToAppRounded";
import FavoriteRoundedIcon from "@material-ui/icons/FavoriteRounded";
import ListRoundedIcon from "@material-ui/icons/ListRounded";
import React from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { LinkContainer } from "react-router-bootstrap";
import { logoutUser } from "../redux/actions";

function QuestionMenu() {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );
  const dispatch = useDispatch();

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
          {isAuthenticated ? (
            <Nav className="me-auto">
              <LinkContainer to="/questionslibrary">
                <Nav.Link>
                  <IconButton color="inherit">
                    <ListRoundedIcon></ListRoundedIcon>
                  </IconButton>
                </Nav.Link>
              </LinkContainer>
              <LinkContainer to="/">
                <Nav.Link>
                  <IconButton color="inherit">
                    <ArrowForwardRoundedIcon></ArrowForwardRoundedIcon>
                  </IconButton>
                </Nav.Link>
              </LinkContainer>
              <LinkContainer to="/">
                <Nav.Link>
                  <IconButton color="inherit">
                    {" "}
                    <FavoriteRoundedIcon></FavoriteRoundedIcon>
                  </IconButton>
                </Nav.Link>
              </LinkContainer>
            </Nav>
          ) : (
            <Nav className="me-auto">
              <LinkContainer to="/questionslibrary">
                <Nav.Link>
                  <IconButton color="inherit">
                    <ListRoundedIcon></ListRoundedIcon>
                  </IconButton>
                </Nav.Link>
              </LinkContainer>
            </Nav>
          )}
          <Nav>
            {isAuthenticated ? (
              <Nav className="me-auto">
                {/* <LinkContainer to="/logout">
                  <Nav.Link>
                    <IconButton color="inherit">
                      <ExitToAppRoundedIcon></ExitToAppRoundedIcon>
                    </IconButton>
                  </Nav.Link>
                </LinkContainer> */}
                <LinkContainer to="/myprofile">
                  <Nav.Link>
                    <IconButton color="inherit">
                      {" "}
                      <AccountCircleRoundedIcon></AccountCircleRoundedIcon>
                    </IconButton>
                  </Nav.Link>
                </LinkContainer>
                <LinkContainer to="/" onClick={() => dispatch(logoutUser())}>
                  <Nav.Link>
                    <IconButton color="inherit">
                      <ExitToAppRoundedIcon></ExitToAppRoundedIcon>
                    </IconButton>
                  </Nav.Link>
                </LinkContainer>
              </Nav>
            ) : (
              <Nav className="me-auto">
                <LinkContainer to="/login">
                  <Nav.Link href="/">Login</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/register">
                  <Nav.Link>Register</Nav.Link>
                </LinkContainer>
              </Nav>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}

export default QuestionMenu;
