import { useState } from "react";
import { Col, Container, Form, Row } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import BaseButton from "../../common/buttons/BaseButton";
import { loginUser } from "../../redux/actions";
import styles from "./Login.module.css";

const Login = () => {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorDisplay, setErrorDisplay] = useState("");
  const dispatch = useDispatch();

  const history = useHistory();

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

  if (isAuthenticated) {
    history.push(`/`);
  }

  return (
    <Container className={styles.containerRoot} fluid>
      <section>
        <Container className={styles.container}>
          <Row className={styles.row}>
            <Col md={8} className={styles.col}>
              <h1 className={styles.title}>Login</h1>
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

                <BaseButton text="Submit" clickHandler={handleSubmit}>
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
