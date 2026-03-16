import {apiService} from "../api-service/apiService.ts";
import type {PostResponse} from "../../models/post/responses/PostResponse.ts";
import type {PostWithComments} from "../../models/post/responses/PostWithComments.ts";
export const commentService = {
    createComment: (data: string) =>
        apiService.post<PostResponse>("comment", data),

    getAllByPostId: (id: string) =>
        apiService.get<PostWithComments>(`comments/post/${id}`),
}