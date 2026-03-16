import type { PostResponse } from "../../../models/post/responses/PostResponse.ts";
import { PostCard } from "../post-card/PostCard.tsx";
import './PostList.css'

interface PostListProps {
    posts: PostResponse[];
}

export const PostList = ({ posts }: PostListProps) => {
    return (
        <div className={"post-list"}>
            {posts.map((post) => (
                <PostCard
                    post={post}
                    key={post.id}
                />
            ))}
        </div>
    )
}