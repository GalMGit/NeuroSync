import {BrowserRouter as Router, Routes, Route, Navigate} from 'react-router-dom'
import {Home} from "../Home/Home.tsx";
import {Layout} from "../layout/Layout.tsx";
import {AuthMain} from "../auth/auth-main/AuthMain.tsx";
import {LoginForm} from "../auth/login-form/LoginForm.tsx";
import {RegisterForm} from "../auth/register-form/RegisterForm.tsx";
import {PostView} from "../posts/post-view/PostView.tsx";
export const App = () => {
  return (
      <Router>
        <Routes>
          <Route path={"/"} element={
            <Layout/>
          }>
            <Route index element={
              <Home/>
            }/>
            <Route path={"/posts/:postId?"} element={
              <PostView/>
            }/>
          </Route>
          <Route>
            <Route path={"/auth"} element={<AuthMain/>}>
              <Route index element={<LoginForm/>}/>
              <Route path={"login"} element={<LoginForm/>}/>
              <Route path={"register"} element={<RegisterForm/>}/>
            </Route>
          </Route>
          <Route path="*" element={<Navigate to="/" replace/>}/>
        </Routes>
      </Router>
  )
}
