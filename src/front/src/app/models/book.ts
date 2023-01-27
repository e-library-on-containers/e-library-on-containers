export interface Book {
  id: number;
  isbn: string;
  title: string;
  authors: string;
  description: string;
  isAvailable: boolean;
  copiesCount: number;
  coverImg: string;
}
