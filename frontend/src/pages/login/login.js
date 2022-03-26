import { Container, Row, Col, Form, Button } from "react-bootstrap";
import styles from "./LoginStyles.module.css";
import { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { loginUser } from "../../redux/actions";

const Login = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorDisplay, setErrorDisplay] = useState("");
  const dispatch = useDispatch();

  const handleEmail = (event) => {
    setEmail(event.target.value);
  };

  const handlePassword = (event) => {
    setPassword(event.target.value);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    dispatch(loginUser(email, password));
  };
  return (
    <Container className={styles.containerRoot} fluid>
      <section>
        <Container className={styles.container}>
          <Row className={styles.row}>
            <Col
              md={6}
              style={{
                background: "rgba(0, 0, 0, 0.15)",
                padding: "3rem",
                paddingTop: "1rem",
                borderRadius: "0.2rem",
              }}
            >
              <h1 style={{ color: "#664fba" }}>Login</h1>
              <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                  <Form.Label className="text-light">Email address</Form.Label>
                  <Form.Control
                    type="email"
                    placeholder="Enter email"
                    value={email}
                    onChange={handleEmail}
                    name="Email"
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicPassword">
                  <Form.Label className="text-light">Password</Form.Label>
                  <Form.Control
                    type="password"
                    placeholder="Password"
                    value={password}
                    onChange={handlePassword}
                    name="Password"
                  />
                </Form.Group>

                <Button
                  className="btn btn-custom shadow lift me-3"
                  variant="outline-light"
                  type="submit"
                  onClick={handleSubmit}
                >
                  Submit
                  {errorDisplay}
                </Button>
              </Form>
            </Col>
          </Row>
        </Container>
      </section>
    </Container>
  );
};
export default Login;
