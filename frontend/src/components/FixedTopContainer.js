import ErrorDisplay from "./ErrorDisplay";
import Header from "./Header";
import { Container } from "react-bootstrap";
import styles from "./FixedTopContainerStyles.module.css";

const FixedTopContainer = () => {
  return (
    <div className={styles.vertAlign} fix="top">
      <Header></Header>
      <ErrorDisplay></ErrorDisplay>
    </div>
  );
};

export default FixedTopContainer;
