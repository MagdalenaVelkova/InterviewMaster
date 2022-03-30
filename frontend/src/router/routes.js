import { Route, Switch } from "react-router-dom";
import allApplicationsPage from "../pages/AllApplicationsPage/AllApplicationsPage";
import individualQuestionPage from "../pages/individualQuestionPage/IndividualQuestionPage";
import landingPage from "../pages/landing/Landing";
import login from "../pages/login/Login";
import profilePage from "../pages/profilePage/ProfilePage";
import questionsLibrary from "../pages/QuestionsLibrary/QuestionsLibrary";
import register from "../pages/register/Register";

const Routes = () => {
  return (
    <Switch>
      <Route path="/login" component={login}></Route>
      <Route path="/register" component={register}></Route>
      <Route
        path="/questionslibrary/:id"
        component={individualQuestionPage}
      ></Route>
      <Route path="/questionslibrary" component={questionsLibrary}></Route>
      <Route path="/myprofile" component={profilePage}></Route>
      <Route path="/applications" component={allApplicationsPage}></Route>
      <Route path="/logout"></Route>
      <Route path="/" component={landingPage}></Route>
    </Switch>
  );
};

export default Routes;
