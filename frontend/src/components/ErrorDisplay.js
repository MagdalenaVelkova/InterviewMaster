import { Alert, Container } from "react-bootstrap";
import { useSelector } from "react-redux";

const ErrorDisplay = () => {
  const errors = useSelector((state) => state.errorReducer.errorMessages);
  return (
    <Container fluid className="vert-align">
      {errors.map((error) => (
        <Alert variant="secondary">{error}</Alert>
      ))}
    </Container>
  );
};

export default ErrorDisplay;
