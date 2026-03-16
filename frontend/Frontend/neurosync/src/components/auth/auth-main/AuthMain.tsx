import './AuthMain.css'
import {Outlet} from "react-router-dom";

export const AuthMain = () => {
    return (
        <div className={"auth-container"}>
            <div className={"auth-place"}>
                <Outlet/>
            </div>
        </div>
    )
}