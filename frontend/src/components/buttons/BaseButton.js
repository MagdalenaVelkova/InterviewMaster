import { Button } from "react-bootstrap";

const BaseButton = (props) => {
  let buttonText = props.text;
  return (
    <Button
      className="btn btn-lg shadow lift me-4"
      variant="outline-light"
      type="submit"
    >
      {buttonText}
    </Button>
  );
};

export default BaseButton;
