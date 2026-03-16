import './Home.css'
import {useEffect, useState} from "react";
import type {PostResponse} from "../../models/post/responses/PostResponse.ts";
import {postService} from "../../services/post-service/postService.ts";
import {PostList} from "../posts/post-list/PostList.tsx";

export const Home = () => {
    const [posts, setPosts] = useState<PostResponse[]>([]);

    useEffect(() => {
        const getPosts = async () => {
            const response = await postService.getAll();
            setPosts(response.data)
        }
        getPosts();
    }, []);

    return (
        <div>
            <PostList
                posts={posts}
            />
        </div>
    )
}