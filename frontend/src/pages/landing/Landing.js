import { Col, Container, Image, Row } from "react-bootstrap";
import { useHistory } from "react-router";
import myImage from "../../assets/img/header_image.png";
import BaseButton from "../../components/buttons/BaseButton";
import styles from "./Landing.module.css";
function LandingPage() {
  const history = useHistory();

  const handleSubmitQuestionsLibrary = () => {
    history.push(`/questionslibrary`);
  };
  const handleSubmitRegister = () => {
    history.push(`/register`);
  };
  return (
    <div>
      {/* Section One  */}
      <section className={styles.sectionOne}>
        <Container>
          {/* Row */}
          <Row className={styles.row}>
            {/* Column */}
            <Col md={5} lg={6}>
              <h1 className={styles.mainHeadings}>
                Welcome to Interview
                <span style={{ color: "#664fba" }}>
                  <b>Master</b>
                </span>
                . <br />
                Get ready for the real world.
              </h1>
              <p className="lead text-md-start text-muted mb-6 mb-lg-8">
                This is an app for competency based interviews and application
                tracking.
              </p>
              <div className="text-md-start">
                <BaseButton
                  text="Browse Our Questions Library"
                  clickHandler={handleSubmitQuestionsLibrary}
                />
                <BaseButton
                  text="Register"
                  clickHandler={handleSubmitRegister}
                />
              </div>
              {/* Column Ends*/}
            </Col>
            {/* Column */}
            <Col md={5} lg={6}>
              <Image src={myImage} fluid></Image>
              {/* Column Ends*/}
            </Col>
            {/* Row Ends */}
          </Row>
        </Container>
        {/* Section Two */}
        <Container className={styles.sectionTwo}>
          {/* Row */}
          <Row className={styles.row}>
            {/* Column */}
            <Col className="aos-init aos-animate" md={4} data-aos="fade-up">
              <h3 className={styles.HeadingTwo}>Made for students</h3>
              <p className="text-light mb-6 mb-md-0">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam ac
                elit lacus. Sed egestas fringilla ante, quis vehicula dolor
                luctus ut. Etiam hendrerit sapien eu lectus scelerisque, eu
                egestas enim efficitur. Aenean in velit ex. Aliquam consequat
                purus quis augue ornare, nec fermentum tortor rutrum. Mauris
                quis justo id dolor.
              </p>
            </Col>
            {/* Column */}
            <Col
              className="aos-init aos-animate"
              md={4}
              data-aos="fade-up"
              data-aos-delay="50"
            >
              <h3 className={styles.HeadingTwo}>Made for students</h3>
              <p className="text-light mb-6 mb-md-0">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam ac
                elit lacus. Sed egestas fringilla ante, quis vehicula dolor
                luctus ut. Etiam hendrerit sapien eu lectus scelerisque, eu
                egestas enim efficitur. Aenean in velit ex. Aliquam consequat
                purus quis augue ornare, nec fermentum tortor rutrum. Mauris
                quis justo id dolor.
              </p>

              {/* Column Ends*/}
            </Col>
            {/* Column */}
            <Col
              className="aos-init aos-animate"
              md={4}
              data-aos="fade-up"
              data-aos-delay="100"
            >
              <h3 className={styles.HeadingTwo}>Made for students</h3>
              <p className="text-light mb-6 mb-md-0">
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam ac
                elit lacus. Sed egestas fringilla ante, quis vehicula dolor
                luctus ut. Etiam hendrerit sapien eu lectus scelerisque, eu
                egestas enim efficitur. Aenean in velit ex. Aliquam consequat
                purus quis augue ornare, nec fermentum tortor rutrum. Mauris
                quis justo id dolor.
              </p>
              {/* Column Ends*/}
            </Col>
            {/* Row Ends */}
          </Row>
        </Container>
      </section>

      {/* Section Three */}
      <section className={styles.sectionThree}>
        <Container>
          {/* Row */}
          <Row className={styles.row}>
            <Col>
              <h2 className={styles.HeadingThree}>
                Something that it's made for students and it's free.
              </h2>
              <p className="text-light">Careful surveys prove you need it!</p>
              {/* Column Ends*/}
            </Col>
            {/* Row Ends */}
          </Row>
          {/* Row */}
          <Row className="justify-content-center">
            {/* Column */}
            <Col> {/* Column Ends*/}</Col>
            {/* Row Ends */}
          </Row>
        </Container>
      </section>
    </div>
  );
}

export default LandingPage;
