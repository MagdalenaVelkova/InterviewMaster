import { Switch, Route } from "react-router-dom";
import Login from "../pages/login/login";
import Register from "../pages/register/register";
import LandingPage from "../pages/landing/Landing";
import questionsLibrary from "../pages/QuestionsLibrary/questionsLibrary";
import profilePage from "../pages/ProfilePage/profilePage";
import allApplicationsPage from "../pages/AllApplicationsPage/allApplicationsPage";
import IndividualQuestionPage from "../pages/IndividualQuestionPage/individualQuestionPage";

const Routes = () => {
  return (
    <Switch>
      <Route path="/login" component={Login}></Route>
      <Route path="/register" component={Register}></Route>
      <Route
        path="/questionslibrary/:id"
        component={IndividualQuestionPage}
      ></Route>
      <Route path="/questionslibrary" component={questionsLibrary}></Route>
      <Route path="/myprofile" component={profilePage}></Route>
      <Route path="/applications" component={allApplicationsPage}></Route>
      <Route path="/logout"></Route>
      <Route path="/" component={LandingPage}></Route>
    </Switch>
  );
};

export default Routes;
