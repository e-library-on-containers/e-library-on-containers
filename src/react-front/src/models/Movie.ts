export interface Movie {
    id: number,
    title: string,
    category: string,
    inPreview: boolean,
    screenwriters: string[],
    directors: string[],
    actors: string[],
}