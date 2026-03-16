import { useState, useEffect } from "react";
import {useParams} from "react-router-dom";
import {postService} from "../../../services/post-service/postService.ts";
import type {PostWithComments} from "../../../models/post/responses/PostWithComments.ts";

export const PostView = () => {
    const { postId } = useParams();
    const [post, setPost] = useState<PostWithComments | null>(null);

    useEffect(() => {
        if (postId) {
            postService.getPostById(postId).then(response => {
                setPost(response.data);
            });
        }
    }, [postId]);


    if (!post) {
        return (
            <div className="loading-container">
                <div className="loader"></div>
                <p>Загрузка поста...</p>
            </div>
        );
    }

    return (
        <div className="product-view">
            <div className="product-view-container">
                <div className="product-view-image">
                    <div className="product-view-image-placeholder">
                        <img src={post.post.posterUrl}
                             height={300}
                             style={{
                                 width: '100%',
                                 height: '300px',
                                 objectFit: 'contain',
                                 borderRadius: '8px'
                             }}
                             alt={post.post.title} />
                    </div>
                </div>

                <div className="product-view-info">
                    <h2>{post.post.title}</h2>
                    <p>{post.post.authorName}</p>
                    <p className="product-view-description">
                        {post.post.description}
                    </p>
                </div>
            </div>
        </div>
    );
};