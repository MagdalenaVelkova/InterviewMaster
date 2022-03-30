import { useState } from "react";
import { Col, Container, Form, Row } from "react-bootstrap";
import { useDispatch } from "react-redux";
import BaseButton from "../../components/Buttons/BaseButton";
import { loginUser } from "../../redux/actions";
import styles from "./LoginStyles.module.css";

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

                <BaseButton text="Submit" onClick={handleSubmit}>
                  {errorDisplay}
                </BaseButton>
              </Form>
            </Col>
          </Row>
        </Container>
      </section>
    </Container>
  );
};
export default Login;
