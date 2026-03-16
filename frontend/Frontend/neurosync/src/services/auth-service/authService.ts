import {apiService} from "../api-service/apiService.ts";
import type {LoginRequest} from "../../models/auth/login/requests/LoginRequest.ts";
import type {LoginResponse} from "../../models/auth/login/responses/LoginResponse.ts";
import type {RegisterRequest} from "../../models/auth/register/requests/RegisterRequest.ts";
import type {RegisterResponse} from "../../models/auth/register/responses/RegisterResponse.ts";

export const authService = {
    login: (data: LoginRequest) =>
        apiService.post<LoginResponse>("login", data),

    register: (data: RegisterRequest) =>
        apiService.post<RegisterResponse>("register", data),
}