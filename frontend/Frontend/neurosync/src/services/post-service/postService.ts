import {apiService} from "../api-service/apiService.ts";
import type {PostResponse} from "../../models/post/responses/PostResponse.ts";
import type {PostWithComments} from "../../models/post/responses/PostWithComments.ts";
export const postService = {
    getAll: () =>
        apiService.get<PostResponse[]>("all/posts"),

    getPostById: (id: string) =>
        apiService.get<PostWithComments>(`post/${id}`),
}