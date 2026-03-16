import type { CommentResponse } from "../../models/comment/responses/CommentResponse.ts";
import { CommentCard } from "../comment-card/CommentCard.tsx";
import './CommentList.css';

interface CommentListProps {
    comments: CommentResponse[];
}

export const CommentList = ({ comments }: CommentListProps) => {
    return (
        <div className="comment-list">
            <h3 className="comment-list-title">
                Комментарии ({comments.length})
            </h3>
            {comments.length === 0 ? (
                <div className="comment-list-empty">
                    <p>Пока нет комментариев</p>
                </div>
            ) : (
                <div className="comment-items">
                    {comments.map((comment) => (
                        <CommentCard
                            comment={comment}
                            key={comment.id}
                        />
                    ))}
                </div>
            )}
        </div>
    );
};