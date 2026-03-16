import './Home.css'
import { useEffect, useState } from "react";
import type { PostResponse } from "../../models/post/responses/PostResponse.ts";
import { postService } from "../../services/post-service/postService.ts";
import { PostList } from "../posts/post-list/PostList.tsx";

export const Home = () => {
    const [posts, setPosts] = useState<PostResponse[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const getPosts = async () => {
            try {
                setLoading(true);
                setError(null);
                const response = await postService.getAll();
                setPosts(response.data);
            } catch (err) {
                setError('Не удалось загрузить посты');
                console.error(err);
            } finally {
                setLoading(false);
            }
        }
        getPosts();
    }, []);

    if (loading) {
        return (
            <div className="home-container">
                <div className="loading-container">
                    <div className="loading-spinner"></div>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="home-container">
                <div className="error-container">
                    <p className="error-message">{error}</p>
                    <button
                        className="retry-button"
                        onClick={() => window.location.reload()}
                    >
                        Попробовать снова
                    </button>
                </div>
            </div>
        );
    }

    return (
        <div className="home-container">
            <PostList posts={posts} />
        </div>
    )
}