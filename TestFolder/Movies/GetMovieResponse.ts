export type GetMovieResponse = {
MovieId: number, 
Description: string, 
Title: string, 
Genre: string, 
ReleaseDate: Date, 
Director: string, 
TheaterId: number, 
ImageUrl: string, 
Sessions: GetSessionResponse[], 
}
