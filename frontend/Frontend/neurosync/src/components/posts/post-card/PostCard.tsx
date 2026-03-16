import type { PostResponse } from "../../../models/post/responses/PostResponse.ts";
import './PostCard.css'
import { useNavigate } from "react-router-dom";

interface PostCardProps {
    post: PostResponse;
}

export const PostCard = ({ post }: PostCardProps) => {
    const navigate = useNavigate();

    const handlePostClick = () => {
        navigate(`/posts/${post.id}`);
    }

    return (
        <div className="post-card">
            <img
                src={post.posterUrl}
                className="post-image"
                alt={post.title}
            />
            <div className="post-content">
                <h3 className="post-title">{post.title}</h3>
                <p className="post-description">{post.description}</p>
                <button
                    onClick={handlePostClick}
                    className="post-button"
                    type="button"
                >
                    Подробнее
                </button>
            </div>
        </div>
    )
}