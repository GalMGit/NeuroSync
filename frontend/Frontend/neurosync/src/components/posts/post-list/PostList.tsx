import type {PostResponse} from "../../../models/post/responses/PostResponse.ts";
import {PostCard} from "../post-card/PostCard.tsx";

interface PostListProps {
    posts: PostResponse[];
}

export const PostList = ({ posts }: PostListProps) => {
    return (
        <div>
            {posts.map((post) => (
                <PostCard
                    post={post}
                    key={post.id}
                />
            ))}
        </div>
    )
}