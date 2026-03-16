export type PostResponse = {
    id: string;
    authorId: string;
    title: string;
    description: string;
    communityId?: string;
    posterUrl?: string;
    authorName: string;
    communityName?: string;
}