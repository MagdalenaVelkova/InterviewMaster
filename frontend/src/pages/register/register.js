import { useState } from "react";
import { Col, Container, Form, Row } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { useHistory } from "react-router";
import BaseButton from "../../common/buttons/BaseButton";
import { registerUser } from "../../redux/actions";
import styles from "./Register.module.css";

function Register() {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );

  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");

  const handleEmail = (event) => {
    setEmail(event.target.value);
  };

  const handlePassword = (event) => {
    setPassword(event.target.value);
  };

  const handleFirstName = (event) => {
    setFirstName(event.target.value);
  };

  const handleLastName = (event) => {
    setLastName(event.target.value);
  };

  const history = useHistory();
  const dispatch = useDispatch();

  const handleSubmit = async (event) => {
    try {
      event.preventDefault();
      dispatch(registerUser(email, password, firstName, lastName)).then(
        history.push(`/`)
      );
    } catch (error) {
      console.error("submission failed", error);
    }
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
              <h1 className={styles.title}>Register</h1>
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

                <Form.Group className="mb-3" controlId="formBasicFirstName">
                  <Form.Label className="text-light">First Name</Form.Label>
                  <Form.Control
                    type="firstName"
                    placeholder="John"
                    value={firstName}
                    onChange={handleFirstName}
                    name="firstName"
                  />
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicLastName">
                  <Form.Label className="text-light">Last Name</Form.Label>
                  <Form.Control
                    type="lastName"
                    placeholder="Doe"
                    value={lastName}
                    onChange={handleLastName}
                    name="lastName"
                  />
                </Form.Group>
                <BaseButton text="Submit" clickHandler={handleSubmit} />
              </Form>
            </Col>
          </Row>
        </Container>
      </section>
    </Container>
  );
}
export default Register;
