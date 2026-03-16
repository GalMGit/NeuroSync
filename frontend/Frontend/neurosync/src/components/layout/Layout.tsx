import './Layout.css'
import {Outlet, useNavigate} from "react-router-dom";

export const Layout = () => {
    const navigate = useNavigate();
    const isAuthenticated = !!localStorage.getItem('token');

    return (
        <div className="layout">
            <header className="header">
                <div className="header-container">
                    <div className="logo" onClick={() => navigate('/')}>
                        <span className="logo-text">NeuroSync</span>
                    </div>

                    <nav className="nav-menu">
                        {isAuthenticated ? (
                            <>
                                <button
                                    className="nav-item"
                                    onClick={() => {
                                        localStorage.removeItem('token');
                                        window.location.reload();
                                    }}
                                >
                                    <span className="nav-icon">🚪</span>
                                    <span>Выйти</span>
                                </button>
                            </>
                        ) : (
                            <>
                                <button
                                    className="nav-item"
                                    onClick={() => navigate('/')}
                                >
                                    <span className="nav-icon">🏠</span>
                                    <span>Главная</span>
                                </button>
                                <button
                                    className="nav-item"
                                    onClick={() => navigate('/auth')}
                                >
                                    <span className="nav-icon">🔑</span>
                                    <span>Вход</span>
                                </button>
                            </>
                        )}

                    </nav>
                </div>
            </header>

            <main className="main-content">
                <Outlet />
            </main>
        </div>
    );
};