import type {PostResponse} from "./PostResponse.ts";
import type {CommentResponse} from "../../comment/responses/CommentResponse.ts";

export type PostWithComments = {
    post: PostResponse;
    comments: CommentResponse[];
}