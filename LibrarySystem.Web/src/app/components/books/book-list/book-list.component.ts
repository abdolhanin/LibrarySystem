import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../services/book.service';
import { Book } from '../../../models/book.model';
import { MatDialog } from '@angular/material/dialog';
import { BookFormComponent } from '../book-form/book-form.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-book-list',
  templateUrl: './book-list.component.html',
  styleUrls: ['./book-list.component.css'],
})
export class BookListComponent implements OnInit {
  books: Book[] = [];
  loading = false;
  error: string | null = null;

  constructor(
    private bookService: BookService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.loadBooks();
  }

  loadBooks(): void {
    this.loading = true;
    this.error = null;

    this.bookService.getAllBooks().subscribe({
      next: (books) => {
        this.books = books;
        this.loading = false;
        console.log('Books loaded:', books);
      },
      error: (error) => {
        this.error = 'Failed to load books. Please try again.';
        this.loading = false;
        console.error('Error loading books:', error);
        this.snackBar.open('Error loading books', 'Close', {
          duration: 5000,
        });
      },
    });
  }

  openBookForm(book?: Book): void {
    const dialogRef = this.dialog.open(BookFormComponent, {
      width: '600px',
      data: book ? { ...book } : null,
    });

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {
        this.loadBooks();
      }
    });
  }

  archiveBook(id: string): void {
    if (confirm('Are you sure you want to archive this book?')) {
      this.bookService.archiveBook(id).subscribe({
        next: () => {
          this.snackBar.open('Book archived successfully', 'Close', {
            duration: 3000,
          });
          this.loadBooks();
        },
        error: (error) => {
          this.snackBar.open('Error archiving book', 'Close', {
            duration: 3000,
          });
        },
      });
    }
  }

  lendBook(id: string): void {
    this.bookService.lendBook(id).subscribe({
      next: () => {
        this.snackBar.open('Book lent successfully', 'Close', {
          duration: 3000,
        });
        this.loadBooks();
      },
      error: (error) => {
        this.snackBar.open('Error lending book', 'Close', {
          duration: 3000,
        });
      },
    });
  }

  returnBook(id: string): void {
    this.bookService.returnBook(id).subscribe({
      next: () => {
        this.snackBar.open('Book returned successfully', 'Close', {
          duration: 3000,
        });
        this.loadBooks();
      },
      error: (error) => {
        this.snackBar.open('Error returning book', 'Close', {
          duration: 3000,
        });
      },
    });
  }
}
