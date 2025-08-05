import { Component, OnInit } from '@angular/core';
import { LoanService } from '../../../services/loan.service';
import { BookService } from '../../../services/book.service';
import { Loan } from '../../../models/loan.model';
import { Book } from '../../../models/book.model';
import { MatDialog } from '@angular/material/dialog';
import { LoanFormComponent } from '../loan-form/loan-form.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-loan-list',
  templateUrl: './loan-list.component.html',
  //   styleUrls: ['./loan-list.component.css']
})
export class LoanListComponent implements OnInit {
  loans: Loan[] = [];
  books: Book[] = [];
  displayedColumns: string[] = [
    'bookTitle',
    'userName',
    'loanDate',
    'returnDate',
    'isReturned',
    'actions',
  ];

  constructor(
    private loanService: LoanService,
    private bookService: BookService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  loadData(): void {
    forkJoin({
      loans: this.loanService.getAllLoans(),
      books: this.bookService.getAllBooks(),
    }).subscribe({
      next: ({ loans, books }) => {
        this.books = books;
        this.loans = loans.map((loan) => ({
          ...loan,
          bookTitle:
            books.find((b) => b.id === loan.bookId)?.title || 'Unknown Book',
        }));
      },
      error: (error) => {
        this.snackBar.open('Error loading data', 'Close', { duration: 3000 });
      },
    });
  }

  openLoanForm(loan?: Loan): void {
    const dialogRef = this.dialog.open(LoanFormComponent, {
      width: '600px',
      data: { loan: loan ? { ...loan } : null, books: this.books },
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadData();
      }
    });
  }

  returnLoan(id: string): void {
    this.loanService.returnLoan(id).subscribe({
      next: () => {
        this.snackBar.open('Book returned successfully', 'Close', {
          duration: 3000,
        });
        this.loadData();
      },
      error: (error) => {
        this.snackBar.open('Error returning book', 'Close', { duration: 3000 });
      },
    });
  }

  deleteLoan(id: string): void {
    if (confirm('Are you sure you want to delete this loan?')) {
      this.loanService.deleteLoan(id).subscribe({
        next: () => {
          this.snackBar.open('Loan deleted successfully', 'Close', {
            duration: 3000,
          });
          this.loadData();
        },
        error: (error) => {
          this.snackBar.open('Error deleting loan', 'Close', {
            duration: 3000,
          });
        },
      });
    }
  }
}
