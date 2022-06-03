import React from "react";
import { Container, Nav, Navbar } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { LinkContainer } from "react-router-bootstrap";
import { useLocation } from "react-router-dom";
import { logoutUser } from "../redux/actions";

function Header() {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );
  let pageLocation = useLocation();
  const dispatch = useDispatch();

  const handleLogout = () => {
    console.log("logout button");
    dispatch(logoutUser());
  };
  if (pageLocation.pathname.includes("questionslibrary/")) {
    return <div></div>;
  } else {
    return (
      <Navbar expand="lg" variant="dark">
        <Container>
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
                  <Nav.Link>Questions Library</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/applications">
                  <Nav.Link>My Applications</Nav.Link>
                </LinkContainer>
              </Nav>
            ) : (
              <Nav className="me-auto">
                <LinkContainer to="/questionslibrary">
                  <Nav.Link>Questions Library</Nav.Link>
                </LinkContainer>
              </Nav>
            )}
            <Nav>
              {isAuthenticated ? (
                <Nav className="me-auto">
                  <LinkContainer to="/myprofile">
                    <Nav.Link>My Profile</Nav.Link>
                  </LinkContainer>
                  <LinkContainer to="/">
                    <Nav.Link onClick={handleLogout}>Logout </Nav.Link>
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
}

export default Header;
