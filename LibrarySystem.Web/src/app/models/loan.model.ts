export interface Loan {
  id?: string;
  bookId: string;
  userId: string;
  userName?: string;
  bookTitle?: string;
  loanDate: Date;
  returnDate?: Date;
  isReturned: boolean;
}
