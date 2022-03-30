import { Container, Row, Col, Form } from "react-bootstrap";
import styles from "./styles.module.css";
import { useState } from "react";
import axios from "axios";
import BaseButton from "../../components/Buttons/BaseButton";

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
            <Col
              md={6}
              style={{
                background: "rgba(0, 0, 0, 0.15)",
                padding: "3rem",
                paddingTop: "1rem",
                borderRadius: "0.2rem",
              }}
            >
              <h1 style={{ color: "#664fba" }}>Register</h1>
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
