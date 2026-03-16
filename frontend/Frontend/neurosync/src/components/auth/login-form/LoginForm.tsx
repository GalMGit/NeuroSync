import './LoginForm.css'
import {type ChangeEvent, type SyntheticEvent, useState} from "react";
import {useNavigate} from "react-router-dom";
import type {LoginRequest} from "../../../models/auth/login/requests/LoginRequest.ts";
import {authService} from "../../../services/auth-service/authService.ts";

const initialState: LoginRequest = {
    email: "",
    password: ""
}

export const LoginForm = () => {

    const [loginUser, setLoginUser] = useState<LoginRequest>(initialState)
    const navigate = useNavigate();
    const login = async (e: SyntheticEvent<HTMLFormElement>) => {
        e.preventDefault();

        const response = await authService.login(loginUser);
        localStorage.setItem('token', response.data.token);
        setLoginUser(initialState);

        window.location.href = "/";
    }

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        setLoginUser({...loginUser, [e.target.name]: e.target.value})
    }


    return (
        <form className={"auth-form"}
              onSubmit={login}>
            <h2>Добро пожаловать!</h2>
            <input
                name={"email"}
                className={"auth-input"}
                value={loginUser.email}
                onChange={handleInputChange}
                placeholder={"Email"}
                required={true}
                type={"email"}/>
            <input
                name={"password"}
                className={"auth-input"}
                onChange={handleInputChange}
                value={loginUser.password}
                placeholder={"Пароль"}
                required={true}
                type={"password"}/>
            <div className={"button-group"}>
                <button
                    type={"submit"}
                    className={"auth-button"}>
                    Войти
                </button>
                <button
                    onClick={() => {
                        navigate("/auth/register")
                    }}
                    className={"auth-button"}
                    type={"button"}>
                    Регистрация
                </button>
            </div>
            <button
                onClick={() => {navigate("/")}}
                className={"to-main-button"}>На главную</button>
        </form>
    )
}