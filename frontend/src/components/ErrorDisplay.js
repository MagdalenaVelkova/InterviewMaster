import { useSelector } from "react-redux";
import { Alert, Container } from "react-bootstrap";

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
