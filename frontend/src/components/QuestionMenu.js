import { Navbar, Container, Nav, NavDropdown } from "react-bootstrap";
import React from "react";
import { LinkContainer } from "react-router-bootstrap";
import { useSelector } from "react-redux";
import { useLocation } from "react-router-dom";
import ListRoundedIcon from "@material-ui/icons/ListRounded";
import ArrowForwardRoundedIcon from "@material-ui/icons/ArrowForwardRounded";
import FavoriteRoundedIcon from "@material-ui/icons/FavoriteRounded";
import AccountCircleRoundedIcon from "@material-ui/icons/AccountCircleRounded";
import ExitToAppRoundedIcon from "@material-ui/icons/ExitToAppRounded";
import { ButtonBase, IconButton } from "@material-ui/core";
import { height, maxHeight } from "@material-ui/system";

function QuestionMenu() {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );

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
                <LinkContainer to="/logout">
                  <Nav.Link>
                    <IconButton color="inherit">
                      {" "}
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
