import AddReactionRoundedIcon from "@mui/icons-material/AddReactionRounded";
import CelebrationRoundedIcon from "@mui/icons-material/CelebrationRounded";
import ContactPageRoundedIcon from "@mui/icons-material/ContactPageRounded";
import ElectricBoltRoundedIcon from "@mui/icons-material/ElectricBoltRounded";
import axios from "axios";
import { useEffect, useState } from "react";
import { Col, Container, Row } from "react-bootstrap";
import Questions from "../questionsLibrary/Questions";
import styles from "./ProfilePage.module.css";

const ProfilePage = (props) => {
  const columnNumber = 3;
  const [profile, setProfile] = useState("");
  const [favouriteQuestions, setFavouriteQuestions] = useState([]);
  const [respondedQuestions, setRespondedQuestions] = useState([]);

  // need to dynamically get user id with redux
  const getProfile = async () => {
    const res = await axios.get(`http://localhost:5000/userprofile`);
    setProfile(res.data);
  };

  const getFavouriteQuestions = async () => {
    const res = await axios.get(`http://localhost:5000/favourites`);
    setFavouriteQuestions(res.data);
  };

  const getRespondedQuestions = async () => {
    const res = await axios.get(`http://localhost:5000/solutions`);
    setRespondedQuestions(res.data);
  };

  useEffect(() => {
    getProfile();
    getFavouriteQuestions();
    getRespondedQuestions();
  }, []);
  return (
    <div>
      <section className={styles.titleSection}>
        <Container className={styles.titleContainer}>
          <Row className={styles.row}>
            <h1 className={styles.mainHeadings}>
              Hi there, {profile.firstName}!
            </h1>
            <p className="lead text-center text-muted mb-6 mb-lg-8">
              Are you ready to step up your employability game?
            </p>
          </Row>
        </Container>
      </section>
      <div className={styles.waveSection}></div>
      <section className={styles.sectionFavContent}>
        <Container>
          <Row className={styles.row}>
            <Col>
              {" "}
              <h2 className={styles.statsHeadings}>Responses</h2>
              <h1 className={styles.statsNumbers}>45/120</h1>
            </Col>
            <Col>
              {" "}
              <h2 className={styles.statsHeadings}>Trophy Gallery</h2>
              <Row className={styles.Row}>
                <Col>
                  <AddReactionRoundedIcon
                    className={styles.trophyIcon}
                  ></AddReactionRoundedIcon>
                </Col>
                <Col>
                  {" "}
                  <CelebrationRoundedIcon
                    className={styles.trophyIcon}
                  ></CelebrationRoundedIcon>
                </Col>
                <Col>
                  {" "}
                  <ContactPageRoundedIcon
                    className={styles.trophyIcon}
                  ></ContactPageRoundedIcon>
                </Col>
                <Col>
                  <ElectricBoltRoundedIcon
                    className={styles.trophyIcon}
                  ></ElectricBoltRoundedIcon>
                </Col>
              </Row>
            </Col>
            <Col>
              <h2 className={styles.statsHeadings}>Open Aplications</h2>
              <h1 className={styles.statsNumbers}>2</h1>
            </Col>
          </Row>
        </Container>
      </section>

      <div className={styles.waveSectionTwo}></div>
      <section className={styles.sectionFavContent}>
        <Container>
          <Row className={styles.row}>
            <h1 className={styles.subHeadings}>Your Favourites</h1>
          </Row>
        </Container>
        <Container>
          <Questions questions={favouriteQuestions}></Questions>
        </Container>
      </section>
      <section className={styles.sectionFavContent}>
        <Container>
          <Row className={styles.row}>
            <h1 className={styles.subHeadings}>Check out your responses</h1>
          </Row>
        </Container>{" "}
        <Container>
          <Questions questions={respondedQuestions}></Questions>
        </Container>
      </section>
    </div>
  );
};
export default ProfilePage;
