import ErrorDisplay from "./ErrorDisplay";
import styles from "./FixedTopContainerStyles.module.css";
import Header from "./Header";

const FixedTopContainer = () => {
  return (
    <div className={styles.vertAlign} fix="top">
      <Header></Header>
      <ErrorDisplay></ErrorDisplay>
    </div>
  );
};

export default FixedTopContainer;
