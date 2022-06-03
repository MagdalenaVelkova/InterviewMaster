import { Col, Container, Image, Row } from "react-bootstrap";
import { useSelector } from "react-redux";
import { useHistory } from "react-router";
import myImage from "../../assets/img/header_image.png";
import BaseButton from "../../common/buttons/BaseButton";
import styles from "./Landing.module.css";

const LandingPage = () => {
  const isAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );
  const history = useHistory();

  const handleSubmitQuestionsLibrary = () => {
    history.push(`/questionslibrary`);
  };
  const handleSubmitRegister = () => {
    history.push(`/register`);
  };

  const handleSubmitMyProfile = () => {
    history.push(`/myprofile`);
  };

  const marketingParagraphs = [
    {
      title: "Weekly-updated questions",
      text: (
        <p className="text-light mb-6 mb-md-0">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam ac elit
          lacus. Sed egestas fringilla ante, quis vehicula dolor luctus ut.
          Etiam hendrerit sapien eu lectus scelerisque, eu egestas enim
          efficitur. Aenean in velit ex. Aliquam consequat purus quis augue
          ornare, nec fermentum tortor rutrum. Mauris quis justo id dolor.
        </p>
      ),
    },
    {
      title: "Developed by industry experts",
      text: (
        <p className="text-light mb-6 mb-md-0">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam ac elit
          lacus. Sed egestas fringilla ante, quis vehicula dolor luctus ut.
          Etiam hendrerit sapien eu lectus scelerisque, eu egestas enim
          efficitur. Aenean in velit ex. Aliquam consequat purus quis augue
          ornare, nec fermentum tortor rutrum. Mauris quis justo id dolor.
        </p>
      ),
    },
    {
      title: "Interactive Design",
      text: (
        <p className="text-light mb-6 mb-md-0">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam ac elit
          lacus. Sed egestas fringilla ante, quis vehicula dolor luctus ut.
          Etiam hendrerit sapien eu lectus scelerisque, eu egestas enim
          efficitur. Aenean in velit ex. Aliquam consequat purus quis augue
          ornare, nec fermentum tortor rutrum. Mauris quis justo id dolor.
        </p>
      ),
    },
  ];

  const starParagraphs = [
    {
      title: "What is the STAR technique?",
      text: (
        <p className="text-light mb-6 mb-md-0">
          Successful candidates know and use the STAR technique, a proven method
          of answering tricky situational questions in a systematic way while
          providing all the essential details.
          <br></br>
          The STAR technique is a method of answering questions that is
          comprised of four steps: <br></br>
          <b>Situation: </b>Describe the situation and when it took place.
          <br></br>
          <b>Task:</b> Explain the task and what was the goal. <br></br>
          <b>Action: </b>Provide details about the action you took to attain
          this. <br></br> <b>Result: </b>Conclude with the result of your
          action.
        </p>
      ),
    },
    {
      title: "Which questions need a STAR response",
      text: (
        <p className="text-light mb-6 mb-md-0">
          Typical competency-based interview questions generally start by asking
          about a time you demonstrated one of the competencies listed in the
          job description. For this reason, it's advisable to familiarise
          yourself with the description before your interview in order to be
          prepared for these questions. Most graduate job interviews ask about
          soft skills such as communication, teamwork and negotiation. Many of
          the questions require you to recount past work experiences. If you are
          applying for an apprenticeship or internship or have little work
          experience, talk about extra-curricular activities and your
          achievements or school projects you've done.
        </p>
      ),
    },
    {
      title: "How the STAR method works",
      text: (
        <p className="text-light mb-6 mb-md-0">
          The STAR method lets you create a simple and easy-to-follow story that
          brings out the difficult situation and resolution. Here's a breakdown
          of what each of the four parts of the technique mean: Situation Set
          the scene of the story by giving a context and the background of the
          situation. If you're asked about teamwork, your response should
          include the project details, who you were collaborating with, when you
          undertook the project and your location at that time. Task Describe
          your exact role or responsibility in the situation. Make sure that the
          hiring manager knows what you were specifically assigned to do, rather
          than what everyone did.
        </p>
      ),
    },
  ];

  return (
    <div>
      <section className={styles.sectionOne}>
        <Container>
          <Row className={styles.row}>
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
                {isAuthenticated ? (
                  <BaseButton
                    text="Profile"
                    clickHandler={handleSubmitMyProfile}
                  />
                ) : (
                  <BaseButton
                    text="Register"
                    clickHandler={handleSubmitRegister}
                  />
                )}
              </div>
            </Col>
            <Col md={5} lg={6}>
              <Image src={myImage} fluid></Image>
            </Col>
          </Row>
        </Container>
        <Container className={styles.sectionTwo}>
          <Row className={styles.row}>
            {marketingParagraphs.map((item) => (
              <Col className="aos-init aos-animate" md={4} data-aos="fade-up">
                <h3 className={styles.HeadingTwo}>{item.title}</h3>
                {item.text}
              </Col>
            ))}
          </Row>
        </Container>
      </section>

      <section className={styles.sectionThree}>
        <Container>
          <Row className={styles.row}>
            <Col>
              <h2 className={styles.HeadingThree}>
                Our quick how-to answer interview questions guide
              </h2>
              <p className="text-light">
                Most job interviews have a segment for competency or behavioural
                questions. Hiring managers use these questions to assess an
                applicant's experience and qualities. These types of questions
                are designed to prompt a story-like response from the
                applicants.
              </p>
            </Col>
          </Row>

          <Container>
            <Row className={styles.row}>
              {starParagraphs.map((item) => (
                <Col
                  className="aos-init aos-animate"
                  md={4}
                  data-aos="fade-up"
                  data-aos-delay="50"
                >
                  <h3 className={styles.HeadingStar}>{item.title}</h3>
                  {item.text}
                </Col>
              ))}
            </Row>
          </Container>
        </Container>
      </section>
    </div>
  );
};

export default LandingPage;
