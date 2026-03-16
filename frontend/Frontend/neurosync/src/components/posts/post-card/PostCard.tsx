import type {PostResponse} from "../../../models/post/responses/PostResponse.ts";
import './PostCard.css'
import {useNavigate} from "react-router-dom";

interface PostCardProps {
    post: PostResponse;
}
export const PostCard = ({ post }: PostCardProps) => {
    const navigate = useNavigate();

    const handlePostClick = () => {
        navigate(`/posts/${post.id}`);
    }

    return (
        <div>
            <img src={post.posterUrl}
                 height={300}
                 style={{
                     width: '100%',
                     height: '300px',
                     objectFit: 'contain',
                     borderRadius: '8px'
                 }}
                 alt={post.title} />
            <h3>{post.title}</h3>
            <p>{post.description}</p>
            <button onClick={handlePostClick} type="button">Подробнее</button>
        </div>
    )
}