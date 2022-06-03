import { Button } from "react-bootstrap";
const BaseButton = (props) => {
  let buttonText = props.text;
  return (
    <Button
      className="btn btn-lg shadow lift me-4"
      // startIcon={<Icon src="../../assets/img/GoogleLogo.svg" />}
      variant="outline-light"
      type="submit"
      onClick={props.clickHandler}
    >
      {buttonText}
    </Button>
  );
};

export default BaseButton;
