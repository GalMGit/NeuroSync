
import type { CommentResponse } from "../../models/comment/responses/CommentResponse.ts";

import './CommentCard.css';

interface CommentCardProps {
    comment: CommentResponse;
}

export const CommentCard = ({ comment }: CommentCardProps) => {
    // const [liked, setLiked] = useState(false);
    // const [likesCount, setLikesCount] = useState(comment.likesCount || 0);

    // const handleLike = () => {
    //     if (liked) {
    //         setLikesCount(prev => prev - 1);
    //     } else {
    //         setLikesCount(prev => prev + 1);
    //     }
    //     setLiked(!liked);
    // };

    // const formatDate = (dateString: string) => {
    //     const date = new Date(dateString);
    //     return date.toLocaleDateString('ru-RU', {
    //         day: 'numeric',
    //         month: 'long',
    //         year: 'numeric',
    //         hour: '2-digit',
    //         minute: '2-digit'
    //     });
    // };

    return (
        <div className="comment-card">
            <div className="comment-header">
                <div className="comment-avatar">
                    {comment.authorName.charAt(0).toUpperCase()}
                </div>
                <span className="comment-author">{comment.authorName}</span>
                <span className="comment-date">
                    {/*{formatDate(comment.createdAt)}*/}
                </span>
            </div>

            <p className="comment-text">{comment.text}</p>

            <div className="comment-actions">

                <button className="comment-action">
                    <span className="action-icon">💬</span>
                    <span>Ответить</span>
                </button>

                <button className="comment-action">
                    <span className="action-icon">↗️</span>
                    <span>Поделиться</span>
                </button>
            </div>
        </div>
    );
};