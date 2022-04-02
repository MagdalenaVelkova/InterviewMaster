import axios from "axios";
import { useState } from "react";
import { Col, Container, Form, Row } from "react-bootstrap";
import BaseButton from "../../components/buttons/BaseButton";
import styles from "./Register.module.css";

function Register() {
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

  const handleSubmit = async (event) => {
    try {
      event.preventDefault();
      const payload = {
        email: email,
        first_name: firstName,
        last_name: lastName,
        password: password,
      };
      const res = await axios.post(
        "http://localhost:8000/api/register",
        payload
      );
    } catch (error) {
      console.error("submission failed", error);
    }
  };
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
                <BaseButton text="Submit" onClick={handleSubmit} />
              </Form>
            </Col>
          </Row>
        </Container>
      </section>
    </Container>
  );
}
export default Register;
