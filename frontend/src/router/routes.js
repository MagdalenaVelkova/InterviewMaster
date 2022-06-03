import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Route, Switch } from "react-router-dom";
import AllApplicationsPage from "../pages/allApplicationsPage/AllApplicationsPage";
import IndividualQuestionPage from "../pages/individualQuestionPage/IndividualQuestionPage";
import LandingPage from "../pages/landing/Landing";
import Login from "../pages/login/Login";
import ProfilePage from "../pages/profilePage/ProfilePage";
import QuestionsLibrary from "../pages/questionsLibrary/QuestionsLibrary";
import Register from "../pages/register/Register";
import { validateToken } from "../redux/actions";

const Routes = () => {
  const dispatch = useDispatch();
  const authenticateUser = () => dispatch(validateToken());
  const isUserAuthenticated = useSelector(
    (state) => state.userReducer.isAuthenticated
  );

  useEffect(() => {
    authenticateUser();
  }, []);

  const routes = [
    {
      isExact: true,
      path: "/",
      component: LandingPage,
      isPrivate: false,
    },
    {
      isExact: true,
      path: "/questionslibrary",
      component: QuestionsLibrary,
      isPrivate: false,
    },
    {
      isExact: true,
      path: "/questionslibrary/:interviewQuestionId",
      component: IndividualQuestionPage,
      isPrivate: true,
    },
    {
      isExact: true,
      path: "/applications",
      component: AllApplicationsPage,
      isPrivate: true,
    },
    {
      isExact: true,
      path: "/login",
      component: Login,
      isPrivate: false,
    },
    {
      isExact: true,
      path: "/myprofile",
      component: ProfilePage,
      isPrivate: true,
    },
    {
      isExact: true,
      path: "/register",
      component: Register,
      isPrivate: false,
    },
  ];

  return (
    <Switch>
      {/* eslint-disable-next-line array-callback-return */}
      {routes.map((route, index) => {
        if (route.isPrivate && isUserAuthenticated) {
          return (
            <Route
              key={index}
              exact={route.isExact}
              path={route.path}
              component={route.component}
            />
          );
        } else {
          return (
            <Route
              key={index}
              exact={route.isExact}
              path={route.path}
              component={route.component}
            />
          );
        }
      })}
    </Switch>
  );
};

export default Routes;
