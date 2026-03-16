import type {RegisterRequest} from "../../../models/auth/register/requests/RegisterRequest.ts";
import {type ChangeEvent,  type SyntheticEvent, useState} from "react";
import { useNavigate } from "react-router-dom";
import {authService} from "../../../services/auth-service/authService.ts";
import './RegisterForm.css'

const initialState: RegisterRequest = {
    email: "",
    username: "",
    password: "",
    confirmPassword: ""
}

export const RegisterForm = () => {
    const navigate = useNavigate();
    const [registerUser, setRegisterUser] = useState<RegisterRequest>(initialState)

    const register = async (e: SyntheticEvent<HTMLFormElement>) => {
        e.preventDefault()
        if(registerUser.password !== registerUser.confirmPassword)
        {
            return
        }
        await authService.register(registerUser)
        setRegisterUser(initialState)
    }

    const handleInputChange = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setRegisterUser({...registerUser, [name]: value})
    }

    return (
        <form className={"auth-form"} onSubmit={register}>
            <h2>Создать аккаунт</h2>
            <input
                name={"email"}
                value={registerUser.email}
                onChange={handleInputChange}
                placeholder={"Email"}
                required={true}
                className={"auth-input"}
                type={"email"}/>
            <input
                name={"username"}
                value={registerUser.username}
                onChange={handleInputChange}
                placeholder={"Username"}
                required={true}
                className={"auth-input"}
                type={"text"}/>
            <input
                name={"password"}
                value={registerUser.password}
                onChange={handleInputChange}
                placeholder={"Password"}
                required={true}
                className={"auth-input"}
                type={"password"}/>
            <input
                name={"confirmPassword"}
                value={registerUser.confirmPassword}
                onChange={handleInputChange}
                placeholder={"Confirm Password"}
                required={true}
                className={"auth-input"}
                type={"password"}/>
            <div className={"button-group"}>
                <button
                    className={"auth-button"}
                    type={"submit"}>Зарегистрироваться</button>
                <button
                    onClick={() => { navigate("/auth/login") }}
                    className={"auth-button"}
                    type={"button"}>Вход</button>
            </div>
        </form>
    )
}